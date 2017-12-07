using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetTripsByUsername(string username);
        Trip GetTripByName(string tripName);
        Trip GetUserTripByName(string tripName, string username);
        Organization GetOrganizationsById(int id);
        IEnumerable<Organization> GetAllOrganizations();
        IEnumerable<JobTitle> GetAllJobTitles();

        void AddTrip(Trip trip);        
        void AddStop(string tripName, Stop newStop, string username);
        void AddOrganization(Organization newOrganization);
        void AddJobTitle(JobTitle jobTitle);

        Task<bool> SaveChangesAsync();
    }
}