using alipay.Lib.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace alipay.Lib.Core
{
    public class alipayCore
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Log.Logging");
        private Config config;
        #region 构造方法
        public alipayCore() : this("")
        {

        }

        public alipayCore(string sign)
        {
            config = new Config(sign);
            
        }
        #endregion

        #region OAuth2.0相关处理

        /// <summary>
        /// 应用授权作用域。
        ///auth_user：以auth_user为scope发起的网页授权，是用来获取用户的基本信息的（比如头像、昵称等）。但这种授权需要用户手动同意，用户同意后，就可在授权后获取到该用户的基本信息。
        ///auth_base：以auth_base为scope发起的网页授权，是用来获取进入页面的用户的userId的，并且是静默授权并自动跳转到回调页的。用户感知的就是直接进入了回调页（通常是业务页面）。默授权，可获取成员的详细信息，但不包含手机、邮箱；
        /// </summary>
        public enum ScopeTypeEnum
        {
            auth_user,
            auth_base
        }

        /// <summary>
        /// 生成OAuth相关的URL
        /// </summary>
        /// <param name="para_URL"></param>
        /// <param name="scope">应用授权作用域
        /// <param name="state">重定向后会带上state参数</param>
        /// <returns></returns>
        public string OAuth_getURL(string para_URL, ScopeTypeEnum scope, string state)
        {
            string OAuth_URL = string.Empty;
            try
            {
                OAuth_URL = string.Format("https://openauth.alipay.com/oauth2/publicAppAuthorize.htm?app_id={0}&scope={1}&redirect_uri={2}&state={3}", config.app_id, scope.ToString(), System.Web.HttpUtility.UrlEncode(para_URL), state);
            }
            catch (Exception e)
            {
                log.Error(string.Format("Corp getOAuth_URL:"), e);
            }
            return OAuth_URL;
        }

        /// <summary>
        /// OAuth根据Code获取用户code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="agentid"></param>
        /// <returns></returns>
        public OAuth_UserInfo OAuth_getUserInfo(string code)
        {
            OAuth_UserInfo instance = null;
            string result = string.Empty;
            if (!string.IsNullOrEmpty(code))
            {
                string url = string.Format("https://openapi.alipay.com/gateway.do");
                try
                {
                    result = HTTPHelper.PostRequest(url, DataTypeEnum.json, string.Format("{\"code\": \"{0}\",\"grant_type\":\"authorization_code\"}", code));
                    log.Info("OAuth_getUserInfo result" + result);
                    instance = JsonConvert.DeserializeObject<OAuth_UserInfo>(result);
                    //if (instance != null)
                    //{
                         
                    //}
                    //else
                    //{
                    //    log.Info(string.Format("OAuth_getUserInfo JsonConvert Failed:{0}", result));
                    //}
                }
                catch (Exception e)
                {
                    log.Error(string.Format("OAuth_getUserInfo ERR:{0}", url), e);
                }
            }
            return instance;
        }
        #endregion
    }
}
