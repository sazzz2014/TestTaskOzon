using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TestTaskOzon
{
    public class IpAddress : IEquatable<IpAddress>
    {
        public IpAddress(byte[] ip)
        {
            Ip = (byte[])ip.Clone();
            Address = $"{ip[0]}.{ip[1]}.{ip[2]}.{ip[3]}";
        }
        public IpAddress(string ip)
        {
            Ip = ip.ParseIp();
            Address = $"{Ip[0]}.{Ip[1]}.{Ip[2]}.{Ip[3]}";
        }

        public bool Equals([AllowNull] IpAddress other)
        {
            return Ip[0] == other.Ip[0]
                && Ip[1] == other.Ip[1] 
                && Ip[2] == other.Ip[2] 
                && Ip[3] == other.Ip[3];
        }
        internal byte[] Ip { get; set; }
        public string Address { get; set; }
    }
    public static class IpAddressExtension
    {
        public static byte[] ParseIp(this string ip)
        {
            try
            {
                var qq = new List<byte>();
                var spl = ip.Split('.');
                foreach (var item in spl)
                {
                    qq.Add(Byte.Parse(item));
                }
                return qq.ToArray();
            }
            catch
            {
                throw new Exception("Не удалось распарсить Ip");
            }
        }
    }

}
