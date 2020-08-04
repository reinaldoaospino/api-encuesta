using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Interfaces.Application
{
    public interface ISurveyVerificationService
    {
        Task<bool> VerificateSurvey(IEnumerable<AnswerSelected> answersSelected);
    }
}