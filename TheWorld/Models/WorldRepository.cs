using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddOrganization(Organization newOrganization)
        {
            _context.Add(newOrganization);
        }

        public void AddStop(string tripName, Stop newStop, string username)
        {
            var trip = GetUserTripByName(tripName, username);

            if(trip != null)
            {
                trip.Stops.Add(newStop);
                _context.Stops.Add(newStop);
            }
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public IEnumerable<Organization> GetAllOrganizations()
        {
            _logger.LogInformation("Getting All Organizations from the Database");

            return _context.Organizations.ToList();
        }

        public Organization GetOrganizationsById(int id)
        {
            _logger.LogInformation("Getting Organizations from the Database By Id");

            return _context.Organizations
                .Where( t => t.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<JobTitle> GetAllJobTitles()
        {
            _logger.LogInformation("Getting All JobTitles from the Database");

            return _context.JobTitles.ToList();
        }

        public void AddJobTitle(JobTitle jobTitle)
        {
            _context.Add(jobTitle);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All Trips from the Database");

            return _context.Trips.ToList();
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .ThenInclude(s => s.Organization)
                .Include(t => t.Stops)
                .ThenInclude(s => s.JobTitle)
                .Where(t => t.Name == tripName)
                .FirstOrDefault();
        }

        public IEnumerable<Trip> GetTripsByUsername(string username)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .ThenInclude(s => s.Organization)
                .Include(t => t.Stops)
                .ThenInclude(s => s.JobTitle)
                .Where(t => t.UserName == username)
                .ToList();
        }

        public Trip GetUserTripByName(string tripName, string username)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .ThenInclude(s => s.Organization)
                .Include(t => t.Stops)
                .ThenInclude(s => s.JobTitle)
                .Where(t => t.Name == tripName && t.UserName == username)
                .FirstOrDefault();
        }


        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
