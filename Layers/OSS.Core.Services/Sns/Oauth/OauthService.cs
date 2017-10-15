﻿#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：OSSCore服务层 —— 社会化Oauth授权模块
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-10-14
*       
*****************************************************************************/

#endregion

using OSS.Common.Authrization;
using OSS.Common.ComModels;
using OSS.Core.Infrastructure.Enums;
using OSS.Core.Services.Sns.Oauth.Handlers;
using OSS.SnsSdk.Oauth.Wx.Mos;

namespace OSS.Core.Services.Sns.Oauth
{

    public class OauthService
    {
        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <param name="appInfo">当前应用信息</param>
        /// <param name="plat">平台类型</param>
        /// <param name="redirectUrl">回调地址</param>
        /// <param name="state">附加信息，回调时附带</param>
        /// <param name="type">授权类型</param>
        /// <returns>返回授权Url</returns>
        public ResultMo<string> GetOauthUrl(AppAuthorizeInfo appInfo, ThirdPaltforms plat,
            string redirectUrl,AuthClientType type, string state = null)
        {
            var handler = GetHandlerByPlatform(appInfo, plat);
            return handler.GetOauthUrl(redirectUrl, state, type);
        }

        /// <summary>
        /// 获取处理Hander
        /// </summary>
        /// <param name="appInfo">应用信息</param>
        /// <param name="plat">平台类型</param>
        /// <returns></returns>
        private static BaseOauthHander GetHandlerByPlatform(AppAuthorizeInfo appInfo, ThirdPaltforms plat)
        {
            BaseOauthHander handler;
            switch (plat)
            {
                case ThirdPaltforms.Wechat:
                    handler = WxOauthHander.Instance;
                    break;
                //  todo 添加其他平台
                default:
                    handler = BaseOauthHander<BaseOauthHander>.Instance;
                    break;
            }

            handler.SetCOntextConfig(appInfo);
            return handler;
        }

    }
}