using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestTaskOzon.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpAddressController : ControllerBase
    {
        private IpAddressContext context { get; set; }
        public IpAddressController(IpAddressContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IEnumerable<IpAddress> Get(string from, string to)
        {
            if (from == null || to == null)
                return null;
            var contextIpAddressDiapazon = context.IpAddressDiapazon.Find(from, to);
            if (contextIpAddressDiapazon != null && contextIpAddressDiapazon.DiapazonString != null)
            {
                return JsonSerializer.Deserialize<List<IpAddress>>(contextIpAddressDiapazon.DiapazonString);
            }
            var ipAddressDiapazon = new IpAddressDiapazon(from, to);
            var f = GetIpAddresses(ipAddressDiapazon);
            return GetIpAddresses(ipAddressDiapazon);
        }
        public IEnumerable<IpAddress> GetIpAddresses(IpAddressDiapazon diapazon)
        {
            while (true)
            {
                AddOneIp(diapazon.Current);
                var newIp = new IpAddress(diapazon.Current.Ip);
                if (newIp.Equals(diapazon.End))
                {
                    diapazon.DiapazonString = JsonSerializer.Serialize(diapazon.Diapazon.Select(_ => _.Address));
                    context.IpAddressDiapazon.Add(diapazon);
                    context.SaveChanges();
                    return diapazon.Diapazon;
                }
                diapazon.Diapazon.Add(newIp);
            }
        }

        public void AddOneIp(IpAddress Ip)
        {
            for (int i = Ip.Ip.Length - 1; i >= 0; i--)
            {
                if (Ip.Ip[i] != byte.MaxValue)
                {
                    Ip.Ip[i] += 1;
                    return;
                }
                else
                {
                    for (int j = Ip.Ip.Length - 1; j >= 0; j--)
                    {
                        if (Ip.Ip[j] == byte.MaxValue)
                        {
                            Ip.Ip[j] += 1;
                            continue;
                        }
                        Ip.Ip[j] += 1;
                        return;
                    }
                }

            }
        }
    }
}
