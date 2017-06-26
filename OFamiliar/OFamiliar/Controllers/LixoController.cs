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
    public class LixoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lixo
        public ActionResult Index()
        {
            var movimentos = db.Movimentos.Include(m => m.Categoria).Include(m => m.DonoDoMovimento).Include(m => m.Familia);
            return View(movimentos.ToList());
        }

        // GET: Lixo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentos movimentos = db.Movimentos.Find(id);
            if (movimentos == null)
            {
                return HttpNotFound();
            }
            return View(movimentos);
        }

        // GET: Lixo/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome");
            ViewBag.DonoDoMovimentoFK = new SelectList(db.Pessoas, "PessoaID", "Nome");
            ViewBag.FamiliasFK = new SelectList(db.Familias, "FamiliaID", "Nome");
            return View();
        }

        // POST: Lixo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovimentosID,Data,Valor,Moeda,Descricao,DonoDoMovimentoFK,FamiliasFK,CategoriaFK")] Movimentos movimentos)
        {
            if (ModelState.IsValid)
            {
                db.Movimentos.Add(movimentos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", movimentos.CategoriaFK);
            ViewBag.DonoDoMovimentoFK = new SelectList(db.Pessoas, "PessoaID", "Nome", movimentos.DonoDoMovimentoFK);
            ViewBag.FamiliasFK = new SelectList(db.Familias, "FamiliaID", "Nome", movimentos.FamiliasFK);
            return View(movimentos);
        }

        // GET: Lixo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentos movimentos = db.Movimentos.Find(id);
            if (movimentos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", movimentos.CategoriaFK);
            ViewBag.DonoDoMovimentoFK = new SelectList(db.Pessoas, "PessoaID", "Nome", movimentos.DonoDoMovimentoFK);
            ViewBag.FamiliasFK = new SelectList(db.Familias, "FamiliaID", "Nome", movimentos.FamiliasFK);
            return View(movimentos);
        }

        // POST: Lixo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovimentosID,Data,Valor,Moeda,Descricao,DonoDoMovimentoFK,FamiliasFK,CategoriaFK")] Movimentos movimentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", movimentos.CategoriaFK);
            ViewBag.DonoDoMovimentoFK = new SelectList(db.Pessoas, "PessoaID", "Nome", movimentos.DonoDoMovimentoFK);
            ViewBag.FamiliasFK = new SelectList(db.Familias, "FamiliaID", "Nome", movimentos.FamiliasFK);
            return View(movimentos);
        }

        // GET: Lixo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentos movimentos = db.Movimentos.Find(id);
            if (movimentos == null)
            {
                return HttpNotFound();
            }
            return View(movimentos);
        }

        // POST: Lixo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movimentos movimentos = db.Movimentos.Find(id);
            db.Movimentos.Remove(movimentos);
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
