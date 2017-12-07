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
    [Route("api/jobtitles")]
    public class JobTitlesController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<JobTitlesController> _logger;

        public JobTitlesController(IWorldRepository repository, ILogger<JobTitlesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllJobTitles();
                return Ok(Mapper.Map<IEnumerable<JobTitleViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all jobtitles: {ex}");

                return BadRequest("Error occured");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]JobTitleViewModel theJobTitle)
        {
            if (ModelState.IsValid)
            {
                //Save to the Database
                var newJobTitle = Mapper.Map<JobTitle>(theJobTitle);

                _repository.AddJobTitle(newJobTitle);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/jobtitles/{theJobTitle.Name}", Mapper.Map<JobTitleViewModel>(newJobTitle));
                }
            }

            return BadRequest("Failed to save changes to the database");

        }

    }

}
