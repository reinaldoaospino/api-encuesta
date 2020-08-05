using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;

namespace Application.Managers
{
    public class EmailManager : IEmailManager
    {
        private readonly IEmailRepository _repository;

        public EmailManager(IEmailRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Email>> Get()
        {
            return await _repository.Get();
        }

        public async Task Create(Email email)
        {
            email.Id = email.GenerateGuid();
            email.GenerateCreationTime();

            await _repository.Create(email);
        }
    }
}