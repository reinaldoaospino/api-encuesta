using Application.Managers;
using Domain.Entities;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.test
{
    public class AnswerManagerTest
    {

        private readonly Mock<IAnswerRepository> _mockAnswerReposotory;
        private readonly Mock<ISurveyRepository> _mockSurveyRepository;
        private readonly Mock<IDescriptionGeneratorService> _mockDesctionGenerator;

        private IAnswerManager _answerManager;


        public AnswerManagerTest()
        {
            _mockAnswerReposotory = new Mock<IAnswerRepository>();
            _mockSurveyRepository = new Mock<ISurveyRepository>();
            _mockDesctionGenerator = new Mock<IDescriptionGeneratorService>();
            _answerManager = new AnswerManager(_mockAnswerReposotory.Object, _mockSurveyRepository.Object, _mockDesctionGenerator.Object);

        }

        [Fact]
        public async Task WhenGetAnswer_ThenGetSuccessful()
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

            _mockAnswerReposotory.Setup(x => x.Get())
                .ReturnsAsync(answerList);

            //?When 
            var result = await _answerManager.Get();

            //?Then
            var expected = answerList.FirstOrDefault();
            var actual = result.ToList().FirstOrDefault();
            
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.User, actual.User);
            Assert.Equal(expected.AnswerSelected.FirstOrDefault().IdOption, actual.AnswerSelected.FirstOrDefault().IdOption);
            Assert.Equal(expected.AnswerSelected.FirstOrDefault().OptionDescription, actual.AnswerSelected.FirstOrDefault().OptionDescription);
            Assert.Equal(expected.AnswerSelected.FirstOrDefault().IdQuestion, actual.AnswerSelected.FirstOrDefault().IdQuestion);
            Assert.Equal(expected.AnswerSelected.FirstOrDefault().QuestionDescription, actual.AnswerSelected.FirstOrDefault().QuestionDescription);

            _mockAnswerReposotory.Verify();
        }

        [Fact]
        public async Task GivenSurvey_WhenCreate_ThenCreateSuccessful()
        {
            //?Given
            var answer = GetAnswer();

            //?When
            await _answerManager.Create(answer);

            //?Then
            _mockAnswerReposotory.Verify(t => t.Create(It.Is<Answer>(e => AnswerIsWellCreated(e, answer))), Times.Once);
        }

        [Fact]
        public async Task GivenSurvey_WhenUpdate_ThenUpdateSuccessful()
        {
            //?Given
            var answer = GetAnswer();

            //?When
            await _answerManager.Update(answer);

            //?Then
            _mockAnswerReposotory.Verify(t => t.Update(It.Is<Answer>(e => AnswerIsWellUpdated(e, answer))), Times.Once);
        }


        [Fact]
        public async Task GivenId_WhenDelete_ThenDeleteSuccessful()
        {
            //?Given
            var id = "1234";

            _mockAnswerReposotory.Setup(x => x.Delete(id))
                .Verifiable();

            //?When
            await _answerManager.Delete(id);

            //?Then
            _mockAnswerReposotory.Verify();
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

        private bool AnswerIsWellCreated(Answer created, Answer answer)
        {
            return created.User == answer.User &&
                created.Id == answer.Id &&
                created.AnswerSelected.FirstOrDefault().IdOption == answer.AnswerSelected.FirstOrDefault().IdOption &&
                created.AnswerSelected.FirstOrDefault().OptionDescription == answer.AnswerSelected.FirstOrDefault().OptionDescription &&
                created.AnswerSelected.FirstOrDefault().IdQuestion == answer.AnswerSelected.FirstOrDefault().IdQuestion &&
                created.AnswerSelected.FirstOrDefault().QuestionDescription == answer.AnswerSelected.FirstOrDefault().QuestionDescription;
        }

        private bool AnswerIsWellUpdated(Answer created, Answer answer)
        {
            return created.User == answer.User &&
                created.Id == answer.Id &&
                created.AnswerSelected.FirstOrDefault().IdOption == answer.AnswerSelected.FirstOrDefault().IdOption &&
                created.AnswerSelected.FirstOrDefault().OptionDescription == answer.AnswerSelected.FirstOrDefault().OptionDescription &&
                created.AnswerSelected.FirstOrDefault().IdQuestion == answer.AnswerSelected.FirstOrDefault().IdQuestion &&
                created.AnswerSelected.FirstOrDefault().QuestionDescription == answer.AnswerSelected.FirstOrDefault().QuestionDescription;
        }
    }
}
