using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;
using System.Linq;
using System;

namespace Application.Managers
{
    public class AnswerManager : IAnswerManager
    {
        private readonly IAnswerRepository _asnwerRepository;
        private readonly ISurveyRepository _surveyRepository;

        public AnswerManager(IAnswerRepository asnwerRepository, ISurveyRepository surveyRepository)
        {
            _asnwerRepository = asnwerRepository;
            _surveyRepository = surveyRepository;
        }


        public async Task<IEnumerable<Answer>> Get()
        {
            return await _asnwerRepository.Get();
        }

        public async Task Create(Answer answer)
        {
            if (!await VerificateIdSurvey(answer.AnswerSelected))
            {
                throw new ApplicationException();
            }
            answer.Id = answer.GenerateGuid();

            await _asnwerRepository.Create(answer);
        }

        public async Task Update(Answer answer)
        {
            await _asnwerRepository.Update(answer);
        }

        public async Task Delete(string id)
        {
            await _asnwerRepository.Delete(id);
        }

        private async Task<bool> VerificateIdSurvey(IEnumerable<AnswerSelected> answersSelected)
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