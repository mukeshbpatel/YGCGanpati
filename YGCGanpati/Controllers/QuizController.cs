using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YGCGanpati.Models;
using Microsoft.AspNet.Identity;

namespace YGCGanpati.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuizController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Play()
        {
            var QList = db.Database.SqlQuery<UserAnswer>("EXEC [dbo].[sp_UserAnswer] N'" +  User.Identity.GetUserId() + "'").ToList();

            return View(QList);
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