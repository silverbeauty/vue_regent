using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Regents.Models.Courses
{
    public class Unit
    {
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public int Sequence { get; set; }

        public string Description { get; set; }

        public string CourseId { get; set; }
    }
}