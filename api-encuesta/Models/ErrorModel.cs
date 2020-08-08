namespace api_encuesta.Models
{
    public class ErrorModel
    {
        public ErrorModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}