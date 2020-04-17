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
        private readonly CurrentUserService _currentUserService;

        public BuildingService(IEstiaDatabaseSettings settings, CurrentUserService currentUserService, IMongoClient client)
        {
            _currentUserService = currentUserService;
            var database = client.GetDatabase(settings.DatabaseName);
            _buildings = database.GetCollection<Building>(settings.BuildingsCollectionName);
        }

        public async Task<Tuple<long, List<Building>>> Get(ListRequest param, CancellationToken cancellationToken)
        {
            var query = _buildings.Find(param.FilterDefinition & Builders<Building>.Filter.Eq(t => t.Deleted, false));
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
            building.CreatedBy = _currentUserService.Name;
            building.CreatedOn = DateTime.Now;
            await _buildings.InsertOneAsync(building, null, cancellationToken);
            return building;
        }

        public async Task<Building> Update(string id, Building building, CancellationToken cancellationToken)
        {
            building.UpdatedBy = _currentUserService.Name;
            building.UpdatedOn = DateTime.Now;
            ReplaceOptions options = null;
            await _buildings.ReplaceOneAsync(b => b.Id == id, building, options, cancellationToken);
            return building;
        }

        public async Task Remove(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<Building>.Filter.Eq(t => t.Id, id);
            var update = Builders<Building>.Update
                .Set(t => t.Deleted, true)
                .Set(t => t.DeletedBy, _currentUserService.Name)
                .Set(t => t.DeletedOn, DateTime.Now);
            await _buildings.UpdateOneAsync(filter, update, null, cancellationToken);
        }
    }
}