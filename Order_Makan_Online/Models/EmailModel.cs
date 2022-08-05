using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Makan_Online.Models
{
    public class EmailModel
    {
        public string EmailReceiver { get; set; }
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Sender { get; set; }
        public string Dept { get; set; }
        public string Username { get; set; }
        public string Option { get; set; }


        public class Recipients
        {
            public string Email { get; set; }
        }




    }
}
