using Apsk.Annotations;

namespace Apsk.AspNetCore.AppSettings
{
    [PropertySource(nameof(OpenApiSetting))]
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
