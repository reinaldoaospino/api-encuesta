using AutoMapper;
using Domain.Entities;
using System.Threading.Tasks;
using infraestructure.Entities;
using System.Collections.Generic;
using infraestructure.Interfaces;
using Domain.Interfaces.Infraestructure;
using Microsoft.Extensions.Configuration;

namespace infraestructure.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly IMongoService _service;
        private readonly IMapper _mapper;

        private readonly string _tableName;

        public SurveyRepository(IMongoService service,
            IConfiguration configuration, IMapper mapper )
        {
            _service = service;
            _mapper = mapper;
            _tableName = configuration["AppSettings:surveyTable"];
        }

        public async Task<IEnumerable<Survey>> Get()
        {
            var surveyEntiity = await _service.Get<SurveyEntity>(_tableName);

            var survey = _mapper.Map<IEnumerable<Survey>>(surveyEntiity);

            return survey;
        }

        public async Task Create(Survey survey)
        {
            var surveyEntiity = _mapper.Map<SurveyEntity>(survey);

            await _service.Create(_tableName, surveyEntiity);
        }

        public async Task Update(Survey survey)
        {
            var surveyEntiity = _mapper.Map<SurveyEntity>(survey);

            await _service.Update(_tableName,surveyEntiity.Id, surveyEntiity);
        }

        public async Task Delete(string id)
        {
            await _service.Delete<SurveyEntity>(_tableName, id);
        }
    }
}