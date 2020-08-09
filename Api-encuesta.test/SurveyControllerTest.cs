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
   public class SurveyControllerTest
    {
        private readonly Mock<ISurveyManager> _mockManager;
        private readonly Mock<IMapper> _mockMapper;

        private SurveyController _surveyController;

        public SurveyControllerTest()
        {
            _mockManager = new Mock<ISurveyManager>();
            _mockMapper = new Mock<IMapper>();
            _surveyController = new SurveyController(_mockManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task WhenGetSurvey_ThenGetSuccessful()
        {
            //?Given
            var surveyList = new List<Survey>();
            var expected = new List<SurveyModel>();

            _mockManager.Setup(x => x.Get())
                .ReturnsAsync(surveyList)
                .Verifiable();

            _mockMapper.Setup(x => x.Map<IEnumerable<SurveyModel>>(surveyList))
                .Returns(expected)
                .Verifiable();

            //?When
            var actual = await _surveyController.Get();

            //?Then
            Assert.IsAssignableFrom<OkObjectResult>(actual.Result);
            Assert.Equal(((OkObjectResult)actual.Result).Value, expected);

            _mockManager.Verify();
            _mockMapper.Verify();
        }

        [Fact]
        public async Task GivenSurvey_WhenCreate_ThenCreatedSuccessful()
        {
            //?Given
            var surveyModel = new SurveyModel();
            var survey = new Survey();

            _mockMapper.Setup(x => x.Map<Survey>(surveyModel))
                .Returns(survey)
                .Verifiable();

            _mockManager.Setup(x => x.Create(survey))
                .Verifiable();

            //?When 
            var result =  await _surveyController.Create(surveyModel);

            //?Then
            Assert.IsType<OkResult>(result);

            _mockMapper.Verify();
            _mockManager.Verify();
        }

        [Fact]
        public async Task GivenSurvey_WhenUpdate_ThenUpdateSuccessful()
        {
            //?Given
            var surveyModel = new SurveyModel();
            var survey = new Survey();

            _mockMapper.Setup(x => x.Map<Survey>(surveyModel))
                .Returns(survey)
                .Verifiable();

            _mockManager.Setup(x => x.Update(survey))
                .Verifiable();

            //?When 
            var result = await _surveyController.Update(surveyModel);

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
            var result = await _surveyController.Delete(id);

            //?Then
            Assert.IsType<OkResult>(result);

            _mockManager.Verify();
        }
    }
}
