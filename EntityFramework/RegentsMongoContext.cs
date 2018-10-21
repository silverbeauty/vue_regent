using MongoDB.Driver;
using Regents.Models.Courses;
using Microsoft.Extensions.Options;
namespace Regents.EntityFramework
{

    public class RegentsMongoContext 
    {
        private readonly IMongoDatabase _database;

        public RegentsMongoContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _database = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Course> Courses => _database.GetCollection<Course>(nameof(Courses));

        public IMongoCollection<Unit> Units => _database.GetCollection<Unit>(nameof(Units));

        public IMongoCollection<Topic> Topics => _database.GetCollection<Topic>(nameof(Topics));

        
    }
        public class Settings
{
    public string ConnectionString { get; set; }
    public string Database { get; set; }
}
}