// <copyright file="SmsSender.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Utils
{
    using Aliyun.Acs.Core;
    using Aliyun.Acs.Core.Exceptions;
    using Aliyun.Acs.Core.Http;
    using Aliyun.Acs.Core.Profile;
    using Apsk.Utils.Abstractions;
    using Apsk.Utils.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Sms helper.
    /// </summary>
    public class SmsSender
        : ISmsSender
    {
        private static DefaultAcsClient _client;

        /// <summary>
        /// Send captcha.
        /// </summary>
        /// <returns></returns>
        public SendSmsResponse SendCaptcha(string mobile, string code)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                throw new System.ArgumentException("message", nameof(mobile));

            if (string.IsNullOrWhiteSpace(code))
                throw new System.ArgumentException("message", nameof(code));

            return SendSms(mobile, new SendCaptchaRequest(code));
        }

        /// <summary>
        /// 发送短信，可提供多个手机号发送.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="mobile">手机,多个手机之间用,隔开</param>
        /// <param name="request">请求 需继承<see cref="AbstractSendSmsRequest"/> 设置各自的<see cref="AbstractSendSmsRequest.TemplateCode"/></param>
        /// <returns></returns>
        private SendSmsResponse SendSms<TRequest>(string mobile, TRequest request)
         where TRequest : AbstractSendSmsRequest
        {
            if (_client == null)
            {
                IClientProfile profile = DefaultProfile.GetProfile("default", request.AccessKeyId, request.Secret);
                _client = new DefaultAcsClient(profile);
            }

            CommonRequest req = new CommonRequest
            {
                Method = MethodType.POST,
                Domain = request.Domain,
                Version = request.Version
            };

            // request.Protocol = ProtocolType.HTTP;
            req.AddQueryParameters("PhoneNumbers", mobile);
            req.AddQueryParameters("Action", request.Action);
            req.AddQueryParameters("SignName", request.SignName);
            req.AddQueryParameters("TemplateCode", request.TemplateCode);

            var jsonStr = JsonConvert.SerializeObject(request);
            req.AddQueryParameters("TemplateParam", jsonStr);

            var res = new SendSmsResponse();
            try
            {
                CommonResponse response = _client.GetCommonResponse(req);
                res.Code = response.HttpStatus == 200 ? 0 : 1;
                res.Message = response.Data;
                return res;
            }
            catch (ServerException e)
            {
                res.Code = 1;
                res.Message = e.Message;
            }
            catch (ClientException e)
            {
                res.Code = 1;
                res.Message = e.Message;
            }

            return res;
        }
    }
}
