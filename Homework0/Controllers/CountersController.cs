using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homework0.Entities;
using Homework0.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Homework0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountersController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly InMemoryRepository repository;

        public CountersController(ILogger<CountersController> logger, InMemoryRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            await Task.Yield();
            //Get Counters
            var result = repository.GetAllCounters();
            var totalCount = result.Sum(x => x.Clicked);

            return Ok( new { total= totalCount, counters = result});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Counter>> Get(int id)
        {
            await Task.Yield();
            //Get By Id
            var result = repository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("clear")]
        public async Task<ActionResult> Clear() 
        {
            await Task.Yield();
            try
            {
                repository.Clear();
                //Reset
                return Ok("Clear");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }

        [HttpPost("click")]
        public async Task<ActionResult<Counter>> Click(int id)
        {
            await Task.Yield();
            //Get By Id
            var result = repository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            var counter = repository.GetById(id);
            counter.Clicked += 1;

            return counter;
        }

        [HttpPost("set")]
        public async Task<ActionResult> SetCounterList(int count)
        {
            await Task.Yield();
            //set
            try
            {
                repository.Set(count);
                return Ok("Set Completed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }
    }
}
