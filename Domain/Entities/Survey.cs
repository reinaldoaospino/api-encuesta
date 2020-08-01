using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Survey
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public IEnumerable<SurveyOption> Options { get; set; }

        public string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}