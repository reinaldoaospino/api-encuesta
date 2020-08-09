using Moq;
using Xunit;
using System;
using AutoMapper;
using System.Linq;
using Domain.Entities;
using System.Threading.Tasks;
using Infraestructure.Entities;
using System.Collections.Generic;
using Infraestructure.Interfaces;
using Infraestructure.Repositories;
using Domain.Interfaces.Infraestructure;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.test
{
    public class EmailRepositoryTest
    {
        private readonly Mock<IMongoService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfiguration;

        private IEmailRepository _emailRepository;

        public EmailRepositoryTest()
        {
            _mockService = new Mock<IMongoService>();
            _mockMapper = new Mock<IMapper>();
            _mockConfiguration = new Mock<IConfiguration>();

            _emailRepository = new EmailRepository(_mockService.Object, _mockConfiguration.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task WhenGetEmails_ThenGetSuccessful()
        {
            //?Given
            var emailList = new List<Email>
            {
                  new Email
                  {    Id = "1234",
                       UserName = "reinaldo.ospino@gmail.com",
                       CreationTime = new DateTime()
                  }
            };

            var emailEntity = new List<EmailEntity>();

            _mockService.Setup(x => x.Get<EmailEntity>(It.IsAny<string>()))
                .ReturnsAsync(emailEntity)
                .Verifiable();

            _mockMapper.Setup(x => x.Map<IEnumerable<Email>>(It.IsAny<IEnumerable<EmailEntity>>()))
                .Returns(emailList)
                .Verifiable();

            //?When
            var result = await _emailRepository.Get();

            //?Then
            Assert.NotNull(result);

            var actual = result.ToList().FirstOrDefault();
            var expected = emailList.FirstOrDefault();

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.UserName, actual.UserName);
            Assert.Equal(expected.CreationTime, actual.CreationTime);

            _mockService.Verify();
            _mockMapper.Verify();
        }

        [Fact]
        public async Task GivenEmail_WhenCreate_ThenCreateSuccessful()
        {
            //?Given
            var id = "1234";
            var userName = "reinaldo.ospino@gmail.com";
            var creationTime = new DateTime();

            var email = new Email
            {
                Id = id,
                UserName = userName,
                CreationTime = creationTime
            };

            var emailEntity = new EmailEntity
            {
                Id = id,
                UserName = userName,
                CreationTime = creationTime
            };

            _mockMapper.Setup(x => x.Map<EmailEntity>(It.IsAny<Email>()))
                .Returns(emailEntity)
                .Verifiable();

            //?When
            await _emailRepository.Create(email);

            //?Then
            _mockMapper.Verify();
            _mockService.Verify();
            _mockService.Verify(t => t.Create(It.IsAny<string>(), It.Is<EmailEntity>(e => EmailIsWellCreated(e, email))), Times.Once);
        }

        private bool EmailIsWellCreated(EmailEntity entity, Email email)
        {
            return entity.Id == email.Id &&
                entity.UserName == email.UserName &&
                entity.CreationTime == email.CreationTime;
        }
    }
}