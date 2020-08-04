namespace api_encuesta.Models
{
    public class AnswerSelectedModel
    {
        public string IdQuestion { get; set; }
        public string QuestionDescription { get; set; }
        public string IdOption { get; set; }
        public string OptionDescription { get; set; }
    }
}