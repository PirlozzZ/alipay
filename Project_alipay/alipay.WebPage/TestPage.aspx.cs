using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace alipay.WebPage
{
    public partial class TestPage : System.Web.UI.Page
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Log.Logging");
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = Request.QueryString["auth_code"];
            string state = Request.QueryString["state"];
            log.Info("code:" + code + "——state:" + state);
            if (!string.IsNullOrEmpty(code))
            {
                try
                {
                    string publicKey = ConfigurationManager.AppSettings["publicKey"];
                    string privateKey = ConfigurationManager.AppSettings["privateKey"];
                    string publicKeyPem = GetCurrentPath() + "public-key.pem";
                    string privateKeyPem = GetCurrentPath() + "aop-sandbox-RSA-private-c#.pem";
                    log.Info("publicKeyPem:" + publicKeyPem);
                    log.Info("privateKeyPem:" + privateKeyPem);
                    IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", "2017062307553030", privateKey, "json", "1.0", "RSA", publicKey, "GBK", false);
                    AlipaySystemOauthTokenRequest request = new AlipaySystemOauthTokenRequest();
                    request.GrantType = "authorization_code";
                    request.Code = code;
                    //request.RefreshToken = "201208134b203fe6c11548bcabd8da5bb087a83b";
                    AlipaySystemOauthTokenResponse response = client.Execute(request);
                    Console.WriteLine(response.Body);
                    log.Info(response.Body);
                }
                catch (Exception err)
                {
                    log.Error("err:", err);
                }
            }
        }

        private static string GetCurrentPath()
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            return basePath + @"Key\";
        }
    }
}