using System.Collections.Generic;

namespace api_encuesta.Models
{
    public class SurveyRequest
    {
        public string Question { get; set; }
        public IEnumerable<SurveyOptionModel> Options { get; set; }
    }
}