﻿using System.Web;
using System.Web.Mvc;

namespace kundt_front_end
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}