using Domain.Entities;
using infraestructure.Entities;

namespace infraestructure.Interfaces
{
    public interface ISurveyEntityMapper
    {
        public SurveyEntity Map(Survey survey);
    }
}
