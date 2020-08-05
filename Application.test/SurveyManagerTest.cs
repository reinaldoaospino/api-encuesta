using Moq;
using Xunit;
using System;
using System.Linq;
using Domain.Entities;
using Domain.Exceptions;
using Application.Managers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;

namespace Application.test
{
    public class SurveyManagerTest
    {
        private readonly Mock<ISurveyRepository> _mockRepository;

        private ISurveyManager _surveyManager;

        public SurveyManagerTest()
        {
            _mockRepository = new Mock<ISurveyRepository>();
            _surveyManager = new SurveyManager(_mockRepository.Object);
        }

        [Fact]
        public async Task WhenGetSurvey_ThenGetSuccessful()
        {
            //?Given
            var surveyList = new List<Survey>
            {
                new Survey
                {
                    Id = "1234",
                    Question = "This is a question",
                    Options = new List<SurveyOption>
                    {
                        new SurveyOption
                        {
                             Option = "This is an option"
                        }
                    }
                }
            };

            _mockRepository.Setup(x => x.Get())
                .ReturnsAsync(surveyList)
                .Verifiable();

            //?When 
            var result = await _surveyManager.Get();

            //?Then
            var expected = surveyList.FirstOrDefault();
            var actual = result.ToList().FirstOrDefault();

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Question, actual.Question);
            Assert.Equal(expected.Options.FirstOrDefault().Option, actual.Options.FirstOrDefault().Option);

            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GivenSurvey_WhenCreate_ThenCreateSuccessful()
        {
            //?Given
            var survey = GetSurvey();

            //?When
            await _surveyManager.Create(survey);

            //?Then
            _mockRepository.Verify(t => t.Create(It.Is<Survey>(e => MessageeIsWellCreated(e, survey))), Times.Once);
        }

        [Fact]
        public async Task GivenSurvey_WhenUpdate_ThenUpdateSuccessful()
        {
            //?Given
            var survey = GetSurvey();

            _mockRepository.Setup(x => x.Get(survey.Id))
                .ReturnsAsync(survey)
                .Verifiable();

            //?When
            await _surveyManager.Update(survey);

            //?Then
            _mockRepository.Verify(t => t.Update(It.Is<Survey>(e => MessageeIsWellUpdated(e, survey))), Times.Once);
        }

        [Fact]
        public async Task GivenSurvey_WhenUpdate_ThenThrowEntityNotFoundException()
        {
            //?Given
            var survey = GetSurvey();

            //?When
            Func<Task> update = () => _surveyManager.Update(survey);

            //?Then
            await Assert.ThrowsAsync<EntityNotFoundException<Survey>>(update);
        }

        [Fact]
        public async Task GivenId_WhenDelete_ThenDeleteSuccessful()
        {
            //?Given
            var id = "1234";

            _mockRepository.Setup(x => x.Delete(id))
                .Verifiable();

            //?When
            await _surveyManager.Delete(id);

            //?Then
            _mockRepository.Verify();
        }

        private Survey GetSurvey()
        {
            return new Survey
            {
                Id = "1234",
                Question = "This is a question",
                Options = new List<SurveyOption>
                    {
                        new SurveyOption
                        {
                             Option = "This is an option"
                        }
                    }
            };
        }

        private bool MessageeIsWellCreated(Survey created, Survey survey)
        {
            return created.Id == survey.Id &&
                created.Question == survey.Question &&
                created.Options.FirstOrDefault().Option == survey.Options.FirstOrDefault().Option;
        }

        private bool MessageeIsWellUpdated(Survey created, Survey survey)
        {
            return created.Id == survey.Id &&
                created.Question == survey.Question &&
                created.Options.FirstOrDefault().Option == survey.Options.FirstOrDefault().Option;
        }
    }
}