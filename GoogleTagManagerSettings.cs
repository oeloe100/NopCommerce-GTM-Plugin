using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.GoogleTagManager
{
    public class GoogleTagManagerSettings : ISettings
    {
        public string HeadSnippet { get; set; }
        public string BodySnippet { get; set; }
        public string WidgetZone { get; set; }
    }
}
