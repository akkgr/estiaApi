using estiaApi.Models;
using estiaApi.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<Tuple<long, List<Building>>> Get(ListRequest param, CancellationToken cancellationToken)
        {
            var query = _buildings.Find(param.FilterDefinition);
            var count = query.CountDocuments();
            var data = await query.Sort(param.SortDefinition)
                .Skip(param.PagingInfo[0])
                .Limit(param.PagingInfo[1])
                .ToListAsync(cancellationToken);

            return new Tuple<long, List<Building>>(count, data);
        }

        public async Task<Building> Get(string id, CancellationToken cancellationToken) =>
            await _buildings.Find<Building>(building => building.Id == id).FirstOrDefaultAsync(cancellationToken);

        public async Task<Building> Create(Building building, CancellationToken cancellationToken)
        {
            await _buildings.InsertOneAsync(building, null, cancellationToken);
            return building;
        }

        public async Task Update(string id, Building building, CancellationToken cancellationToken)
        {
            ReplaceOptions options = null;
            await _buildings.ReplaceOneAsync(b => b.Id == id, building, options, cancellationToken);
        }

        public async Task Remove(Building building, CancellationToken cancellationToken) =>
            await _buildings.DeleteOneAsync(b => b.Id == building.Id, cancellationToken);

        public async Task Remove(string id, CancellationToken cancellationToken) =>
            await _buildings.DeleteOneAsync(b => b.Id == id, cancellationToken);
    }
}