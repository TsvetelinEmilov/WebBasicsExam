namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Data;
    using SharedTrip.Data.Models;
    using SharedTrip.Models.Trips;
    using SharedTrip.Services;
    using System;
    using System.Globalization;
    using System.Linq;

    using static Data.DataConstants;

    public class TripsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IValidator validator;

        public TripsController
            (ApplicationDbContext data,
            IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var trips = this.data.Trips
                .Select(t => new TripsListingViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString(DateFormat),
                    Seats = t.Seats,
                })
                .ToList();

            return this.View(trips);
        }
        [Authorize]
        public HttpResponse Add() => View();

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddTripModel model)
        {
            var modelErrors = this.validator.ValidateTrip(model);

            if (modelErrors.Any())
            {
                return this.Redirect("/Trips/Add");
            }

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                Description = model.Description,
                ImagePath = model.ImagePath,
                DepartureTime = DateTime.ParseExact(model.DepartureTime, DateFormat, CultureInfo.InvariantCulture),
                Seats = model.Seats,
            };

            this.data.Trips.Add(trip);
            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }
        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var trip = this.data.Trips
                .First(t => t.Id == tripId);

            return this.View(trip);
        }

        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            var trip = this.data.Trips
                .First(t => t.Id == tripId);

            var userId = this.User.Id;

            if (this.data.UserTrips.Any(ut => ut.UserId == userId && ut.TripId == trip.Id) || trip.Seats - 1 < 0)
            {
                return this.Redirect($"/Trips/Details?tripId={tripId} ");
            }

            var userTrip = new UserTrip
            {
                UserId = userId,
                TripId = trip.Id
            };

            this.data.UserTrips.Add(userTrip);
            this.data.SaveChanges();

            trip.Seats--;
            this.data.SaveChanges();

            return this.Redirect("/Trips/All");
        }




    }
}
