using Newtonsoft.Json;
using Soap.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Soap.ConsoleApp
{
    class Program
    {
        public static event EventHandler<EventProcessedArgs> @event;
        static void Main(string[] args)
        {

            //SoapCaller();
            //SoapCaller2();
            //ValueTypeCaller();
            //TemplateMethodCaller();
            //EnumratorCaller();
            //PrototypeCaller();
            //EventQueueCaller();
            //FilterCaller();
            //StrategyCaller();
            //ObserverCaller();
            SerializerCaller();

            Console.Read();
        }

        private static void SerializerCaller()
        {
            var type = typeof(Person);
            var person = new Person()
            {
                Address = new Address() { Province = "江苏", City = "南京", DetailAddress = "大沟村一组", District = "栖霞", Mobile = "17551031463", Postcode = "223700" },
                Name = "gainorloss"
            };

            var typeJson = JsonConvert.SerializeObject(type);
            var objJson = JsonConvert.SerializeObject(person);
            Console.WriteLine(typeJson);
            Console.WriteLine(objJson);

            var type_ = JsonConvert.DeserializeObject<Type>(typeJson);
            var person_ = JsonConvert.DeserializeObject(objJson,type_);
        }

        private static void ObserverCaller()
        {
            var loggingObserver = new LoggerObserver();
            var doObserver = new DoObserver();

            var subject = new ConcreteSubject();
            subject.AddObserver(loggingObserver);
            subject.AddObserver(doObserver);
            subject.Notify();
        }

        private static void StrategyCaller()
        {
            var strategy = new StrategyContext(new FileLogger());
            strategy.Write();
        }

        private static void FilterCaller()
        {
            var students = new List<Student>() { new Student() { Age = 25, Name = "gain", Email = "519564415@qq.com", Birthday = new DateTime(1993, 03, 08) },
            new Student() { Age = 26, Name = "gain", Email = "381471278@qq.com", Birthday = new DateTime(1993, 03, 08) }};

            var filters = new List<IFilter>() { new AgeFilter(), new NameFilter(), new MailFilter() };

            var andFilter = new AndFilter(filters);
            var rt = andFilter.Filter(students);
        }

        private static void EventQueueCaller()
        {
            @event += Program_event;
        }

        private static void Program_event(object sender, EventProcessedArgs e)
        {
            throw new NotImplementedException();
        }

        private static void PrototypeCaller()
        {
            var person = new Person() { Name = "gainorloss" };
            person.Address = new Address() { DetailAddress = "郑楼镇大沟村一组52号" };

            {
                //var clone = person.Clone() as Person;
                //clone.Address = person.Address.Clone() as Address;
                //person.Name = "gain";
                //person.Address.DetailAddress = "东城一品";
            }

            {
                //using (var ms = new MemoryStream())
                //{
                //    BinaryFormatter binaryFormatter = new BinaryFormatter();
                //    binaryFormatter.Serialize(ms, person);

                //    ms.Seek(0, SeekOrigin.Begin);
                //    var clone = binaryFormatter.Deserialize(ms);
                //    person.Address = new Address() { DetailAddress = "郑楼镇大沟村一组52号" };
                //}
            }

            {
                //var copy = person.Copy();
                //person.Name = "ZhangJian";
                //person.Address.Postcode = "223700";
            }

            {
                var times = 1000000;

                Perform(() => person.Clone(), times);

                Perform(() => person.Copy(), times);

                person.Address.Postcode = "223700";
            }
        }

        private static void Perform(Action act, int times = 1)
        {
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < times + 1; i++)
                act();
            sw.Stop();
            Console.WriteLine($"[$Clone]:执行{times}用时{sw.ElapsedMilliseconds}(无线电)");
        }

        private static void EnumratorCaller()
        {
            var aggregation = new Aggregation();
            aggregation.Add(1);
            aggregation.Add(2);
            aggregation.Add(3);
            aggregation.Add(4);

            var enumerator = aggregation.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }

        private static void TemplateMethodCaller()
        {
            AbstractExam zhangSan = new ZhangSan();
            zhangSan.Question();

            AbstractExam lisi = new LiSi();
            lisi.Question();
        }

        private static void ValueTypeCaller()
        {
            var times = 100000000;
            var a = new Point2D(10, 1);
            var b = new Point2D(10, 1);

            var rt = new List<long>();
            var sw = new Stopwatch();
            sw.Restart();
            for (int i = 0; i < times; i++)
            {
                a.Equals(b);
                //Console.WriteLine($"a.Equals(b):{a.Equals(b)}");
                //Console.WriteLine($"a==b:{a == b}");
                //Console.WriteLine($"a!=b:{a != b}");
                //Console.WriteLine($"a:{a.GetHashCode()};b:{b.GetHashCode()};");
            }
            sw.Stop();
            rt.Add(sw.ElapsedMilliseconds);

            var c = new Point(10, 1);
            var d = new Point(10, 1);
            sw.Restart();
            for (int i = 0; i < times; i++)
            {
                c.Equals(d);
                //Console.WriteLine($"c.Equals(d):{c.Equals(d)}");
                //Console.WriteLine($"c:{c.GetHashCode()};d:{d.GetHashCode()};");
            }
            sw.Stop();
            rt.Add(sw.ElapsedMilliseconds);
            Console.WriteLine($"{string.Join(",", rt)}");
        }

        private static void SoapCaller()
        {
            var hello = new Hello() { Name = "dnawo" };

            #region Xml.
            //构造soap请求信息
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            sb.Append("<soap:Header>");
            sb.Append("<MySoapHeader xmlns=\"http://www.mzwu.com/\" soap:mustUnderstand=\"1\">");//mustUnderstand=1,接收者必须能处理此SoapHeader信息,否则返回错误
            sb.Append("<UserName>admin</UserName>");
            sb.Append("<UserPass>admin888</UserPass>");
            sb.Append("</MySoapHeader>");
            sb.Append("</soap:Header>");
            sb.Append("<soap:Body>");
            sb.Append("<Hello xmlns=\"http://www.mzwu.com/\">");
            sb.Append("<name>dnawo</name>");
            sb.Append("</Hello>");
            sb.Append("</soap:Body>");
            sb.Append("</soap:Envelope>");
            sb.Append("</xml>");
            #endregion

            var postStr = string.Empty;
            using (var ms = new MemoryStream())
            {
                var soap = new SoapFormatter();
                soap.Serialize(ms, hello);

                postStr = Encoding.UTF8.GetString(ms.ToArray());
                Console.WriteLine(postStr);
            }

            XmlTypeMapping myTypeMapping =
    new SoapReflectionImporter().ImportTypeMapping(typeof(Hello));
            XmlSerializer mySerializer = new XmlSerializer(myTypeMapping);
            using (var ms = new MemoryStream())
            {
                mySerializer.Serialize(ms, hello);

                postStr = Encoding.UTF8.GetString(ms.ToArray());
                Console.WriteLine(postStr);
            }

            //发起请求
            Uri uri = new Uri("http://192.168.4.222:8888/services/ORDERCANCEL");
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.ContentType = "text/xml; charset=utf-8";
            webRequest.Method = "POST";
            using (Stream requestStream = webRequest.GetRequestStream())
            {
                byte[] paramBytes = Encoding.UTF8.GetBytes(postStr);
                requestStream.Write(paramBytes, 0, paramBytes.Length);
            }

            //响应
            WebResponse webResponse = webRequest.GetResponse();

            using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
            {
                var rspStr = myStreamReader.ReadToEnd();

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(rspStr);

                XmlNamespaceManager nsp = new XmlNamespaceManager(xmlDoc.NameTable);
                nsp.AddNamespace("wsdl", "http://schemas.xmlsoap.org/wsdl/");

                var node = xmlDoc.SelectSingleNode("//wsdl:message[1]/wsdl:part", nsp);
            }
        }

        private static void SoapCaller2()
        {

            #region 获取支持的省
            {
                //var soapClient = new SoapClient();
                //var rsp = soapClient.Call<GetSupportProvinceResponse>("http://www.webxml.com.cn/WebServices/ChinaZipSearchWebService.asmx", new GetSupportProvince());
            }
            #endregion
            {
                var soapClient = new SoapClient();
                var rsp = soapClient.Call<getFLZLByZyIDResponse>("http://qbgm5n.natappfree.cc//services/DzdaFlzlZyDownloadService", new getFLZLByZyIDRequest());
            }

            #region 订单取消
            {
                //var url = "http://192.168.4.222:8888/services/ORDERCANCEL";

                //var soapClient = new SoapClient();

                //var orderCancel = new orderCancel()
                //{
                //    externorderkey = "11",
                //    storerkey = "11",
                //    whid = "11"
                //};
                //orderCancel.items.Add(new orderCancelSku() { erporderitemid = "11" });
                //var req = new ImpOrderCancel();
                //req.list.Add(orderCancel);

                //var rsp = soapClient.Call<ImpOrderCancelResponse>(url, req);
            }
            #endregion

            #region 订单修改
            {
                //var soapClient = new SoapClient();

                //var req = new ImpOrderModifyRequest();
                //req.List.Add(new OrderModify()
                //{
                //     Whid="11"
                //});

                //var rsp = soapClient.Call<ImpOrderModifyResponse>("http://192.168.4.222:8888/services/ORDERMODIFY", req);
            }
            #endregion
        }
    }
}
