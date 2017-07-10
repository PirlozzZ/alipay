using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using System;
using System.Collections.Generic;
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
            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];
            log.Info("code:" + code + "——state:" + state);
            if (!string.IsNullOrEmpty(code))
            {
                IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", "2017062307553030", "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxC63kbKWX++F5SmlFP3/L/fOvCnnhon7psxgGFqw+n0UkTgFNf6MlqaF2l43JacP/At3i5vyMbX/yFcoDW+mNIeLOhrHBoKYbu1mVSQEcW82LLAlasVKM6i0wCmBstlMYmbjInvhYG8n5pkcMPzsgVmSX8h07qFt2sP/Jkz6TVmRmvKKfT2ebT+3vgPvLgcmtl4iEZDvPyhbOqzAdCVh2OpuSym//y74NpxEzZ1/Ml0qoxCmtdmLuaDWv/0pUbcUpmYalr1H9Wvmxd2P7WdSO74iUSOC7bV/yZE82kH8SLKA07cK9nWokhllS/kUPb0uzlhUE93hkKurxS5QU4RR5QIDAQAB", "json", "1.0", "RSA2", "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAofT80EKaSXQIsGqsdpLORJZ2a/EhZNI+5aBoJ2pusjo7F5bxJpPEiii4H09yyHDQKnhcuDfEXNjhX8vpPV5umtFPNfo1PRx/FpSEG2kPDmBKITNd6KotvGKhCouhjsE+WLQbpmLe72J1hpkR8LRn4LH4qnOPRt7wX9Avw0rB9eEUKTy6jbSgFCIv/Nqa5MDKouTI/Hu4/Quf37gYYfz3u7rzl5V8y9bJKofSzxNm9afN8xgUas7abBtev5edX960/t6wq9Rglr97yKxy7FdQN98JYg7wHZHTi+nRjRNtYdKr5Q3sdW85X1yH/ZxpAicCf1eoScrFxIGpumvybPI+HQIDAQAB", "GBK", false);
                AlipayOpenAuthTokenAppRequest request = new AlipayOpenAuthTokenAppRequest();
                request.BizContent = "{" +
                "\"grant_type\":\"authorization_code\"," +
                "\"code\":\"" + code + "\"," +
                "  }";
                AlipayOpenAuthTokenAppResponse response = client.Execute(request);
                log.Info(response.Body);
            }
        }
    }
}