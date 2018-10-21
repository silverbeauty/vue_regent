using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Regents.Models.Courses
{
    public class Topic
    {
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Description { get; set; }

        public string UnitId { get; set; }
    }
}