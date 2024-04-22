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

        public List<TripDTO> GetTrips()
        {
            return tripData.GetTrips().Select(trip => EntityDTO.EntityToDTO(trip)).ToList();
        }
    }
}
