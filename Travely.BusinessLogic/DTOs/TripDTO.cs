using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travely.BusinessLogic.DTOs
{
    public class TripDTO
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Country { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Notes { get; set; }
    }
}
