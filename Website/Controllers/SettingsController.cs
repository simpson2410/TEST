using Business.IRepostitory;
using Common;
using Entities.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Controllers
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class SettingsController : Controller
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly ILogger<SettingsController> _logger;

        public SettingsController(ISettingsRepository settingsRepository,
            ILogger<SettingsController> logger)
        {
            _logger = logger;
            _settingsRepository = settingsRepository;
        }
        public IActionResult Index()
        {
            var data = _settingsRepository.GetAllData().FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Settings settings)
        {
            try
            {
                if (settings != null)
                {
                    _settingsRepository.Update(settings);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, string.Format(MessageConstants.NotExists, "Setting"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(settings);
        }
    }
}
