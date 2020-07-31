using Domain.Entities;
using Domain.Interfaces.Infraestructure;
using infraestructure.Interfaces;
using System.Threading.Tasks;

namespace infraestructure.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly IMongoService _service;
        private readonly string _tableName;

        public SurveyRepository(IMongoService service)
        {
            _service = service;
            _tableName = "Survey"; //tomarlo del config
        }

        public async Task Create(Survey survey)
        {
           await _service.Create(_tableName, survey);
        }
    }
}
