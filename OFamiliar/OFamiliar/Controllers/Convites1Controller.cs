using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OFamiliar.Models;

namespace OFamiliar.Controllers
{
    public class Convites1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Convites1
        public ActionResult Index()
        {
            var convites = db.Convites.Include(c => c.Destinatario).Include(c => c.Emissor).Include(c => c.Familiar);
            return View(convites.ToList());
        }

        // GET: Convites1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convite convite = db.Convites.Find(id);
            if (convite == null)
            {
                return HttpNotFound();
            }
            return View(convite);
        }

        // GET: Convites1/Create
        public ActionResult Create()
        {
            ViewBag.DestinatarioFK = new SelectList(db.Pessoas, "PessoaID", "Nome");

            var emissor = db.Pessoas.Where(p => p.UserName.Equals(User.Identity.Name));


            ViewBag.EmissorFK = new SelectList(db.Pessoas, "PessoaID", "Nome");
            ViewBag.FamiliarFK = new SelectList(db.Familias, "FamiliaID", "Nome");
            return View();
        }

        // POST: Convites1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConviteID,Data,Token,EstadoDoConvite,EmissorFK,DestinatarioFK,FamiliarFK")] Convite convite)
        {
            if (ModelState.IsValid)
            {
                db.Convites.Add(convite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DestinatarioFK = new SelectList(db.Pessoas, "PessoaID", "Nome", convite.DestinatarioFK);
            ViewBag.EmissorFK = new SelectList(db.Pessoas, "PessoaID", "Nome", convite.EmissorFK);
            ViewBag.FamiliarFK = new SelectList(db.Familias, "FamiliaID", "Nome", convite.FamiliarFK);
            return View(convite);
        }

        // GET: Convites1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convite convite = db.Convites.Find(id);
            if (convite == null)
            {
                return HttpNotFound();
            }
            ViewBag.DestinatarioFK = new SelectList(db.Pessoas, "PessoaID", "Nome", convite.DestinatarioFK);
            ViewBag.EmissorFK = new SelectList(db.Pessoas, "PessoaID", "Nome", convite.EmissorFK);
            ViewBag.FamiliarFK = new SelectList(db.Familias, "FamiliaID", "Nome", convite.FamiliarFK);
            return View(convite);
        }

        // POST: Convites1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConviteID,Data,Token,EstadoDoConvite,EmissorFK,DestinatarioFK,FamiliarFK")] Convite convite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(convite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DestinatarioFK = new SelectList(db.Pessoas, "PessoaID", "Nome", convite.DestinatarioFK);
            ViewBag.EmissorFK = new SelectList(db.Pessoas, "PessoaID", "Nome", convite.EmissorFK);
            ViewBag.FamiliarFK = new SelectList(db.Familias, "FamiliaID", "Nome", convite.FamiliarFK);
            return View(convite);
        }

        // GET: Convites1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convite convite = db.Convites.Find(id);
            if (convite == null)
            {
                return HttpNotFound();
            }
            return View(convite);
        }

        // POST: Convites1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Convite convite = db.Convites.Find(id);
            db.Convites.Remove(convite);
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
    }
}
