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

        public void Update(Survey survey)
        {
            Question = survey.Question;

            foreach (var option in Options)
            {
                foreach (var surveyOption in survey.Options)
                {
                    if(option.Option != surveyOption.Option)
                    {
                        option.Id = survey.GenerateGuid();
                        option.Option = surveyOption.Option;
                    }
                }
            }

        }
    }
}