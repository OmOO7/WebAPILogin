using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPIForLogin
{
    public class Authentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization == null)
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                else
                {
                    string token = actionContext.Request.Headers.Authorization.Parameter;
                    if (token.Length > 0)
                    {
                        if (!Demo.APIKEY.Equals(token))
                        {
                            string res = Response("Failed", "Invalid Header");
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, JsonConvert.DeserializeObject(res));
                        }
                    }
                    else
                    {
                        string res = Response("Failed", "Invalid Header");
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, JsonConvert.DeserializeObject(res));
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


            //    if(actionContext.Request.Headers.Authorization != null)
            //    {
            //        var authToken = actionContext.Request.Headers.Authorization.Parameter;
            //        var decodeToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
            //        var arrUserNameandPassword = decodeToken.Split(':');

            //        if (IsAuthorizedUser(arrUserNameandPassword[0], arrUserNameandPassword[1]))
            //        {
            //            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(arrUserNameandPassword[0]), null);
            //        }
            //        else
            //        {
            //            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            //        }
            //    }
            //    else
            //    {
            //        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            //    }

            //}

            //public static bool IsAuthorizedUser(string Username, string Password)
            //{

            //    return Username == "bhushan" && Password == "demo";
            //}

        }
        private static string Response(string status, string DispalyMessage, string messagestatus = "")
        {
            try
            {
                JObject JO = new JObject(new JProperty("status", status),
                                         new JProperty("message", DispalyMessage));
                return JsonConvert.SerializeObject(JO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

