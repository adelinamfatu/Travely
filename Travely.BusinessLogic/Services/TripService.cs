using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.BusinessLogic.Converters;
using Travely.BusinessLogic.DTOs;
using Travely.Domain;
using Travely.Domain.CRUD;
using Travely.Domain.Entities;

namespace Travely.BusinessLogic.Services
{
    public class TripService
    {
        public TripData tripData;

        public TripService(AppDbContext context)
        {
            this.tripData = new TripData(context);
        }

        public void AddTrip(TripDTO trip)
        {
            tripData.AddTrip(DTOEntity.DTOtoEntity(trip));
        }

        public async Task<List<TripDTO>> GetTrips()
        {
            var trips = await tripData.GetTrips();
            return trips.Select(trip => EntityDTO.EntityToDTO(trip)).ToList();
        }

        public void DeleteTrip(Guid tripId)
        {
            tripData.DeleteTrip(tripId);
        }
    }
}
