using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Interfaces.Application
{
    public interface IEmailManager
    {
        Task<IEnumerable<Email>> Get();
        Task Create(Email email);
    }
}
