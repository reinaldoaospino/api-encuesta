﻿using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Infraestructure.Entities
{
    public class SurveyEntity
    {
        [BsonId]
        public string Id { get; set; }
        public string Question { get; set; }
        public IEnumerable<SurveyOptionEntity> Options { get; set; }
    }
}