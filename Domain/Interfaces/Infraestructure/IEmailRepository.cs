using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Interfaces.Infraestructure
{
    public interface IEmailRepository
    {
        Task<IEnumerable<Email>> Get();
        Task Create(Email email);
    }
}