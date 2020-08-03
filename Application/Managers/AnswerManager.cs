using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;

namespace Application.Managers
{
    public class AnswerManager : IAnswerManager
    {
        private readonly IAnswerRepository _repository;

        public AnswerManager(IAnswerRepository repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<Answer>> Get()
        {
            return await _repository.Get();
        }

        public async Task Create(Answer answer)
        {
            answer.Id = answer.GenerateGuid();

            await _repository.Create(answer);
        }

        public async Task Update(Answer answer)
        {
            await _repository.Update(answer);
        }

        public async Task Delete(string id)
        {
            await _repository.Delete(id);
        }
    }
}