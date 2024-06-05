using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;
using System.Collections.ObjectModel;
using static Travely.Client.Utilities.Messenger;

namespace Travely.Client.Models
{
    public partial class ItineraryViewModel : ObservableObject
    {
        private readonly TripDetailService? tripDetailService;

        [ObservableProperty]
        private Guid tripId;

        [ObservableProperty]
        private string? currentSpotName;

        [ObservableProperty]
        private ObservableCollection<DayItinerary> itinerary;

        private Dictionary<string, DateTime> tripDaysDates;

        private double? currentLongitude;

        private double? currentLatitude;

        private string? currentAddress;

        public ItineraryViewModel(TripDetailService tripDetailService)
        {
            this.tripDetailService = tripDetailService;
            Itinerary = new ObservableCollection<DayItinerary>();
            tripDaysDates = new Dictionary<string, DateTime>();
        }

        public async Task LoadItinerary(Guid tripId)
        {
            this.TripId = tripId;
            Itinerary.Clear();

            if (tripDetailService is not null)
            {
                var tripSpots = await tripDetailService.GetTripSpots(tripId);
                var spotsByDay = tripSpots.GroupBy(spot => spot.Date.Date).ToDictionary(g => g.Key, g => g.ToList());

                var tripDays = await tripDetailService.GetTripDays(tripId);
                int dayCount = 1;

                foreach (var day in tripDays)
                {
                    string dayTitle = $"Day {dayCount} - {day:dd.MM.yyyy}";

                    var dayItinerary = new DayItinerary(dayTitle, new ObservableCollection<SpotDTO>());

                    tripDaysDates[dayTitle] = day;

                    if (spotsByDay.TryGetValue(day.Date, out var spots))
                    {
                        foreach (var spot in spots)
                        {
                            if (spot is not null)
                            {
                                dayItinerary.Spots.Add(spot);
                            }
                        }
                    }

                    Itinerary.Add(dayItinerary);
                    dayCount++;
                }
            }
        }

        public void AddSpot(string dayTitle)
        {
            if (tripDetailService is not null && CurrentSpotName is not null)
            {
                if (tripDaysDates.TryGetValue(dayTitle, out DateTime day))
                {
                    var dayItinerary = Itinerary.FirstOrDefault(it => it.DayTitle == dayTitle);
                    if (dayItinerary != null)
                    {
                        tripDetailService.AddSpot(new SpotDTO
                        {
                            Name = CurrentSpotName,
                            Latitude = currentLatitude,
                            Longitude = currentLongitude,
                            Address = currentAddress,
                            Date = day
                        }, TripId);

                        WeakReferenceMessenger.Default.Send(new ReloadSpotsMessage());
                    }
                }
            }
        }

        [RelayCommand]
        public void DeleteSpot(SpotDTO spot)
        {
            if (tripDetailService != null)
            {
                tripDetailService.DeleteSpot(spot.Id);
                WeakReferenceMessenger.Default.Send(new ReloadSpotsMessage());
            }
        }

        [RelayCommand]
        public void UpdateSpotTime(SpotDTO spot)
        {
            if (tripDetailService != null)
            {
                tripDetailService.UpdateSpotTime(spot.Id, spot.Time);
            }
        }

        [RelayCommand]
        public void UpdateSpotFee(SpotDTO spot)
        {
            if (tripDetailService != null && spot.EntryFee != null)
            {
                decimal entryFee = spot.EntryFee ?? 0m;
                tripDetailService.UpdateSpotFee(spot.Id, entryFee);
            }
        }

        public async void GetSpotData(double latitude, double longitude)
        {
            if (tripDetailService is not null)
            {
                this.currentLongitude = longitude;
                this.currentLatitude = longitude;
                this.currentAddress = await tripDetailService.GetSpotDetails(latitude, longitude);
                this.CurrentSpotName = currentAddress.Split(',')[0];
            }
        }
    }

    public partial class DayItinerary : ObservableObject
    {
        public DayItinerary(string dayTitle, ObservableCollection<SpotDTO> spots)
        {
            DayTitle = dayTitle;
            Spots = spots;
        }

        public string DayTitle { get; }

        [ObservableProperty]
        private ObservableCollection<SpotDTO> spots;
    }
}