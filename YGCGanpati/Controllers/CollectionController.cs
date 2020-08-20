using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YGCGanpati.Models;
using Microsoft.AspNet.Identity;
using SelectPdf;

namespace YGCGanpati.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private DateTime dt = DateTime.Now;

        public ActionResult Receipt(int id)
        {
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            } else
            {
                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.PdfPageSize = PdfPageSize.HalfLetter;
                converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
                converter.Options.WebPageWidth = 1024;
                converter.Options.WebPageHeight = 0;

                // create a new pdf document converting an url
                PdfDocument doc = converter.ConvertHtmlString(GetHtml(collection), string.Empty);

                
                byte[] fileBytes = doc.Save();
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Pdf, collection.FlatNo + " " + collection.Name + ".pdf");
            }
             
        }

        // GET: Collection
        public ActionResult Index()
        {
            var collections = db.Collections.Where(c=>c.CollectionDate.Year == dt.Year).OrderBy(o=>o.FlatNo);
            return View(collections.ToList());
        }

        // GET: Collection/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: Collection/Create
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Collection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CollectionID,FirstName,LastName,FlatNo,CollectionDate,Amount")] Collection collection)
        {

            collection.CreatedDate = Common.GetCurrentDate();            
            collection.ModifiedDate = collection.CreatedDate;
            collection.UserProfile = db.Users.Find(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                db.Collections.Add(collection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(collection);
        }

        // GET: Collection/Edit/5
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }            
            return View(collection);
        }

        // POST: Collection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CollectionID,FirstName,LastName,FlatNo,CollectionDate,Amount,CreatedDate")] Collection collection)
        {            
            collection.ModifiedDate = Common.GetCurrentDate();
            collection.UserProfile = db.Users.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                db.Entry(collection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }            
            return View(collection);
        }

        // GET: Collection/Delete/5
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: Collection/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Manager, Admin")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Collection collection = db.Collections.Find(id);
            db.Collections.Remove(collection);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        const string receiptHTML = @"
<html>
<head>
<style>
body { font-size: 20px; font-family: 'Times New Roman', Times, serif; margin: 20px;}
h1 { color: #ff6600;  }
.img1 { position: absolute; top: 40px;left: 40px;}
.img2 { position: absolute; top: 40px;right: 40px;}

a { color: #ff6600; text-decoration: none; }
h3 { font-size:18px; }
table { width: 99%; margin-top: 35px; margin-bottom: 20px; font-size: 26px; font-family: 'Times New Roman', Times, serif;}
td { padding-top: 15px; padding-bottom: 15px; line-height: 1.6; }
</style>
</head>
 <body>
<img src='http://ganpati.ygconline.com/images/ganesh.png' class='img1' height='100px' />
<img src='http://ganpati.ygconline.com/images/ygc.png' class='img2' height='100px' />
<div style='border:ridge; border-radius: 10px; width:97%; height:590px; text-align: center; padding:10px;'>  
<h1>Yashraj Green Castle Ganpati Festival</h1>
<h3>Kale Padal, Hadapsar, Pune-411028<h3> 
<hr/>
<table>
	<tr>
    	<th colspan='3' style='font-size: 25px;'>CASH RECEIPT</th>
    </tr>
	<tr>
    	<td><b>Receipt No</b> :- <i>{{ReceiptNo}}</i></td>
        <td>&nbsp;</td>
        <td style='text-align: right;'><b>Date</b> :- <i>{{Date}}</i></td>
    </tr>
    <tr>
    	<td colspan='3'>Received with thanks from <b>{{Name}} ({{FlatNo}})</b> the sum of &nbsp; <i> ₹ {{Amount}}/- <br/>({{AmountWord}}) </i> by Cash towards Yashraj Green Castle Ganpati Festival Fund.</td>
    </tr>
    <tr>
    	<td colspan='3' style='text-align: right; padding-top:40px;'>
            Received By,<br/>
            <b><i>YGC Ganesh Festival Treasurer</i></b>
        </td>
    </tr>
</table>
<div><a href='http://ganpati.ygconline.com'>http://ganpati.ygconline.com</a></div>
</div>
 </body>
</html>";
        private string GetHtml(Collection collection)
        {
            string result = receiptHTML.Replace("{{Date}}", collection.CollectionDate.ToString("dd-MMM-yyyy"))
                                       .Replace("{{Name}}", collection.Name)
                                       .Replace("{{ReceiptNo}}", (collection.CollectionID - 667).ToString("000"))
                                       .Replace("{{FlatNo}}", collection.FlatNo)
                                       .Replace("{{Amount}}", collection.Amount.ToString("0"))
                                       .Replace("{{AmountWord}}", NumbersToWords(collection.Amount));
            return result;
        }

        private static string NumbersToWords(decimal inputNumber)
        {
            int inputNo = (int)inputNumber;

            if (inputNo == 0)
                return "Zero";

            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }

            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }


    }
}
