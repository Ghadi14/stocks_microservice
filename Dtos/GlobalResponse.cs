namespace WebApplication1.Dtos
{
    public class GlobalResponse
    {
        public StatusCode statusCode { get; set; } = new StatusCode();
    }
    public class StatusCode
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}
