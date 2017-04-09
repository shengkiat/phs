using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PHS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //** BEGIN FORM BUILDER ROUTES **//

            // register
            routes.MapRoute(
               "form-preregistration",
               "registration/{id}",
               defaults: new { controller = "forms", action = "PreRegistration", id = UrlParameter.Optional }

            );


            // download file
            routes.MapRoute(
               "form-download-file",
               "forms/file/download/{valueId}",
               new { controller = "forms", action = "GetFileFromDisk", valueId = UrlParameter.Optional }
            );


            // register
            routes.MapRoute(
               "form-edit",
               "forms/edit/{id}",
               new { controller = "forms", action = "edit", Id = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // register
            routes.MapRoute(
               "form-preview",
               "forms/preview/{id}",
               new { controller = "forms", action = "preview", Id = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // register
            routes.MapRoute(
               "form-register",
               "forms/register/{id}/{embed}",
               new { controller = "forms", action = "register", Id = UrlParameter.Optional, embed = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // create form
            routes.MapRoute(
               "form-create",
               "forms/create",
               new { controller = "forms", action = "create" }
            );

            // form confirmation
            routes.MapRoute(
               "form-confirmation",
               "forms/confirmation/{id}/{embed}",
               new { controller = "forms", action = "formconfirmation", Id = UrlParameter.Optional, embed = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // view form entries
            routes.MapRoute(
               "form-entries",
               "forms/entries/{formid}",
               new { controller = "forms", action = "ViewEntries", formId = UrlParameter.Optional },
               new { formId = @"\d+" }
            );

            // delete form
            routes.MapRoute(
               "form-delete",
               "forms/delete/{formid}",
               new { controller = "forms", action = "delete", formId = UrlParameter.Optional },
               new { formId = @"\d+" }
            );

            // forms home
            routes.MapRoute(
               "form-home",
               "forms/index",
               new { controller = "forms", action = "index" }
            );

            //** END FORM BUILDER ROUTES **//

            // view form
            routes.MapRoute(
               "journeymodality-viewform",
               "journeymodality/viewform/{formId}/{embed}",
               new { controller = "patientJourney", action = "viewForm", formId = UrlParameter.Optional, embed = UrlParameter.Optional },
               new { formId = @"\d+" }
            );

            //
            routes.MapRoute(
               "journeymodality-edit",
               "journeymodality/edit/{formId}",
               new { controller = "patientJourney", action = "edit", formId = UrlParameter.Optional },
               new { formId = @"\d+" }
            );


            // View Form with past data
            routes.MapRoute(
               "ViewSubmittedForm",
               "forms/saveform/{id}/{entryID}/{embed}",
               new { controller = "forms", action = "ViewSaveForm", Id = UrlParameter.Optional, entryID= UrlParameter.Optional,  embed = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "login", id = UrlParameter.Optional }
            );



        }
    }
}
