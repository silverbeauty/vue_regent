using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Regents.ViewModels.Courses
{
    public class TopicVm
    {
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Description { get; set; }

        public string UnitId { get; set; }
    }
}