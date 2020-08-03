using AutoMapper;
using Domain.Entities;
using System.Threading.Tasks;
using Infraestructure.Entities;
using Infraestructure.Interfaces;
using System.Collections.Generic;
using Domain.Interfaces.Infraestructure;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly IMongoService _service;
        private readonly IMapper _mapper;

        private readonly string _collectionName;

        public AnswerRepository(IMongoService service,
             IConfiguration configuration, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _collectionName = configuration["AppSettings:answerCollection"];
        }

        public async Task<IEnumerable<Answer>> Get()
        {
            var answerEntity = await _service.Get<AnswerEntity>(_collectionName);

            var answer = _mapper.Map<IEnumerable<Answer>>(answerEntity);

            return answer;
        }

        public async Task Create(Answer answer)
        {
            var answerEntity = _mapper.Map<AnswerEntity>(answer);

            await _service.Create(_collectionName, answerEntity);
        }

        public async Task Update(Answer answer)
        {
            var answerEntity = _mapper.Map<AnswerEntity>(answer);

            await _service.Update(_collectionName, answerEntity.Id, answerEntity);
        }

        public async Task Delete(string id)
        {
            await _service.Delete<AnswerEntity>(_collectionName, id);
        }


    }
}
