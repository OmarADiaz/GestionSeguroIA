// CreditRiskAgent.Api/Controllers/ChatController.cs
using CreditRiskAgent.Application.Common.Plugins;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;

namespace CreditRiskAgent.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly Kernel _kernel;
    private readonly SegurosPlugin _segurosPlugin;

    public ChatController(Kernel kernel, SegurosPlugin segurosPlugin)
    {
        _kernel = kernel;
        _segurosPlugin = segurosPlugin;

        // Importar el plugin de seguros al Kernel para que Gemini pueda invocarlo automáticamente
        if (!_kernel.Plugins.Contains("Seguros"))
        {
            _kernel.Plugins.AddFromObject(_segurosPlugin, "Seguros");
        }
    }

    [HttpPost("mensaje")]
    public async Task<IActionResult> EnviarMensaje(
    [FromBody] ChatRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Mensaje))
        {
            return BadRequest(new
            {
                error = "El mensaje no puede estar vacío."
            });
        }

        try
        {
            var chatCompletion = _kernel.GetRequiredService<IChatCompletionService>();

            // IMPORTANTE: En lugar de un historial largo con múltiples roles complejos,
            // inicializamos un historial fresco por cada petición o controlamos los mensajes.
            var history = new ChatHistory();

            // Añadir instrucciones del sistema de manera compatible
            history.AddSystemMessage("""
                Eres un asistente virtual experto en seguros. 
                Utiliza las herramientas disponibles cuando el usuario solicite información real de pólizas, clientes o siniestros. 
                Nunca inventes información, No muestres nombres internos de herramientas ni parámetros técnicos al usuario, 
                no utilices Markdown, No utilices asteriscos, Presenta la información en texto plano, con saltos de línea, encabezados ni listas con símbolos,Utiliza listas y tablas cuando faciliten la lectura y responde de manera clara y profesional.
                """);

            history.AddUserMessage(request.Mensaje);

            // Configuración de ejecución estándar compatible con Gemini
            var executionSettings = new PromptExecutionSettings
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            var result = await chatCompletion.GetChatMessageContentAsync(
                history,
                executionSettings,
                _kernel);

            var response = new ChatResponse
            {
                Pregunta = request.Mensaje,
                Respuesta = result.Content ?? string.Empty,
                Fecha = DateTime.Now,
                Exitoso = true
            };

            return Ok(response);
        }
        catch (HttpOperationException ex)
        {
            Console.WriteLine("ERROR GEMINI:");
            Console.WriteLine($"HTTP: {ex.StatusCode}");
            Console.WriteLine($"DETALLE: {ex.ResponseContent}");

            return StatusCode(502, new
            {
                error = "Error al comunicarse con Gemini.",
                codigo = ex.StatusCode?.ToString(),
                detalle = ex.ResponseContent ?? ex.Message
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            return StatusCode(500, new
            {
                error = "Error al procesar la solicitud.",
                detalle = ex.Message
            });
        }
    }

    
}

public class ChatRequest
{
    public string Mensaje { get; set; } = string.Empty;
}
public class ChatResponse
{
    public string Pregunta { get; set; } = string.Empty;
    public string Respuesta { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public bool Exitoso { get; set; }
}