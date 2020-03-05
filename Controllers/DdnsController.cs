using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lacker_admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace lacker_admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DdnsController : ControllerBase
    {
        private readonly Ddns service;

        public DdnsController()
        {
            this.service = Ddns.Instance;
        }

        [HttpGet("update")]
        public async Task UpdateRecord()
        {
            await service.UpdateRecord();
        }
    }
}
