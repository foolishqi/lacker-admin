using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace lacker_admin.Services
{
    public class WakeOnLan
    {
        private readonly IEnumerable<dynamic> hosts =
            new List<dynamic>
            {
                new { host = "home", mac = "70:85:C2:36:CA:5A", address = "255.255.255.255", port = 8000 },
                new { host = "yw", mac = "", address = "172.16.10.38", port = 8000 }
            };

        public async Task Wake(string mac, string address, int port)
        {
            var macBuffer = mac.Split('-', ':')
                .Select(m => Convert.ToByte(m, 16))
                .ToArray();

            using (var client = new UdpClient(address, port))
            using (var stream = new MemoryStream())
            {
                client.EnableBroadcast = true;
                
                for (int i = 0; i < 6; i++)
                {
                    stream.WriteByte(0xFF);
                }

                for (int i = 0; i < 16; i++)
                {
                    stream.Write(macBuffer, 0, 6);
                }

                await client.SendAsync(stream.GetBuffer(), (int)stream.Length);
            }
        }

        public Task WakeHost(string host)
        {
            var h = hosts.First(m => m.host == host);

            return Wake(h.mac, h.address, h.port);
        }
    }
}