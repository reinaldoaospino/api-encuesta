using Domain.Entities;
using infraestructure.Entities;
using infraestructure.Interfaces;
using System.Collections.Generic;

namespace infraestructure.Mappers
{
    public class SurveyEntityMapper : ISurveyEntityMapper
    {
        public SurveyEntity Map(Survey survey)
        {
            return new SurveyEntity
            {
                Question = survey.Question,
                Options = MapOptions(survey.Options)
            };
        }

        private IEnumerable<SurveyOptionEntity> MapOptions(IEnumerable<SurveyOption> options)
        {
            foreach (var option in options)
            {
                yield return new SurveyOptionEntity
                {
                    Option = option.Option
                };
            }
        }
    }
}