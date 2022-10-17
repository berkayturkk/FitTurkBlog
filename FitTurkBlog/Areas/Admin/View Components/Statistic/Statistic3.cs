using FitTurkBlog.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Xml;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.Statistic
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class Statistic3 : ViewComponent
    {

        SqlDbContext sqlDbContext = new SqlDbContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.vUsers = sqlDbContext.Users.Count();
            ViewBag.vContact = sqlDbContext.Contacts.Count();
            ViewBag.vCategory = sqlDbContext.Categories.Count();
            string bugun = "http://www.tcmb.gov.tr/kurlar/today.xml";
            var xmldoc = new XmlDocument();
            xmldoc.Load(bugun);
            DateTime tarih = Convert.ToDateTime(xmldoc.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);
            string USD = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='USD']/BanknoteSelling").InnerXml;
            string EUR = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='EUR']/BanknoteSelling").InnerXml;
            string GBP = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='GBP']/BanknoteSelling").InnerXml;
            ViewBag.vUSD = USD;
            ViewBag.vEUR = EUR;
            ViewBag.vGBP = GBP;

            return View();
        }
    }
}
