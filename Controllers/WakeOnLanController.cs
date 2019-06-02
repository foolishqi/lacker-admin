using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lacker_admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace lacker_admin.Controllers
{
    [Route("api/wake_on_lan")]
    [ApiController]
    public class WakeOnLanController : ControllerBase
    {
        private readonly WakeOnLan service;

        public WakeOnLanController()
        {
            this.service = new WakeOnLan();
        }

        [HttpGet]
        public async Task<ActionResult> Wake(
            string mac, string address = "255.255.255.255", int port = 8000)
        {
            await service.Wake(mac, address, port);

            return Ok();
        }

        [HttpGet("wake/{host}")]
        public async Task<ActionResult> Wake(string host)
        {
            await service.WakeHost(host);

            return Ok();
        }
    }
}
