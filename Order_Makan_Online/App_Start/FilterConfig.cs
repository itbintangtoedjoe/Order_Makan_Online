﻿using System.Web;
using System.Web.Mvc;

namespace Order_Makan_Online
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
