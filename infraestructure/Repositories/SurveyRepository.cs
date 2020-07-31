using Domain.Entities;
using System.Threading.Tasks;
using infraestructure.Interfaces;
using Domain.Interfaces.Infraestructure;
using Microsoft.Extensions.Configuration;

namespace infraestructure.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly IMongoService _service;
        private readonly ISurveyEntityMapper _surveyMapper;
        private readonly string _tableName;

        public SurveyRepository(IMongoService service, ISurveyEntityMapper surveyMapper, IConfiguration configuration)
        {
            _service = service;
            _surveyMapper = surveyMapper;
            _tableName = configuration["AppSettings:surveyTable"];
        }

        public async Task Create(Survey survey)
        {
            var suveryEntity = _surveyMapper.Map(survey);

            await _service.Create(_tableName, suveryEntity);
        }
    }
}