using System.Collections.Generic;

namespace Domain.Entitys
{
    public class Survey
    {
        public string Question { get; set; }
        public IEnumerable<SurveyOption> Options { get; set; }
    }
}