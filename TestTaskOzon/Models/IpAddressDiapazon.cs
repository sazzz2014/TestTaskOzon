using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskOzon
{
    public class IpAddressDiapazon
    {
        public IpAddressDiapazon(string from, string to)
        {
            From = from;
            To = to;
            Current = new IpAddress(from);
            End = new IpAddress(to);
            Diapazon = new List<IpAddress>();
            CreateDate = DateTime.Now;
        }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Начало диапазона IP-адресов
        /// </summary>
        [Key]
        public string From { get; set; }
        /// <summary>
        /// Конец диапазона IP-адресов
        /// </summary>
        [Key]
        public string To { get; set; }
        /// <summary>
        /// Элементы, входящие в данный диапазон
        /// </summary>
        internal List<IpAddress> Diapazon { get; set; }
        /// <summary>
        /// Сериализованные адреса(для записи в бд)
        /// </summary>
        public string DiapazonString { get; set; }
        /// <summary>
        /// Текущее значение диапазона адресов
        /// Изначально равно From, по мере продвижения будет изменяться до End
        /// </summary>
        internal IpAddress Current { get; set; }
        /// <summary>
        /// Конец диапазона IP-адресов
        /// </summary>
        internal IpAddress End { get; set; }


    }
}
