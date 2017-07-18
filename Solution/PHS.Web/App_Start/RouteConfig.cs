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

            // PreRegistration
            routes.MapRoute(
               "form-preregistration",
               "registration",
               defaults: new { controller = "FormAccess", action = "PreRegistration" }

            );

            // PreRegistration
            routes.MapRoute(
               "form-public",
               "public/{slug}/{embed}",
               new { controller = "FormAccess", action = "publicFillIn", slug = UrlParameter.Optional, embed = UrlParameter.Optional }
            );


            // download file
            //routes.MapRoute(
               //"form-download-file",
              // "Form/file/download/{valueId}",
              // new { controller = "Form", action = "GetFileFromDisk", valueId = UrlParameter.Optional }
            //);

            // create form
            routes.MapRoute(
               "form-createform",
               "Form/createForm",
               new { controller = "Form", action = "createForm" }
            );

            // edit form
            routes.MapRoute(
               "form-editform",
               "Form/editForm",
               new { controller = "Form", action = "editForm" }
            );

            // view template
            routes.MapRoute(
               "form-viewtemplate",
               "Form/viewtemplate/{formId}",
               new { controller = "Form", action = "viewtemplate", formId = UrlParameter.Optional },
               new { formId = @"\d+" }
            );

            // delete form
            routes.MapRoute(
               "form-deleteform",
               "Form/deleteform/{formId}",
               new { controller = "Form", action = "deleteform", formId = UrlParameter.Optional },
               new { formId = @"\d+" }
            );

            // edit template
            routes.MapRoute(
               "form-edittemplate",
               "Form/edittemplate/{id}",
               new { controller = "Form", action = "edittemplate", Id = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // preview template
            routes.MapRoute(
               "form-previewtemplate",
               "Form/previewtemplate/{id}",
               new { controller = "Form", action = "previewtemplate", Id = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // FillIn
            routes.MapRoute(
               "form-fillin",
               "Form/fillin/{id}/{embed}",
               new { controller = "FormAccess", action = "fillin", Id = UrlParameter.Optional, embed = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // form submit confirmation
            routes.MapRoute(
               "form-submitconfirmation",
               "Form/submitconfirmation/{id}/{embed}",
               new { controller = "FormAccess", action = "SubmitConfirmation", Id = UrlParameter.Optional, embed = UrlParameter.Optional },
               new { Id = @"\d+" }
            );

            // view template entries
            routes.MapRoute(
               "form-entries",
               "Form/entries/{formid}",
               new { controller = "Form", action = "ViewEntries", formId = UrlParameter.Optional },
               new { formId = @"\d+" }
            );

            // delete template
            routes.MapRoute(
               "form-deletetemplate",
               "Form/deletetemplate/{formId}/{templateId}",
               new { controller = "Form", action = "deletetemplate", formId = UrlParameter.Optional, templateId = UrlParameter.Optional },
               new { formId = @"\d+", templateId = @"\d+" }
            );

            // Forms home
            routes.MapRoute(
               "form-home",
               "Form/index",
               new { controller = "Form", action = "index" }
            );

            //** END FORM BUILDER ROUTES **//

            // Internal FillIn
            routes.MapRoute(
               "participantjourney-viewform",
               "formaccess/InternalFillIn/{id}/{embed}",
               new { controller = "FormAccess", action = "InternalFillIn", id = UrlParameter.Optional, embed = UrlParameter.Optional },
               new { id = @"\d+" }
            );

            // view form
            routes.MapRoute(
               "journeymodality-viewform",
               "journeymodality/viewform/{formId}/{embed}",
               new { controller = "pastparticipantJourney", action = "viewForm", formId = UrlParameter.Optional, embed = UrlParameter.Optional },
               new { formId = @"\d+" }
            );

            //
            routes.MapRoute(
               "journeymodality-edit",
               "journeymodality/edit/{formId}",
               new { controller = "pastparticipantJourney", action = "edit", formId = UrlParameter.Optional },
               new { formId = @"\d+" }
            );


            // View Form with past data
            routes.MapRoute(
               "ViewSubmittedForm",
               "Form/saveform/{id}/{entryID}/{embed}",
               new { controller = "FormAccess", action = "ViewSaveForm", Id = UrlParameter.Optional, entryID= UrlParameter.Optional,  embed = UrlParameter.Optional },
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
