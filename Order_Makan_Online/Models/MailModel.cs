using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order_Makan_Online.Models
{
    public class MailModel
    {

        public string mailPriority { get; set; }
        public bool isHtml { get; set; }
        public string mailSubject { get; set; }
        public string mailBody { get; set; }
        public string mailTo { get; set; }
        public string mailCC { get; set; }
        public string mailBCC { get; set; }

    }


}