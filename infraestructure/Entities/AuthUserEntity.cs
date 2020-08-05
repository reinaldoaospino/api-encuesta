using MongoDB.Bson.Serialization.Attributes;

namespace Infraestructure.Entities
{
    public class AuthUserEntity
    {
        [BsonId]
        public string Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}