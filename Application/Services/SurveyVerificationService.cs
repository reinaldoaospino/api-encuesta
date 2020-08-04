using System.Linq;
using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;

namespace Application.Services
{
    public class SurveyVerificationService : ISurveyVerificationService
    {
        private readonly ISurveyRepository _surveyRepository;
        public SurveyVerificationService(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<bool> VerificateSurvey(IEnumerable<AnswerSelected> answersSelected)
        {

            foreach (var answer in answersSelected)
            {
                var survey = await _surveyRepository.Get(answer.IdQuestion);

                if (survey == null)
                    return false;
                if (!VerificateIdOptions(survey.Options, answer.IdOption))
                    return false;
            }

            return true;
        }

        private bool VerificateIdOptions(IEnumerable<SurveyOption> surveysOptions, string idOptionSelected)
        {
            var exist = surveysOptions.ToList().Find(x => x.Id == idOptionSelected);
            if (exist == null)
                return false;

            return true;
        }
    }
}