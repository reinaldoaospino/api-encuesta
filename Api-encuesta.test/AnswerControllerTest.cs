using Moq;
using Xunit;
using AutoMapper;
using Domain.Entities;
using api_encuesta.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api_encuesta.Controllers;
using System.Collections.Generic;
using Domain.Interfaces.Application;

namespace Api_encuesta.test
{
    public class AnswerControllerTest
    {

        private readonly Mock<IAnswerManager> _mockManager;
        private readonly Mock<IMapper> _mockMapper;

        private AnswerController _answerController;

        public AnswerControllerTest()
        {
            _mockManager = new Mock<IAnswerManager>();
            _mockMapper = new Mock<IMapper>();
            _answerController = new AnswerController(_mockManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task WhenGetAnswer_ThenGetSuccessful()
        {
            //?Given
            var answerList = new List<Answer>();
            var expected = new List<AnswerModel>();

            _mockManager.Setup(x => x.Get())
                .ReturnsAsync(answerList)
                .Verifiable();

            _mockMapper.Setup(x => x.Map<IEnumerable<AnswerModel>>(answerList))
                .Returns(expected)
                .Verifiable();

            //?When
            var actual = await _answerController.Get();

            //?Then
            Assert.IsAssignableFrom<OkObjectResult>(actual.Result);
            Assert.Equal(((OkObjectResult)actual.Result).Value, expected);

            _mockManager.Verify();
            _mockMapper.Verify();
        }

        [Fact]
        public async Task GivenAnswer_WhenCreate_ThenCreatedSuccessful()
        {
            //?Given
            var answerModel = new AnswerModel();
            var answer = new Answer();

            _mockMapper.Setup(x => x.Map<Answer>(answerModel))
                .Returns(answer)
                .Verifiable();

            _mockManager.Setup(x => x.Create(answer))
                .Verifiable();

            //?When 
            var result = await _answerController.Create(answerModel);

            //?Then
            Assert.IsType<OkResult>(result);

            _mockMapper.Verify();
            _mockManager.Verify();
        }

        [Fact]
        public async Task GivenAnswer_WhenUpdate_ThenCreatedSuccessful()
        {
            //?Given
            var answerModel = new AnswerModel();
            var answer = new Answer();

            _mockMapper.Setup(x => x.Map<Answer>(answerModel))
                .Returns(answer)
                .Verifiable();

            _mockManager.Setup(x => x.Update(answer))
                .Verifiable();

            //?When 
            var result = await _answerController.Update(answerModel);

            //?Then
            Assert.IsType<OkResult>(result);

            _mockMapper.Verify();
            _mockManager.Verify();
        }

        [Fact]
        public async Task GivenId_WhenDelete_ThenDeleteSuccessful()
        {
            //?Given
            var id = "1234";

            _mockManager.Setup(x => x.Delete(id))
                .Verifiable();


            //?When 
            var result = await _answerController.Delete(id);

            //?Then
            Assert.IsType<OkResult>(result);

            _mockManager.Verify();
        }
    }
}