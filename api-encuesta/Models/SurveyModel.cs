using System.Collections.Generic;

namespace api_encuesta.Models
{
    public class SurveyModel
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public IEnumerable<SurveyOptionModel> Options { get; set; }
    }
}