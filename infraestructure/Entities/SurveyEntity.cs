using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace infraestructure.Entities
{
    public class SurveyEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Question { get; set; }
        public IEnumerable<SurveyOptionEntity> Options { get; set; }
    }
}
