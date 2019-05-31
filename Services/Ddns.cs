using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace lacker_admin.Services
{
    public class Ddns
    {
        public static readonly Ddns Instance = new Ddns();
        
        private readonly HttpClient client;

        public Ddns()
        {
            this.client = new HttpClient();
        }

        public async Task<string> UpdateRecord()
        {
            var address = await GetIPAsync();

            var message = await UpdateRecord(address);

            return message;
        }

        public void RunAutoUpdate()
        {
            Task.Run(AutoUpdateRecord);
        }

        private async Task<string> GetIPAsync()
        {
            var address = await client.GetStringAsync("http://ipinfo.io/ip");
            
            return address.TrimEnd();
        }

        private async Task<string> GetRecord()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "login_token", "99424,901e9241e465b3b53fb47e59b32289e1" },
                { "format", "json" },
                { "domain", "foolishqi.tk" },
                { "record_id", "68881311" },
            });

            var response = await client.PostAsync("https://dnsapi.cn/Record.Info", content);

            var message = await response.Content.ReadAsStringAsync();

            return message;
        }

        private async Task<string> UpdateRecord(string address)
        {
            var param = new Dictionary<string, string>()
            {
                { "login_token", "99424,901e9241e465b3b53fb47e59b32289e1" },
                { "format", "json" },
                { "domain", "foolishqi.tk" },
                { "record_id", "68881311" },
                { "sub_domain", "m" },
                { "record_type", "A" },
                { "record_line_id", "0" },
                { "ttl", "600" },
                { "value", address },
            };

            var response = await client.PostAsync(
                "https://dnsapi.cn/Record.Modify", new FormUrlEncodedContent(param));

            return await response.Content.ReadAsStringAsync();
        }

        private async Task AutoUpdateRecord()
        {
            var message = await GetRecord();

            while (true)
            {
                var address = await GetIPAsync();

                if (!message.Contains(address))
                {
                    message = await UpdateRecord(address);
                }

                await Task.Delay(60 * 1000);
            }
        }
    }
}