using System.Linq;
using Domain.Entities;
using Domain.Exceptions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;

namespace Application.Services
{
    public class DescriptionGeneratorService : IDescriptionGeneratorService
    {
        private readonly ISurveyRepository _surveyRepository;
        public DescriptionGeneratorService(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task GenerateSurveysDescriptions(IEnumerable<AnswerSelected> answersSelected)
        {
            foreach (var answer in answersSelected)
            {
                var survey = await _surveyRepository.Get(answer.IdQuestion);

                if (survey == null)
                    throw new EntityNotFoundException<Answer>(answer.IdQuestion);

                answer.QuestionDescription = survey.Question;

                GenerateOptionsDescriptions(survey.Options, answer);
            }

        }

        private void GenerateOptionsDescriptions(IEnumerable<SurveyOption> surveysOptions, AnswerSelected answerSelected)
        {
            var exist = surveysOptions.ToList().Find(x => x.Id == answerSelected.IdOption);
            if (exist == null)
                throw new EntityNotFoundException<SurveyOption>(answerSelected.IdOption);

            answerSelected.OptionDescription = exist.Option;
        }
    }
}