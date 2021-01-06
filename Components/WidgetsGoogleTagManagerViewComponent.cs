using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Widgets.GoogleTagManager.Components
{
    [ViewComponent(Name = "WidgetsGoogleTagManager")]
    public class PaymentMolliePaymentStandardViewComponent : NopViewComponent
    {
        private readonly GoogleTagManagerSettings _googleTagManagerSettings;

        public PaymentMolliePaymentStandardViewComponent(
            GoogleTagManagerSettings googleTagManagerSettings)
        {
            _googleTagManagerSettings = googleTagManagerSettings;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var script = _googleTagManagerSettings.BodySnippet;

            return View("~/Plugins/Widgets.GoogleTagManager/Views/PublicInfo.cshtml", script);
        }
    }
}
