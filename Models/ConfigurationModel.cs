using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Widgets.GoogleTagManager.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("GTM Head Snippet")]
        public string HeadSnippet { get; set; }
        public bool HeadSnippet_OverrideForStore { get; set; }
        [NopResourceDisplayName("GTM Body Snippet")]
        public string BodySnippet { get; set; }
        public bool BodySnippet_OverrideForStore { get; set; }
    }
}
