using System;
using System.Web.Mvc;


namespace ttkaart.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)] // Allow only one menuitem filter
    public class MenuItemAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// Current menuitem
        /// </summary>
        public string MenuItem { get; set; }

        /// <summary>
        /// Called after the action method is invoked
        /// </summary>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (MenuItem != null)
            {
                filterContext.Controller.ViewBag.MenuItem = MenuItem;
            }
        }

    }

}


