using System.Web;
using System.Web.Mvc;

namespace StudiekollenNew
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //// Du måste vara admin för att komma åt allt OM INTE [AllowAnonymous]- attributet specifikt appliceras.
            //filters.Add(new AuthorizeAttribute() { Roles = "Admin" });
        }
    }
}
