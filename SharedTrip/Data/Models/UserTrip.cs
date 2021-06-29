using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Data.Models
{
    public class UserTrip
    {
        public string UserId { get; init; }

        public User User { get; set; }

        public string TripId { get; init; }

        public Trip Trip { get; set; }
    }
}
