using System.Collections.Generic;

namespace Domain.Entities
{
    public class Survey
    {
        public string Question { get; set; }
        public IEnumerable<SurveyOption> Options { get; set; }
    }
}