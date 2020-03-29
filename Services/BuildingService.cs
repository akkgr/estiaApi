using estiaApi.Models;
using estiaApi.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace estiaApi.Services
{
    public class BuildingService
    {
        private readonly IMongoCollection<Building> _buildings;

        public BuildingService(IEstiaDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _buildings = database.GetCollection<Building>(settings.BuildingsCollectionName);
        }

        public List<Building> Get() =>
            _buildings.Find(building => true).ToList();

        public Building Get(string id) =>
            _buildings.Find<Building>(building => building.Id == id).FirstOrDefault();

        public Building Create(Building building)
        {
            _buildings.InsertOne(building);
            return building;
        }

        public void Update(string id, Building building) =>
            _buildings.ReplaceOne(b => b.Id == id, building);

        public void Remove(Building building) =>
            _buildings.DeleteOne(b => b.Id == building.Id);

        public void Remove(string id) =>
            _buildings.DeleteOne(b => b.Id == id);
    }
}