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
    [Route("api/[controller]")]
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
                throw new Exception("Некорректно указаны параметры запроса");

            var contextIpAddressDiapazon = context.IpAddressDiapazon.Find(from, to);
            if (contextIpAddressDiapazon != null && contextIpAddressDiapazon.DiapazonString != null)
            {
                var ipAddressDiapazonInDb = contextIpAddressDiapazon.DiapazonString
                    .Substring(1, contextIpAddressDiapazon.DiapazonString.Length - 2)
                    .Split(",")
                    .Select(_ => new IpAddress(_.Substring(1, _.Length - 2)));
                return ipAddressDiapazonInDb;
            }
            var ipAddressDiapazon = new IpAddressDiapazon(from, to);
            return GetIpAddresses(ipAddressDiapazon);
        }
        /// <summary>
        /// Получаем список всех адресов в заданном диапазоне
        /// </summary>
        /// <param name="diapazon"></param>
        /// <returns></returns>
        public IEnumerable<IpAddress> GetIpAddresses(IpAddressDiapazon diapazon)
        {
            while (true)
            {
                var newIp = AddOneIp(diapazon.Current);
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
        /// <summary>
        /// Увеличиваем текущее значение ip-адреса на один
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public IpAddress AddOneIp(IpAddress ip)
        {
            for (int i = ip.Ip.Length - 1; i >= 0; i--)
            {
                if (ip.Ip[i] != byte.MaxValue)
                {
                    ip.Ip[i] += 1;
                    break;
                }
                else
                {
                    for (int j = ip.Ip.Length - 1; j >= 0; j--)
                    {
                        if (ip.Ip[j] == byte.MaxValue)
                        {
                            ip.Ip[j] += 1;
                            continue;
                        }
                        ip.Ip[j] += 1;
                        break;
                    }
                }
            }
            return new IpAddress(ip.Ip);
        }
    }
}
