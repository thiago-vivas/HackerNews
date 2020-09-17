using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HackerNews.Manager;
using HackerNews.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryManager manager;
        public StoriesController(IStoryManager manager)
        {
            this.manager = manager;
        }
        [HttpGet("Best")]
        [ResponseCache(Duration = 60, VaryByQueryKeys = new string[] { "count" })]
        public async Task<ActionResult<IList<Story>>> GetBest([FromQuery] int count = 20)
        {
            return Ok(await manager.GetBestStories(count));
        }
    }
}
