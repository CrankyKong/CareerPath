using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/organizations")]
    public class OrganizationsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<OrganizationsController> _logger;

        public OrganizationsController(IWorldRepository repository, ILogger<OrganizationsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all organizations: {ex}");

                return BadRequest("Error occured");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]OrganizationViewModel theOrganization)
        {
            if (ModelState.IsValid)
            {
                //Save to the Database
                var newOrganization = Mapper.Map<Organization>(theOrganization);

                _repository.AddOrganization(newOrganization);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/organizations/{theOrganization.Name}", Mapper.Map<OrganizationViewModel>(newOrganization));
                }
            }

            return BadRequest("Failed to save changes to the database");

        }

    }
}
