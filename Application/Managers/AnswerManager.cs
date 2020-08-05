using Domain.Entities;
using Domain.Exceptions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;

namespace Application.Managers
{
    public class AnswerManager : IAnswerManager
    {
        private readonly IAnswerRepository _asnwerRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IDescriptionGeneratorService _surveyVerificationService;

        public AnswerManager(IAnswerRepository asnwerRepository, ISurveyRepository surveyRepository,
            IDescriptionGeneratorService surveyVerificationService)
        {
            _asnwerRepository = asnwerRepository;
            _surveyRepository = surveyRepository;
            _surveyVerificationService = surveyVerificationService;
        }

        public async Task<IEnumerable<Answer>> Get()
        {
            return await _asnwerRepository.Get();
        }

        public async Task Create(Answer answer)
        {
            await _surveyVerificationService.GenerateSurveysDescriptions(answer.AnswerSelected);

            answer.Id = answer.GenerateGuid();

            await _asnwerRepository.Create(answer);
        }

        public async Task Update(Answer answer)
        {
            var existingAnswer = await _asnwerRepository.Get(answer.Id);

            var answerNoExists = existingAnswer == null;

            if (answerNoExists)
                throw new EntityNotFoundException<Answer>(answer.Id);

            existingAnswer.Update(answer);

            await _asnwerRepository.Update(existingAnswer);
        }

        public async Task Delete(string id)
        {
            await _asnwerRepository.Delete(id);
        }
    }
}