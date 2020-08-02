using Moq;
using Xunit;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using Infraestructure.Entities;
using System.Collections.Generic;
using Infraestructure.Interfaces;

namespace Infraestructure.test
{
    public class MongoServiceTest
    {
        private readonly Mock<IMongoDatabase> _mockDb;
        private readonly Mock<IMongoClient> _mockClient;
        private readonly Mock<IMongoCollection<SurveyEntity>> _mockCollection;

        private IMongoService _mongoService;

        public MongoServiceTest()
        {
            _mockDb = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
            _mockCollection = new Mock<IMongoCollection<SurveyEntity>>();
            _mockClient.Setup(stub => stub.GetDatabase(It.IsAny<string>(), It.IsAny<MongoDatabaseSettings>())).Returns(_mockDb.Object);
            _mongoService = new MongoService("dataBase", _mockClient.Object);
        }

        [Fact]
        public async Task GivenCollectionName_WhenGetCollection_ThenReturnCollections()
        {
            //?When

            var expect = new List<SurveyEntity>();

            var collectionName = "Survey";
            var findCollection = Mock.Of<IAsyncCursor<SurveyEntity>>();
            var collectionMock = Mock.Of<IMongoCollection<SurveyEntity>>();

            SetupGetCollections(collectionName);

            Mock.Get(findCollection).Setup(x => x.Current)
                .Returns(expect)
                .Verifiable();

            _mockCollection.Setup(x => x.FindAsync<SurveyEntity>(It.IsAny<FilterDefinition<SurveyEntity>>(), null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(findCollection)
                .Verifiable();

            //?Given
            var result = await _mongoService.Get<SurveyEntity>(collectionName);

            //?Then
            Assert.NotNull(result);
            _mockDb.VerifyAll();
            _mockCollection.VerifyAll();
        }
        [Fact]
        public async Task GivenCollectionNameAndRecord_WhenCreate_ThenCreateSuccessful()
        {
            //?Given
            var collectionName = "Survey";
            var record = new SurveyEntity();

            SetupGetCollections(collectionName);

            _mockCollection.Setup(x => x.InsertOneAsync(record, null, It.IsAny<CancellationToken>()))
                .Verifiable();

            //?When
            await _mongoService.Create(collectionName, record);

            //?Then
            _mockDb.VerifyAll();
            _mockCollection.VerifyAll();
        }

        [Fact]
        public async Task GivenCollectionNameTheIdAndRecord_WhenUpdate_ThenUpdateSuccessful()
        {
            //?Given
            var collectionName = "Survey";
            var id = "1234";
            var record = new SurveyEntity();

            SetupGetCollections(collectionName);

            _mockCollection.Setup(x => x.ReplaceOneAsync(It.IsAny<FilterDefinition<SurveyEntity>>(), record, It.IsAny<ReplaceOptions>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            //?When
            await _mongoService.Update(collectionName, id, record);

            //?Then
            _mockClient.VerifyAll();
            _mockCollection.VerifyAll();
        }

        [Fact]
        public async Task GivenCollectionNameAndId_WhenDelete_ThenDeleteSuccessful()
        {
            //?Given
            var collectionName = "Survey";
            var id = "1234";
            var record = new SurveyEntity();

            SetupGetCollections(collectionName);

            _mockCollection.Setup(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<SurveyEntity>>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            //?When
            await _mongoService.Delete<SurveyEntity>(collectionName, id);

            //?Then
            _mockClient.VerifyAll();
            _mockCollection.VerifyAll();
        }

        private void SetupGetCollections(string collectionName)
        {
            _mockDb.Setup(x => x.GetCollection<SurveyEntity>(collectionName, null))
                .Returns(_mockCollection.Object)
                .Verifiable();
        }
    }
}