using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheSlice.Models;
using TheSlice.Service;
using System.Web.Routing;

namespace TheSlice.Controllers
{
    public class ApplicationController<TSource> : Controller
    {
        private const string LogOnSession = "LogOnSession";  //session index name
        private const string ErrorController = "Error";      //session independent controller
        private const string LogOnController = "Home";      //session independent and LogOn controller    
        private const string LogOnAction = "Login";          //action to rederect

        public ApplicationController()
        {
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            /*important to check both, because logOn and error Controller should be access able with out any session*/
            if (!IsNonSessionController(requestContext) && !HasSession())
            {
                //Rederect to logon action
                Rederect(requestContext, Url.Action(LogOnAction, LogOnController));
            }
        }

        public bool IsNonSessionController(RequestContext requestContext)
        {
            var currentController = requestContext.RouteData.Values["controller"].ToString().ToLower();
            var nonSessionedController = new List<string>() { ErrorController.ToLower(), LogOnController.ToLower() };
            return nonSessionedController.Contains(currentController);
        }

        public void Rederect(RequestContext requestContext, string action)
        {
            requestContext.HttpContext.Response.Clear();
            requestContext.HttpContext.Response.Redirect(action);
            requestContext.HttpContext.Response.End();
        }

        public bool HasSession()
        {
            //check session is available
            return Session[LogOnSession] != null;
        }

        public TSource GetLogOnSessionModel()
        {
            //return logged in session model
            return (TSource)this.Session[LogOnSession];
        }

        public void SetLogOnSessionModel(TSource model)
        {
            //update or set logged in session model
            Session[LogOnSession] = model;
        }

        public void AbandonSession()
        {
            //destroy logged in session model
            if (HasSession())
            {
                Session.Abandon();
            }
        }        
    }
}