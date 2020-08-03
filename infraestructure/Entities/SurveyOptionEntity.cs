using MongoDB.Bson.Serialization.Attributes;

namespace Infraestructure.Entities
{
    public class SurveyOptionEntity
    {
        [BsonId]
        public string Id { get; set; }
        public string Option { get; set; }
    }
}