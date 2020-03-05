using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lacker_admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace lacker_admin.Controllers
{
    [ApiController]
    [Route("api/wol")]
    public class WakeOnLanController : ControllerBase
    {
        private readonly WakeOnLan service;

        public WakeOnLanController()
        {
            this.service = new WakeOnLan();
        }

        [HttpGet]
        public async Task Wake(
            string mac, string address = "255.255.255.255", int port = 8000)
        {
            await service.Wake(mac, address, port);
        }

        [HttpGet("hosts")]
        public Task<IEnumerable<dynamic>> GetHosts() {
            return service.GetHosts();
        }

        [HttpPost("hosts/{host}")]
        public async Task Wake(string host)
        {
            await service.WakeHost(host);
        }
    }
}
