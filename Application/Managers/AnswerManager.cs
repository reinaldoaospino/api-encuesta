using System;
using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;

namespace Application.Managers
{
    public class AnswerManager : IAnswerManager
    {
        private readonly IAnswerRepository _asnwerRepository;
        private readonly ISurveyVerificationService _surveyVerificationService;

        public AnswerManager(IAnswerRepository asnwerRepository, ISurveyVerificationService surveyVerificationService)
        {
            _asnwerRepository = asnwerRepository;
            _surveyVerificationService = surveyVerificationService;
        }


        public async Task<IEnumerable<Answer>> Get()
        {
            return await _asnwerRepository.Get();
        }

        public async Task Create(Answer answer)
        {
            var verification = await _surveyVerificationService.VerificateSurvey(answer.AnswerSelected);

            if (!verification)
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
    }
}