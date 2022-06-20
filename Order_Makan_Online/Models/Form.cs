using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Makan_Online.Models
{
    public class Form
    {
        public string Category { get; set; }
        public string OrderNum { get; set; }
        public string Username { get; set; }
        public DateTime TanggalBuat { get; set; }
        public string Department { get; set; }
        public string Lokasi { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public int Quantity { get; set; }
        public string Shift { get; set; }


    }
}
