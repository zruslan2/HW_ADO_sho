using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_ADO_shop.Entities
{
    public enum Currency
    {
        KZT,
        USD,
        RUR,
        EUR
    }
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string eMail { get; set; }
        public Currency baseCur { get; set; }
    }
}
