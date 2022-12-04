using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TravelApp.Data.DataConstants.DataConstants.JourneyConstants;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace TravelApp.Data.Entities
{
    public class Journey
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(JourneyMaxLengthTitle)]
        public string Title { get; set; } = null!;
        public IEnumerable<CountryJourney> CountriesJourneys { get; set; } = new List<CountryJourney>();
        public IEnumerable<Trip> Trips { get; set; } = new List<Trip>();
        [Required]
        [StringLength(JourneyMaxLengthDescription)]
        public string Description { get; set; } = null!;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        [Range(JourneyMinDays, JourneyMaxDays)]
        public int Days { get; set; }
        [Required]
        [Range(typeof(decimal), "0.00", "10000.00", ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }
        [Required]
        [Range(JourneyMinNumberPeople, JourneyMaxNumberPeople)]
        public int NumberOfPeople { get; set; }
        [Required]
        [StringLength(JourneyMaxLengthImage)]
        public string Image { get; set; } = null!;
        public List<ApplicationUserJourney> ApplicationUsersJourneys { get; set; } = new List<ApplicationUserJourney>();
        public IEnumerable<Request> Requests { get; set; } = new List<Request>();
    }
}
