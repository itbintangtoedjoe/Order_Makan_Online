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
        public string RevOdr { get; set; }
        public DateTime UserLastUpdate { get; set; }
        public string OrderNumDetail { get; set; }
        public string omh_no { get; set; }
        public string omd_no { get; set; }

        public string Username { get; set; }
        public string UserIdDetail { get; set; }
        public string UserAD { get; set; }
        public DateTime TanggalBuat { get; set; }
        public DateTime UserLastUpdateHeader { get; set; }
        public DateTime UserLastUpdateDetail { get; set; }
        public DateTime Omd_Tanggal { get; set; }
        public string Department { get; set; }
        public string Lokasi { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public int Quantity { get; set; }
        public int QuantitySbm { get; set; }
        public string Shift { get; set; }
        public string NIK { get; set; }
        public string AlasanReject { get; set; }

        
    }

    public class ListDetailAttribute
    {
        public string OrderDetail { get; set; }

        public DateTime TanggalDetail { get; set; }
        public DateTime HariDetail { get; set; }

        public int QuantityDetail { get; set; }

        public string ShiftDetail { get; set; }



    }

    public class ListDetail
    {
        public List<ListDetailAttribute> Detail { get; set; }
    }
}
