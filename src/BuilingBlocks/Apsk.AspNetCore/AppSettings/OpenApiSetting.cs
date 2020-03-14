using Apsk.Annotations;

namespace Apsk.AspNetCore.AppSettings
{
    [PropertySource(IgnoreResourceNotFound =true)]
    public class OpenApiSetting
    {
        /// <summary>
        /// Gets or sets title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets version.
        /// </summary>
        public string Version { get; set; }
    }
}
