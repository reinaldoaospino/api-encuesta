using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Interfaces.Application
{
    public interface IDescriptionGeneratorService
    {
        Task GenerateSurveysDescriptions(IEnumerable<AnswerSelected> answersSelected);
    }
}