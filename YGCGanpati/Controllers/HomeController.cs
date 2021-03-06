﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YGCGanpati.Models;
namespace YGCGanpati.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            string sql = @"SELECT TDate,SUM(Collections) as Collections,SUM(Expenses) as Expenses,SUM(Balance) Balance FROM ( 
                            (select CollectionDate as TDate,sum(Amount) as Collections,0 Expenses, 0 Balance from Collections where YEAR(CollectionDate)={{YEAR}} group by CollectionDate) 
                            UNION 
                            (select ExpenseDate as TDate,0 as Collections, sum(ExpenseAmount) as Expenses, 0 Balance from Expenses where YEAR(ExpenseDate)={{YEAR}} group by ExpenseDate) ) 
                            AS TBL GROUP BY TDate ";
            sql = sql.Replace("{{YEAR}}", DateTime.Today.Year.ToString());
            var DBdata = db.Database.SqlQuery<GraphData>(sql).ToList();
            return View(DBdata);            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}