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
               defaults: new { controller = "Forms", action = "PreRegistration", id = UrlParameter.Optional }

            );


            // download file
            routes.MapRoute(
               "form-download-file",
               "Forms/file/download/{valueId}",
               new { controller = "Forms", action = "GetFileFromDisk", valueId = UrlParameter.Optional }
            );


            // register
            routes.MapRoute(
               "form-edit",
               "Forms/edit/{id}",
               new { controller = "Forms", action = "edit", Id = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // register
            routes.MapRoute(
               "form-preview",
               "Forms/preview/{id}",
               new { controller = "Forms", action = "preview", Id = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // register
            routes.MapRoute(
               "form-register",
               "Forms/register/{id}/{embed}",
               new { controller = "Forms", action = "register", Id = UrlParameter.Optional, embed = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // create form
            routes.MapRoute(
               "form-create",
               "Forms/create",
               new { controller = "Forms", action = "create" }
            );

            // form confirmation
            routes.MapRoute(
               "form-confirmation",
               "Forms/confirmation/{id}/{embed}",
               new { controller = "Forms", action = "formconfirmation", Id = UrlParameter.Optional, embed = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // view form entries
            routes.MapRoute(
               "form-entries",
               "Forms/entries/{formid}",
               new { controller = "Forms", action = "ViewEntries", formId = UrlParameter.Optional },
               new { formId = @"\d+" }
            );

            // delete form
            routes.MapRoute(
               "form-delete",
               "Forms/delete/{formid}",
               new { controller = "Forms", action = "delete", formId = UrlParameter.Optional },
               new { formId = @"\d+" }
            );

            // Forms home
            routes.MapRoute(
               "form-home",
               "Forms/index",
               new { controller = "Forms", action = "index" }
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
               "Forms/saveform/{id}/{entryID}/{embed}",
               new { controller = "Forms", action = "ViewSaveForm", Id = UrlParameter.Optional, entryID= UrlParameter.Optional,  embed = UrlParameter.Optional },
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
