using CSRedis;
using Newtonsoft.Json;
using Soap.ConsoleApp.Extensions;
using Soap.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static CSRedis.CSRedisClient;

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
            //SerializerCaller();
            //RedisCaller();
            //MediatorCaller();
            //FactoryCaller();
            //EmitCaller();
            //MementoCaller();
            //StateCaller();
            ResponsibilityCaller();

            Console.Read();
        }

        private static void ResponsibilityCaller()
        {
            var developmentMgr = new DevelopMgrHandler();
            var researachMgr = new ResearchMgrHandler();
            var leader = new LeaderHandler();

            developmentMgr.SetNext(researachMgr);
            researachMgr.SetNext(leader);
            leader.SetNext();

            {
                developmentMgr.Handle(10);
            }
            {
                developmentMgr.Handle(1);
            }
            {
                developmentMgr.Handle(4);
            }
        }

        private static void StateCaller()
        {
            var ctx = new StateContext();
            ctx.State = new MorningState() { Hour = 5 };
            ctx.Request();
        }

        private static void MementoCaller()
        {
            var originator = new Originator();
            originator.SetState("gainorloss");
            Console.WriteLine(originator.State);

            var memento = originator.CreateMemento();
            originator.SetState("gain");
            Console.WriteLine(originator.State);

            originator.RecoverMemento(memento);
            Console.WriteLine(originator.State);
        }

        private static void EmitCaller()
        {
            //IFoo<T>,Bar
            {
                //var name = "Soap.Emit";
                //var typeName = "Foo";

                //var baseType = typeof(Bar);
                //var interfaceType = typeof(IFoo<>);

                //var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(name), AssemblyBuilderAccess.Run);
                //var moduleBuilder = assemblyBuilder.DefineDynamicModule(name);

                ////类
                //var typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class);
                //var genericTypeBuilder = typeBuilder.DefineGenericParameters("T")[0];
                //genericTypeBuilder.SetGenericParameterAttributes(GenericParameterAttributes.NotNullableValueTypeConstraint);

                //typeBuilder.SetParent(baseType);
                //typeBuilder.AddInterfaceImplementation(interfaceType.MakeGenericType(genericTypeBuilder));

                ////字段
                //var fldBuilder = typeBuilder.DefineField("_name", genericTypeBuilder, FieldAttributes.Private);

                ////构造函数
                //var ctorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard, new[] { genericTypeBuilder });
                //var ctorIL = ctorBuilder.GetILGenerator();
                //ctorIL.Emit(OpCodes.Ldarg_0);
                //ctorIL.Emit(OpCodes.Ldarg_1);
                //ctorIL.Emit(OpCodes.Stfld, fldBuilder);
                //ctorIL.Emit(OpCodes.Ret);

                ////属性
                //var propBuilder = typeBuilder.DefineProperty("Name", PropertyAttributes.SpecialName, genericTypeBuilder, Type.EmptyTypes);

                ////get prop.
                //var getBuilder = typeBuilder.DefineMethod("get_Name", MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.Virtual, CallingConventions.Standard, genericTypeBuilder, Type.EmptyTypes);
                //var getIL = getBuilder.GetILGenerator();
                //getIL.Emit(OpCodes.Ldarg_0);
                //getIL.Emit(OpCodes.Ldfld, fldBuilder);
                //getIL.Emit(OpCodes.Ret);
                //typeBuilder.DefineMethodOverride(getBuilder, interfaceType.GetProperty("Name").GetGetMethod());
                //propBuilder.SetGetMethod(getBuilder);

                //var setBuilder = typeBuilder.DefineMethod("set_Name", MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.NewSlot, CallingConventions.Standard, null, new[] { genericTypeBuilder });
                //var setIL = setBuilder.GetILGenerator();
                //setIL.Emit(OpCodes.Ldarg_0);
                //setIL.Emit(OpCodes.Ldarg_1);
                //setIL.Emit(OpCodes.Stfld, fldBuilder);
                //setIL.Emit(OpCodes.Ret);//感觉可以省略
                //typeBuilder.DefineMethodOverride(setBuilder, interfaceType.GetProperty("Name").GetSetMethod());
                //propBuilder.SetSetMethod(setBuilder);

                //var methodBuilder = typeBuilder.DefineMethod("DisplayName", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual, CallingConventions.Standard, null, Type.EmptyTypes);
                //var il = methodBuilder.GetILGenerator();
                //il.Emit(OpCodes.Ldarg_0);
                //il.Emit(OpCodes.Ldflda, fldBuilder);
                //il.Emit(OpCodes.Constrained, genericTypeBuilder);
                //il.Emit(OpCodes.Callvirt, typeof(object).GetMethod("ToString", Type.EmptyTypes));
                //il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
                //il.Emit(OpCodes.Ret);

                //typeBuilder.DefineMethodOverride(methodBuilder, baseType.GetMethod("DisplayName", Type.EmptyTypes));

                //var type = typeBuilder.CreateType();
                //var obj = Activator.CreateInstance(type.MakeGenericType(typeof(DateTime)), DateTime.Now);
                //(obj as Bar).DisplayName();
                //var name_ = (obj as IFoo<DateTime>).Name;
            }

            //User
            {
                //var name = "Soap.Emit";
                //var typeName = "User";

                //var asmBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(name), AssemblyBuilderAccess.Run);
                //var moduleBuilder = asmBuilder.DefineDynamicModule(name);

                //var typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass);

                ////static filed:Token
                //var sFldTokenBuilder = typeBuilder.DefineField("Token", typeof(string), FieldAttributes.Public | FieldAttributes.Static);

                ////static ctor.
                //var sCtorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Static, CallingConventions.Standard, Type.EmptyTypes);
                //var sCtorIL = sCtorBuilder.GetILGenerator();
                //sCtorIL.Emit(OpCodes.Ldstr, "ppm.erp");
                //sCtorIL.Emit(OpCodes.Stsfld, sFldTokenBuilder);
                //sCtorIL.Emit(OpCodes.Ret);

                ////filed:id
                //var fldIdBuilder = typeBuilder.DefineField("_id", typeof(string), FieldAttributes.Private);
                //var getIdBuilder = typeBuilder.DefineMethod("get_Id", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot, CallingConventions.Standard, typeof(string), Type.EmptyTypes);
                //var getIdIL = getIdBuilder.GetILGenerator();
                //getIdIL.Emit(OpCodes.Ldarg_0);
                //getIdIL.Emit(OpCodes.Ldfld, fldIdBuilder);
                //getIdIL.Emit(OpCodes.Ret);

                ////prop:id
                //var propIdBuilder = typeBuilder.DefineProperty("Id", PropertyAttributes.SpecialName, typeof(string), Type.EmptyTypes);
                //propIdBuilder.SetGetMethod(getIdBuilder);

                //var ctorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public | MethodAttributes.HideBySig, CallingConventions.Standard, new[] { typeof(string) });
                //var ctorIL = ctorBuilder.GetILGenerator();
                //ctorIL.Emit(OpCodes.Ldarg_0);
                //ctorIL.Emit(OpCodes.Ldarg_1);
                //ctorIL.Emit(OpCodes.Stfld, fldIdBuilder);
                //ctorIL.Emit(OpCodes.Ret);

                //var type = typeBuilder.CreateTypeInfo().AsType();
                //dynamic usr = Activator.CreateInstance(type, Guid.NewGuid().ToString("N"));
                //var id = usr.Id;
            }

            //auto property extension
            {
                var sw = new Stopwatch();
                sw.Start();

                var assemblyName = Guid.NewGuid().ToString("N");
                var asmBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run);
                var moduleBuilder = asmBuilder.DefineDynamicModule(assemblyName);
                var typeBuilder = moduleBuilder.DefineType(nameof(User), TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit);

                typeBuilder.DefineAutoProperty("Name", typeof(string));
                typeBuilder.DefineAutoProperty("Id", typeof(string));

                //var type = typeBuilder.CreateTypeInfo().AsType();
                var type = typeBuilder.CreateType();
                dynamic usr = Activator.CreateInstance(type);
                usr.Id = assemblyName;
                usr.Name = assemblyName;

                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);

                sw.Restart();
                var user = new User();
                user.Id = assemblyName;
                user.Name = assemblyName;
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);
            }
        }

        private static void FactoryCaller()
        {
            var factory = new PostgresFactory();
            factory.CreateOrder();
            factory.CreateUser();
        }

        private static void MediatorCaller()
        {
            var mediator = new ConcreteMediator();

            var usr1 = new User1(mediator);
            var usr2 = new User2(mediator);

            usr1.Send(usr2);
        }

        private static void RedisCaller()
        {
            var csredis = new CSRedisClient(null, "r-8vbegt4nw2n5k8rvo0pd.redis.zhangbei.rds.aliyuncs.com:6379,password=FhXhDs85588100,defaultDatabase=13,prefix=et.erp:");
            RedisHelper.Initialization(csredis);

            var channelName = "gainorloss";

            RedisHelper.Subscribe((channelName, args => Console.WriteLine(args.Body)));

            Thread.Sleep(1000);
            RedisHelper.Publish(channelName, channelName);

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
            var person_ = JsonConvert.DeserializeObject(objJson, type_);
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
