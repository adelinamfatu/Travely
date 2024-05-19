using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public class EditTripViewModel : ObservableObject
    {
        private readonly TripService? tripService;

        public EditTripViewModel(TripService tripService)
        {
            this.tripService = tripService;
        }
    }
}
