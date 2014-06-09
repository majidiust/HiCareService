using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace HiCareService
{
    [ServiceContract]
    interface ServiceInterface
    {
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "Account/login/json/{username}/{password}")]
        ValidatedUser LoginUserJson(string username, string password);
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Xml,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "Account/login/xml/{username}/{password}")]
        ValidatedUser LoginUserXML(string username, string password);
    }
}
