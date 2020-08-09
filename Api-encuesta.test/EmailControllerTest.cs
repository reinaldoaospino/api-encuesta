using Moq;
using Xunit;
using AutoMapper;
using Domain.Entities;
using api_encuesta.Models;
using System.Threading.Tasks;
using api_encuesta.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Domain.Interfaces.Application;

namespace Api_encuesta.test
{
    public class EmailControllerTest
    {
        private readonly Mock<IEmailManager> _mockManager;
        private readonly Mock<IMapper> _mockMapper;

        private EmailController _emailController;

        public EmailControllerTest()
        {
            _mockManager = new Mock<IEmailManager>();
            _mockMapper = new Mock<IMapper>();

            _emailController = new EmailController(_mockManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task WhenGetEmails_ThenGetSuccessful()
        {
            //?Given
            var emails = new List<Email>();
            var expected = new List<EmailModel>();

            _mockManager.Setup(x => x.Get())
                .ReturnsAsync(emails)
                .Verifiable();

            _mockMapper.Setup(x => x.Map<IEnumerable<EmailModel>>(emails))
                .Returns(expected)
                .Verifiable();
                
            //?When
            var actual = await _emailController.Get();

            //?Then
            Assert.IsAssignableFrom<OkObjectResult>(actual.Result);
            Assert.Equal(((OkObjectResult)actual.Result).Value, expected);

            _mockManager.Verify();
            _mockMapper.Verify();
        }

        [Fact]
        public async Task GivenEmail_WhenCreate_ThenCreatedSuccessful()
        {
            //?Given
            var email = new Email();
            var emailModel = new EmailModel();

            _mockManager.Setup(x => x.Create(email))
                .Verifiable();

            _mockMapper.Setup(x => x.Map<Email>(emailModel))
                .Returns(email)
                .Verifiable();

            //?When
            var result = await _emailController.Create(emailModel);

            //?Then
            Assert.IsType<OkResult>(result);

            _mockManager.Verify();
            _mockMapper.Verify();
        }
    }
}