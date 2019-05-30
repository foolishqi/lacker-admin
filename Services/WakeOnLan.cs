using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace lacker_admin.Services
{
    public class WakeOnLan
    {
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
    }
}