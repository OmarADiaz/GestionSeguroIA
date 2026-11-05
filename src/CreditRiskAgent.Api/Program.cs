using CreditRiskAgent.Application.Common;
using CreditRiskAgent.Application.Common.Plugins;
using CreditRiskAgent.Application.Polizas.Queries.GetPolizaByNumero;
using CreditRiskAgent.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

//  Configurar Base de Datos (SQL Server)
builder.Services.AddDbContext<SegurosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<SegurosDbContext>());

//  Registrar MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IApplicationDbContext).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetPolizaByNumeroQueryHandler).Assembly));

// Configurar Semantic Kernel con Google Gemini (User Secrets + Endpoint OpenAI)
var geminiApiKey = builder.Configuration["GeminiSettings:ApiKey"]
    ?? throw new InvalidOperationException("API Key de Gemini no configurada.");

var geminiModelId = builder.Configuration["GeminiSettings:ModelId"]
    ?? throw new InvalidOperationException("Modelo Gemini no configurado.");

// Política de reintentos con Polly para códigos 503 / 429
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .OrResult(msg => ((int)msg.StatusCode) == 503)
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
        onRetry: (outcome, timespan, retryCount, context) =>
        {
            Console.WriteLine($"[Polly] Intento {retryCount} fallido. Reintentando en {timespan.TotalSeconds} segundos...");
        });

// Registrar el Kernel y el conector de Chat Completion de Gemini
builder.Services.AddKernel()
    .AddOpenAIChatCompletion(
        modelId: geminiModelId,
        apiKey: geminiApiKey,
        endpoint: new Uri("https://generativelanguage.googleapis.com/v1beta/openai/")
    );

// Registrar el Plugin de Seguros en el contenedor DI
builder.Services.AddTransient<SegurosPlugin>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//  Registrar la instancia del plugin en el Kernel global después de construir la app
// Esto permite resolver dependencias como IMediator que requiere SegurosPlugin
var kernel = app.Services.GetRequiredService<Kernel>();
var segurosPlugin = app.Services.GetRequiredService<SegurosPlugin>();
kernel.Plugins.AddFromObject(segurosPlugin, "SegurosPlugin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();