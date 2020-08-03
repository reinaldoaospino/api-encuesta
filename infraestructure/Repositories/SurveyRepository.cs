using AutoMapper;
using Domain.Entities;
using System.Threading.Tasks;
using Infraestructure.Entities;
using System.Collections.Generic;
using Infraestructure.Interfaces;
using Domain.Interfaces.Infraestructure;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly IMongoService _service;
        private readonly IMapper _mapper;

        private readonly string _collectionName;

        public SurveyRepository(IMongoService service,
            IConfiguration configuration, IMapper mapper )
        {
            _service = service;
            _mapper = mapper;
            _collectionName = configuration["AppSettings:surveyCollection"];
        }

        public async Task<IEnumerable<Survey>> Get()
        {
            var surveyEntiity = await _service.Get<SurveyEntity>(_collectionName);

            var survey = _mapper.Map<IEnumerable<Survey>>(surveyEntiity);

            return survey;
        }

        public async Task Create(Survey survey)
        {
            var surveyEntiity = _mapper.Map<SurveyEntity>(survey);

            await _service.Create(_collectionName, surveyEntiity);
        }

        public async Task Update(Survey survey)
        {
            var surveyEntiity = _mapper.Map<SurveyEntity>(survey);

            await _service.Update(_collectionName, surveyEntiity.Id, surveyEntiity);
        }

        public async Task Delete(string id)
        {
            await _service.Delete<SurveyEntity>(_collectionName, id);
        }
    }
}