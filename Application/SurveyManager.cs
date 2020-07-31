using Domain.Abstations.Application;
using Domain.Abstations.Infraestructure;
using Domain.Entitys;
using System.Threading.Tasks;

namespace Application
{
    public class SurveyManager : ISurveyManager
    {
        private readonly IMongoDbServices _mongoDbServices;

        public SurveyManager(IMongoDbServices mongoDbServices)
        {
            _mongoDbServices = mongoDbServices;
        }

        public async Task Create(Survey obj)
        {
           await _mongoDbServices.Create("Survey", obj);
        }
    }
}
