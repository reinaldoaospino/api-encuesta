using AutoMapper;
using Domain.Entities;
using System.Threading.Tasks;
using Infraestructure.Entities;
using System.Collections.Generic;
using Infraestructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces.Infraestructure;

namespace Infraestructure.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IMongoService _service;
        private readonly IMapper _mapper;

        private readonly string _collectionName;

        public EmailRepository(IMongoService service,
            IConfiguration configuration, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _collectionName = configuration["AppSettings:emailCollection"];
        }

        public async Task<IEnumerable<Email>> Get()
        {
            var emailEntity = await _service.Get<EmailEntity>(_collectionName);

            var email = _mapper.Map<IEnumerable<Email>>(emailEntity);

            return email;
        }

        public async Task Create(Email email)
        {
            var emailEntity = _mapper.Map<EmailEntity>(email);

            await _service.Create(_collectionName, emailEntity);
        }
    }
}