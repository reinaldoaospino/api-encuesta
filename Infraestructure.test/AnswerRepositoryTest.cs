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
    public class AnswerRepositoryTest
    {
        private readonly Mock<IMongoService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfiguration;

        private IAnswerRepository _answerRepository;

        public AnswerRepositoryTest()
        {
            _mockService = new Mock<IMongoService>();
            _mockMapper = new Mock<IMapper>();
            _mockConfiguration = new Mock<IConfiguration>();
            _answerRepository = new AnswerRepository(_mockService.Object, _mockConfiguration.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task WhenGetAnswers_ThenGetSuccessful()
        {
            //?Given
            var answerList = new List<Answer>
            {
                new Answer
                {
                    User = "reinaldo",
                    Id = "1234",
                    AnswerSelected = new List<AnswerSelected>
                    {
                       new AnswerSelected
                       {
                            IdOption = "1234",
                            OptionDescription="this is a description",
                            IdQuestion = "1234",
                            QuestionDescription = "this is a decription"
                       }
                    }
                }
            };

            var answerEntity = new List<AnswerEntity>();

            _mockService.Setup(x => x.Get<AnswerEntity>(It.IsAny<string>()))
                .ReturnsAsync(answerEntity)
                .Verifiable();

            _mockMapper.Setup(x => x.Map<IEnumerable<Answer>>(It.IsAny<IEnumerable<AnswerEntity>>()))
                .Returns(answerList)
                .Verifiable();

            //?When
            var result = await _answerRepository.Get();

            //?Then
            Assert.NotNull(result);

            var actual = result.ToList().FirstOrDefault();
            var expect = answerList.FirstOrDefault();

            Assert.Equal(expect.Id, actual.Id);
            Assert.Equal(expect.User, actual.User);
            Assert.Equal(expect.AnswerSelected.FirstOrDefault().IdOption, actual.AnswerSelected.FirstOrDefault().IdOption);
            Assert.Equal(expect.AnswerSelected.FirstOrDefault().OptionDescription, actual.AnswerSelected.FirstOrDefault().OptionDescription);
            Assert.Equal(expect.AnswerSelected.FirstOrDefault().IdQuestion, actual.AnswerSelected.FirstOrDefault().IdQuestion);
            Assert.Equal(expect.AnswerSelected.FirstOrDefault().QuestionDescription, actual.AnswerSelected.FirstOrDefault().QuestionDescription);
            _mockService.Verify();
            _mockMapper.Verify();
        }

        [Fact]
        public async Task GivenAnswer_WhenCreate_ThenCreateSuccessful()
        {
            //?Given
            var answer = GetAnswer();

            var answerEntity = GetAnswerEntity();

            _mockMapper.Setup(x => x.Map<AnswerEntity>(It.IsAny<Answer>()))
                .Returns(answerEntity)
                .Verifiable();

            //?When
            await _answerRepository.Create(answer);

            //?Then
            _mockMapper.Verify();
            _mockService.Verify();
            _mockService.Verify(t => t.Create(It.IsAny<string>(), It.Is<AnswerEntity>(e => AnswerIsWellCreated(e, answer))), Times.Once);
        }

        [Fact]
        public async Task GivenAnswer_WhenUpdate_ThenUpdateSuccessful()
        {
            //?Given
            var answer = GetAnswer();

            var answerEntity = GetAnswerEntity();

            _mockMapper.Setup(x => x.Map<AnswerEntity>(It.IsAny<Answer>()))
                .Returns(answerEntity)
                .Verifiable();

            //?When
            await _answerRepository.Update(answer);

            //?Then
            _mockMapper.Verify();
            _mockService.Verify();
            _mockService.Verify(t => t.Update(It.IsAny<string>(), It.IsAny<string>(), It.Is<AnswerEntity>(e => AnswerIsWellUpdated(e, answer))), Times.Once);
        }

        [Fact]
        public async Task GivenId_WhenDelete_ThenDeleteSuccessful()
        {
            //?Given
            var id = "1234";

            _mockService.Setup(x => x.Delete<AnswerEntity>(It.IsAny<string>(), id))
                .Verifiable();

            //?When
            await _answerRepository.Delete(id);

            //?THen
            _mockService.Verify();
        }

        private Answer GetAnswer()
        {
            return new Answer
            {
                User = "reinaldo",
                Id = "1234",
                AnswerSelected = new List<AnswerSelected>
                    {
                       new AnswerSelected
                       {
                            IdOption = "1234",
                            OptionDescription="this is a description",
                            IdQuestion = "1234",
                            QuestionDescription = "this is a decription"
                       }
                    }
            };
        }

        private AnswerEntity GetAnswerEntity()
        {
            return new AnswerEntity
            {
                User = "reinaldo",
                Id = "1234",
                AnswerSelected = new List<AnswerSelectedEntity>
                    {
                       new AnswerSelectedEntity
                       {
                            IdOption = "1234",
                            OptionDescription="this is a description",
                            IdQuestion = "1234",
                            QuestionDescription = "this is a decription"
                       }
                    }
            };
        }

        private bool AnswerIsWellCreated(AnswerEntity entity, Answer answer)
        {
            return entity.User == answer.User &&
                entity.Id == answer.Id &&
                entity.AnswerSelected.FirstOrDefault().IdOption == answer.AnswerSelected.FirstOrDefault().IdOption &&
                entity.AnswerSelected.FirstOrDefault().OptionDescription == answer.AnswerSelected.FirstOrDefault().OptionDescription &&
                entity.AnswerSelected.FirstOrDefault().IdQuestion == answer.AnswerSelected.FirstOrDefault().IdQuestion &&
                entity.AnswerSelected.FirstOrDefault().QuestionDescription == answer.AnswerSelected.FirstOrDefault().QuestionDescription;
        }

        private bool AnswerIsWellUpdated(AnswerEntity entity, Answer answer)
        {
            return entity.User == answer.User &&
                entity.Id == answer.Id &&
                entity.AnswerSelected.FirstOrDefault().IdOption == answer.AnswerSelected.FirstOrDefault().IdOption &&
                entity.AnswerSelected.FirstOrDefault().OptionDescription == answer.AnswerSelected.FirstOrDefault().OptionDescription &&
                entity.AnswerSelected.FirstOrDefault().IdQuestion == answer.AnswerSelected.FirstOrDefault().IdQuestion &&
                entity.AnswerSelected.FirstOrDefault().QuestionDescription == answer.AnswerSelected.FirstOrDefault().QuestionDescription;
        }
    }
}