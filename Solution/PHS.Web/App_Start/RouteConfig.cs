﻿using System;
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


            // edit
            routes.MapRoute(
               "form-edittemplate",
               "Forms/edittemplate/{id}",
               new { controller = "Forms", action = "edittemplate", Id = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // preview
            routes.MapRoute(
               "form-previewtemplate",
               "Forms/previewtemplate/{id}",
               new { controller = "Forms", action = "previewtemplate", Id = UrlParameter.Optional },
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
               "form-createtemplate",
               "Forms/createTemplate",
               new { controller = "Forms", action = "createTemplate" }
            );

            // form submit confirmation
            routes.MapRoute(
               "form-submitconfirmation",
               "Forms/submitconfirmation/{id}/{embed}",
               new { controller = "Forms", action = "SubmitConfirmation", Id = UrlParameter.Optional, embed = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // view template entries
            routes.MapRoute(
               "form-entries",
               "Forms/entries/{formid}",
               new { controller = "Forms", action = "ViewEntries", formId = UrlParameter.Optional },
               new { formId = @"\d+" }
            );

            // delete form
            routes.MapRoute(
               "form-deletetemplate",
               "Forms/deletetemplate/{templateId}",
               new { controller = "Forms", action = "deletetemplate", templateId = UrlParameter.Optional },
               new { templateId = @"\d+" }
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
               new { controller = "Forms", action = "ViewSaveTemplate", Id = UrlParameter.Optional, entryID= UrlParameter.Optional,  embed = UrlParameter.Optional },
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
