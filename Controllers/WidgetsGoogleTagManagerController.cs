using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.GoogleTagManager.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Widgets.GoogleTagManager.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
    [AutoValidateAntiforgeryToken]
    public class WidgetsGoogleTagManagerController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        #endregion

        #region Ctor

        public WidgetsGoogleTagManagerController(
            ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        #endregion

        #region Methods

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var googleTagManagerSettings = _settingService.LoadSetting<GoogleTagManagerSettings>(storeScope);

            var model = new ConfigurationModel()
            {
                BodySnippet = googleTagManagerSettings.BodySnippet,
                HeadSnippet = googleTagManagerSettings.HeadSnippet,
            };

            if (storeScope > 0)
            {
                model.BodySnippet_OverrideForStore = _settingService.SettingExists(googleTagManagerSettings, x => x.BodySnippet, storeScope);
                model.HeadSnippet_OverrideForStore = _settingService.SettingExists(googleTagManagerSettings, x => x.HeadSnippet, storeScope);
            }

            return View("~/Plugins/Widgets.GoogleTagManager/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var googleTagManagerSettings = _settingService.LoadSetting<GoogleTagManagerSettings>(storeScope);

            googleTagManagerSettings.BodySnippet = model.BodySnippet;
            googleTagManagerSettings.HeadSnippet = model.HeadSnippet;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            _settingService.SaveSettingOverridablePerStore(googleTagManagerSettings, x => x.BodySnippet, model.BodySnippet_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(googleTagManagerSettings, x => x.HeadSnippet, model.HeadSnippet_OverrideForStore, storeScope, false);

            //now clear settings cache
            _settingService.ClearCache();

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        #endregion
    }
}
