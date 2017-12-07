using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldContextSeedData
    {
        private WorldContext _context;
        private UserManager<WorldUser> _userManager;

        public WorldContextSeedData(WorldContext context, UserManager<WorldUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            if (await _userManager.FindByEmailAsync("sam.hastings@theworld.com") == null)
            {
                var user = new WorldUser()
                {
                    UserName = "loganskidmore",
                    Email = "loganskidmore91@gmail.com"
                };

                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }

            //Adds data to db if there is nothhing in it
            if (!_context.Trips.Any())
            {
                var BurgerKing = new Organization()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "Burger King"
                };

                var cashier = new JobTitle()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "Cashier"
                };

                var tacoBell = new Organization()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "Taco Bell"
                };

                var webDev = new JobTitle()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "Web Developer"
                };

                var church = new Organization()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "The Church of Jesus Christ of Latter-day Saints"
                };

                var byui = new Organization()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "Brigham Young University - Idaho"
                };


                var usTrip = new Trip()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "Logan's Career Path",
                    UserName = "loganskidmore", 
                    Stops = new List<Stop>()
                    {
                        new Stop() {  Name = "Olathe, KS", Arrival = new DateTime(2006, 6, 4), Latitude = 38.8835296630859, Longitude = -94.8188705444336, Order = 0, Organization = BurgerKing, JobTitle = cashier },
                        new Stop() {  Name = "Olathe, KS", Arrival = new DateTime(2008, 6, 9), Latitude = 38.8835296630859, Longitude = -94.8188705444336, Order = 1, Organization = new Organization(){ DateCreated = DateTime.UtcNow,
                    Name = "Walmart"}, JobTitle = cashier },
                        new Stop() {  Name = "Edinburgh, Scotland", Arrival = new DateTime(2010, 10, 22), Latitude = 55.9542999267578, Longitude = -3.20188999176025, Order = 2, Organization = church, JobTitle = new JobTitle(){ DateCreated = DateTime.UtcNow,
                    Name = "Missionary"} },
                        new Stop() {  Name = "Olathe, KS", Arrival = new DateTime(2012, 10, 7),  Latitude = 38.8835296630859, Longitude = -94.8188705444336, Order = 3, Organization = new Organization(){ DateCreated = DateTime.UtcNow,
                    Name = "Family Video"}, JobTitle = cashier },
                        new Stop() {  Name = "Manhattan, KS", Arrival = new DateTime(2014, 8, 13), Latitude = 39.1788101196289, Longitude = -96.5618362426758, Order = 4, Organization = new Organization(){ DateCreated = DateTime.UtcNow,
                    Name = "Holiday Inn"}, JobTitle =  new JobTitle(){ DateCreated = DateTime.UtcNow,
                    Name = "Bellman"} },
                        new Stop() {  Name = "Rexburg, ID", Arrival = new DateTime(2015, 1, 7), Latitude = 43.8260688781738, Longitude = -111.783088684082, Order = 5, Organization = byui, JobTitle =  new JobTitle(){ DateCreated = DateTime.UtcNow,
                    Name = "Student"} },
                        new Stop() {  Name = "Rexburg, ID", Arrival = new DateTime(2016, 4, 23), Latitude = 43.8260688781738, Longitude = -111.783088684082, Order = 5, Organization = byui, JobTitle =  new JobTitle(){ DateCreated = DateTime.UtcNow,
                    Name = "Response Center Operator"} },
                        new Stop() {  Name = "Riverton, UT", Arrival = new DateTime(2017, 4, 23), Latitude = 40.5205383300781, Longitude = -111.931671142578, Order = 5, Organization = church, JobTitle =  webDev }
                    }
                };

                _context.Trips.Add(usTrip);

                _context.Stops.AddRange(usTrip.Stops);

                var worldTrip = new Trip()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "Logan's Dream path",
                    UserName = "loganskidmore", 
                    Stops = new List<Stop>()
                    {
                        new Stop() { Order = 0, Latitude =  33.748995, Longitude =  -84.387982, Name = "Atlanta, Georgia", Arrival = DateTime.Parse("Jun 3, 2014"), Organization = tacoBell, JobTitle = webDev },
                        new Stop() { Order = 1, Latitude =  48.856614, Longitude =  2.352222, Name = "Paris, France", Arrival = DateTime.Parse("Jun 4, 2014"), Organization = tacoBell, JobTitle = webDev },
                        new Stop() { Order = 2, Latitude =  50.850000, Longitude =  4.350000, Name = "Brussels, Belgium", Arrival = DateTime.Parse("Jun 25, 2014"), Organization = tacoBell, JobTitle = webDev },
                        new Stop() { Order = 3, Latitude =  51.209348, Longitude =  3.224700, Name = "Bruges, Belgium", Arrival = DateTime.Parse("Jun 28, 2014"), Organization = tacoBell, JobTitle = webDev },
                        new Stop() { Order = 4, Latitude =  48.856614, Longitude =  2.352222, Name = "Paris, France", Arrival = DateTime.Parse("Jun 30, 2014"), Organization = tacoBell, JobTitle = webDev },
                        new Stop() { Order = 5, Latitude =  51.508515, Longitude =  -0.125487, Name = "London, UK", Arrival = DateTime.Parse("Jul 8, 2014"), Organization = tacoBell, JobTitle = webDev },
                        new Stop() { Order = 6, Latitude =  51.454513, Longitude =  -2.587910, Name = "Bristol, UK", Arrival = DateTime.Parse("Jul 24, 2014"), Organization = tacoBell, JobTitle = webDev }
             
                    }
                };

                _context.Trips.Add(worldTrip);

                _context.Stops.AddRange(worldTrip.Stops);

                //push data to DB
                await _context.SaveChangesAsync();

            }
        }
    }
}
