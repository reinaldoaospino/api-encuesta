using Moq;
using Xunit;
using AutoMapper;
using System.Linq;
using Domain.Entities;
using System.Threading.Tasks;
using Infraestructure.Entities;
using Infraestructure.Interfaces;
using System.Collections.Generic;
using Infraestructure.Repositories;
using Domain.Interfaces.Infraestructure;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.test
{
    public class SurveyRepositoryTest
    {
        private readonly Mock<IMongoService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfiguration;

        private ISurveyRepository _surveyRepository;

        public SurveyRepositoryTest()
        {
            _mockService = new Mock<IMongoService>();
            _mockMapper = new Mock<IMapper>();
            _mockConfiguration = new Mock<IConfiguration>();
            _surveyRepository = new SurveyRepository(_mockService.Object, _mockConfiguration.Object, _mockMapper.Object);
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

            var surveyEntity = new List<SurveyEntity>();

            _mockService.Setup(x => x.Get<SurveyEntity>(It.IsAny<string>()))
                .ReturnsAsync(surveyEntity)
                .Verifiable();

            _mockMapper.Setup(x => x.Map<IEnumerable<Survey>>(It.IsAny<IEnumerable<SurveyEntity>>()))
                .Returns(surveyList)
                .Verifiable();

            //?When
            var result = await _surveyRepository.Get();

            //?Then
            Assert.NotNull(result);

            var actual = result.ToList().FirstOrDefault();
            var expect = surveyList.FirstOrDefault();

            Assert.Equal(expect.Id, actual.Id);
            Assert.Equal(expect.Question, actual.Question);
            Assert.Equal(expect.Options.FirstOrDefault().Option, actual.Options.FirstOrDefault().Option);

            _mockService.Verify();
            _mockMapper.Verify();
        }

        [Fact]
        public async Task GivenSurvey_WhenCreate_ThenCreateSuccessful()
        {
            //?Given
            var survey = GetSurvey();

            var surveyEntity = GetSurveyEntity();

            _mockMapper.Setup(x => x.Map<SurveyEntity>(It.IsAny<Survey>()))
                .Returns(surveyEntity)
                .Verifiable();

            //?When
            await _surveyRepository.Create(survey);

            //?Then
            _mockMapper.Verify();
            _mockService.Verify();
            _mockService.Verify(t => t.Create(It.IsAny<string>(), It.Is<SurveyEntity>(e => MessageeIsWellCreated(e, survey))), Times.Once);
        }

        [Fact]
        public async Task GivenSurvey_WhenUpdate_ThenUpdateSuccessful()
        {
            //?Given
            var survey = GetSurvey();

            var surveyEntity = GetSurveyEntity();

            _mockMapper.Setup(x => x.Map<SurveyEntity>(It.IsAny<Survey>()))
                .Returns(surveyEntity)
                .Verifiable();

            //?When
            await _surveyRepository.Update(survey);

            //?Then
            _mockMapper.Verify();
            _mockService.Verify();
            _mockService.Verify(t => t.Update(It.IsAny<string>(), It.IsAny<string>(), It.Is<SurveyEntity>(e => MessageeIsWellUpdated(e, survey))), Times.Once);
        }

        [Fact]
        public async Task GivenId_WhenDelete_ThenDeleteSuccessful()
        {
            //?Given
            var id = "1234";

            _mockService.Setup(x => x.Delete<SurveyEntity>(It.IsAny<string>(), id))
                .Verifiable();

            //?When
            await _surveyRepository.Delete(id);

            //?THen
            _mockService.Verify();
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

        private SurveyEntity GetSurveyEntity()
        {
            return new SurveyEntity
            {
                Id = "1234",
                Question = "This is a question",
                Options = new List<SurveyOptionEntity>
                     {
                         new SurveyOptionEntity
                         {
                              Option = "This is an option"
                         }
                     }
            };
        }

        private bool MessageeIsWellCreated(SurveyEntity entity, Survey survey)
        {
            return entity.Id == survey.Id &&
                entity.Question == survey.Question &&
                entity.Options.FirstOrDefault().Option == survey.Options.FirstOrDefault().Option;
        }

        private bool MessageeIsWellUpdated(SurveyEntity entity, Survey survey)
        {
            return entity.Id == survey.Id &&
                entity.Question == survey.Question &&
                entity.Options.FirstOrDefault().Option == survey.Options.FirstOrDefault().Option;
        }
    }
}