using estiaApi.Models;
using estiaApi.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace estiaApi.Services
{
    public class ApartmentService
    {
        private readonly IMongoCollection<Apartment> _apartments;
        private readonly CurrentUserService _currentUserService;

        public ApartmentService(IEstiaDatabaseSettings settings, CurrentUserService currentUserService, IMongoClient client)
        {
            _currentUserService = currentUserService;
            var database = client.GetDatabase(settings.DatabaseName);
            _apartments = database.GetCollection<Apartment>(settings.ApartmentsCollectionName);
        }

        public async Task<Tuple<long, List<Apartment>>> Get(ListRequest param, CancellationToken cancellationToken)
        {
            var query = _apartments.Find(param.FilterDefinition & Builders<Apartment>.Filter.Eq(t => t.Deleted, false));
            var count = query.CountDocuments();
            var data = await query.Sort(param.SortDefinition)
                .Skip(param.PagingInfo[0])
                .Limit(param.PagingInfo[1])
                .ToListAsync(cancellationToken);

            return new Tuple<long, List<Apartment>>(count, data);
        }

        public async Task<Apartment> Get(string id, CancellationToken cancellationToken) =>
            await _apartments.Find<Apartment>(a => a.Id == id).FirstOrDefaultAsync(cancellationToken);

        public async Task<Apartment> Create(Apartment building, CancellationToken cancellationToken)
        {
            building.CreatedBy = _currentUserService.Name;
            building.CreatedOn = DateTime.Now;
            await _apartments.InsertOneAsync(building, null, cancellationToken);
            return building;
        }

        public async Task<Apartment> Update(string id, Apartment building, CancellationToken cancellationToken)
        {
            building.UpdatedBy = _currentUserService.Name;
            building.UpdatedOn = DateTime.Now;
            ReplaceOptions options = null;
            await _apartments.ReplaceOneAsync(b => b.Id == id, building, options, cancellationToken);
            return building;
        }

        public async Task Remove(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<Apartment>.Filter.Eq(t => t.Id, id);
            var update = Builders<Apartment>.Update
                .Set(t => t.Deleted, true)
                .Set(t => t.DeletedBy, _currentUserService.Name)
                .Set(t => t.DeletedOn, DateTime.Now);
            await _apartments.UpdateOneAsync(filter, update, null, cancellationToken);
        }

        public async Task<Tuple<long, List<Apartment>>> GetByBuilding(string id, ListRequest param, CancellationToken cancellationToken)
        {
            var query = _apartments.Find(
                param.FilterDefinition &
                Builders<Apartment>.Filter.Eq(t => t.Deleted, false) &
                Builders<Apartment>.Filter.Eq(t => t.BuildingId, id));
            var count = query.CountDocuments();
            var data = await query.Sort(param.SortDefinition)
                .Skip(param.PagingInfo[0])
                .Limit(param.PagingInfo[1])
                .ToListAsync(cancellationToken);

            return new Tuple<long, List<Apartment>>(count, data);
        }
    }
}