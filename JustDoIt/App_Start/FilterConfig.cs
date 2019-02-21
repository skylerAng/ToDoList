using System.Web;
using System.Web.Mvc;

namespace JustDoIt
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // Set authorization as default requirement
            filters.Add(new AuthorizeAttribute());
        }
    }
}
