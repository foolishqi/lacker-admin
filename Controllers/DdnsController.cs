using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lacker_admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace lacker_admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DdnsController : ControllerBase
    {
        private readonly Ddns service;

        public DdnsController()
        {
            this.service = Ddns.Instance;
        }

        [HttpGet("update")]
        public async Task<ActionResult> UpdateRecord()
        {
            return Ok(await service.UpdateRecord());
        }
    }
}
