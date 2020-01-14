using Apsk.Annotations;
using System;

namespace Apsk.Cloud.AppSettings
{
    [PropertySource(IgnoreResourceNotFound =false)]
    public class ServiceSetting
    {
        public Uri ConsulUri { get; set; } = new Uri("http://localhost:8500");

        public Uri Uri { get; set; }

        public string Name { get; set; }

        public string GetAddress() => $"{Uri.Scheme}://{Uri.Host}";
        public string GetUrl() => $"{Uri.Scheme}://{Uri.Host}:{Uri.Port}";
    }
}
