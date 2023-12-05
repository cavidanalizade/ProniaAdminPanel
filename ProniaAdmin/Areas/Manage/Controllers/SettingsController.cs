﻿using Microsoft.AspNetCore.Mvc;
using System;

namespace ProniaAdmin.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingsController : Controller
    {
        AppDBC _db;

        public SettingsController(AppDBC db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Setting> list = _db.Setting.ToList();

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                _db.Setting.Add(setting);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(setting);
        }
        public IActionResult Update(string Key)
        {
            Setting setting = _db.Setting.Find(Key);
            if (setting == null)
            {
                return View();
            }
            return View(setting);
        }


        [HttpPost]
        public IActionResult Update(Setting newSetting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Setting oldSetting = _db.Setting.Find(newSetting.Key);
            if (oldSetting == null)
            {
                return View();
            }
            oldSetting.Key = newSetting.Key;
            oldSetting.Value = newSetting.Value;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string Key)
        {
            var setting = _db.Setting.FirstOrDefault(s => s.Key == Key);
            if (setting == null)
            {
                return View();
            }
            _db.Setting.Remove(setting);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }

}
