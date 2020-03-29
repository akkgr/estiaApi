using estiaApi.Models;
using estiaApi.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using System;

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

        public Tuple<long, List<Building>> Get(ListRequest param)
        {
            SortDefinition<Building> sortDefinition = new BsonDocument(param.SortValue[0], param.SortValue[1] == "ASC" ? 1 : -1);
            var query = _buildings.Find(param.FilterValue);
            var count = query.CountDocuments();
            var data = query.Sort(sortDefinition)
                .Skip(param.RangeValue[0])
                .Limit(param.RangeValue[1] + 1 - param.RangeValue[0])
                .ToList();

            return new Tuple<long, List<Building>>(count, data);
        }

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