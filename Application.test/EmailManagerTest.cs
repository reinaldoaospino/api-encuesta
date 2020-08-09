using Moq;
using Xunit;
using System;
using System.Linq;
using Domain.Entities;
using Application.Managers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;

namespace Application.test
{
   public class EmailManagerTest
    {
        private Mock<IEmailRepository> _mockRepository;

        private IEmailManager _emailManager;

        public EmailManagerTest()
        {
            _mockRepository = new Mock<IEmailRepository>();
            _emailManager = new EmailManager(_mockRepository.Object);
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

            _mockRepository.Setup(x => x.Get())
                .ReturnsAsync(emailList)
                .Verifiable();

            //?When
            var result = await _emailManager.Get();

            //?Then
            Assert.NotNull(result);

            var actual = result.ToList().FirstOrDefault();
            var expected = emailList.FirstOrDefault();

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.UserName, actual.UserName);
            Assert.Equal(expected.CreationTime, actual.CreationTime);

            _mockRepository.Verify();
        }

        [Fact]
        public async Task GivenEmail_WhenCreate_ThenCreateSuccessful()
        {
            //?Given
            var email = new Email
            {
                Id = "1234",
                UserName = "reinaldo.ospino@gmail.com",
                CreationTime = new DateTime()
            };

            //?When
            await _emailManager.Create(email);

            //?Then
            _mockRepository.Verify(t => t.Create(It.Is<Email>(e => EmailIsWellCreated(e, email))),Times.Once);

        }

        private bool EmailIsWellCreated(Email created, Email email)
        {
            return created.Id == email.Id &&
                created.UserName == email.UserName &&
                created.CreationTime == email.CreationTime;
        }
    }
}