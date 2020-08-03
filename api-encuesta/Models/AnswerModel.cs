using System.Collections.Generic;

namespace api_encuesta.Models
{
    public class AnswerModel
    {
        public string Id { get; set; }
        public string User { get; set; }
        public IEnumerable<AnswerSelectedModel> AnswerSelected { get; set; }
    }
}