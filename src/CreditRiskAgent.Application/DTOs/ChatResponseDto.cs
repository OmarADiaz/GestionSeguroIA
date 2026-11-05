public class ChatResponseDto
{
    public string Pregunta { get; set; } = string.Empty;
    public string MensajeRespuesta { get; set; } = string.Empty;
    public List<SiniestroDto> SiniestrosDetectados { get; set; } = new();
}