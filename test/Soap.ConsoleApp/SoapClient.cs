using Soap.ConsoleApp.Models;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Soap.ConsoleApp
{
    public class SoapClient
    {
        public TResponse Call<TResponse>(string url, object para = null)
            where TResponse : class
        {
            string reqStr = string.Empty;
            if (para != null)
            {
                using (var ms = new MemoryStream())
                {
                    new XmlSerializer(para.GetType()).Serialize(ms, para);
                    reqStr = Encoding.UTF8.GetString(ms.ToArray());
                    var idx = reqStr.IndexOf('>');
                    reqStr = reqStr.Substring(idx + 1);
                }
            }

            #region 构造Soap请求
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            sb.AppendLine("  <soap:Body>");

            if (!string.IsNullOrWhiteSpace(reqStr))
                sb.AppendLine(reqStr);

            sb.AppendLine("  </soap:Body>");
            sb.AppendLine("</soap:Envelope>");
            #endregion

#if DEBUG
            Console.WriteLine($"【请求体】：{sb.ToString()}");
#endif

            WebRequest webRequest = WebRequest.Create(new Uri(url));
            webRequest.ContentType = "text/xml; charset=utf-8";
            webRequest.Method = "POST";
            using (Stream requestStream = webRequest.GetRequestStream())
            {
                byte[] paramBytes = Encoding.UTF8.GetBytes(sb.ToString());
                requestStream.Write(paramBytes, 0, paramBytes.Length);
            }

            //响应
            WebResponse webResponse = webRequest.GetResponse();
            using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
            {
                var rspStr = myStreamReader.ReadToEnd();

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(rspStr);
                var xmlNsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
                xmlNsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                var bodyNode = xmlDoc.DocumentElement.SelectSingleNode("//soap:Body", xmlNsMgr);
#if DEBUG
                Console.WriteLine($"【响应体】:{bodyNode.InnerXml}");
#endif
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TResponse));

                using (var sr = new StringReader(bodyNode.InnerXml))
                {
                    var rsp = xmlSerializer.Deserialize(sr) as TResponse;
                    return rsp;
                }
            }
        }
    }
}
