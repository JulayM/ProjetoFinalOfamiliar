using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OFamiliar.Models;
using OFamiliar.Models;

namespace OFamiliar.Controllers
{
    //Organizar
    public class MovimentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movimentos
        public async Task<ActionResult> Index()
        {
            var movimentos = db.Movimentos.Include(m => m.Categoria).Include(m => m.DonoDoMovimento).Include(m => m.Familia);
            return View(await movimentos.ToListAsync());
        }

        // GET: Movimentos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentos movimentos = await db.Movimentos.FindAsync(id);
            if (movimentos == null)
            {
                return HttpNotFound();
            }
            return View(movimentos);
        }

        // GET: Movimentos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome");
            ViewBag.DonoDoMovimentoFK = new SelectList(db.Pessoas, "PessoaID", "Nome");
            ViewBag.FamiliasFK = new SelectList(db.Familias, "FamiliaID", "Nome");
            return View();
        }

        // POST: Movimentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MovimentosID,Data,Valor,Descricao,DonoDoMovimentoFK,FamiliasFK,CategoriaFK")] Movimentos movimentos)
        {
            if (ModelState.IsValid)
            {
                db.Movimentos.Add(movimentos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", movimentos.CategoriaFK);
            ViewBag.DonoDoMovimentoFK = new SelectList(db.Pessoas, "PessoaID", "Nome", movimentos.DonoDoMovimentoFK);
            ViewBag.FamiliasFK = new SelectList(db.Familias, "FamiliaID", "Nome", movimentos.FamiliasFK);
            return View(movimentos);
        }

        // GET: Movimentos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentos movimentos = await db.Movimentos.FindAsync(id);
            if (movimentos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", movimentos.CategoriaFK);
            ViewBag.DonoDoMovimentoFK = new SelectList(db.Pessoas, "PessoaID", "Nome", movimentos.DonoDoMovimentoFK);
            ViewBag.FamiliasFK = new SelectList(db.Familias, "FamiliaID", "Nome", movimentos.FamiliasFK);
            return View(movimentos);
        }

        // POST: Movimentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MovimentosID,Data,Valor,Descricao,DonoDoMovimentoFK,FamiliasFK,CategoriaFK")] Movimentos movimentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimentos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", movimentos.CategoriaFK);
            ViewBag.DonoDoMovimentoFK = new SelectList(db.Pessoas, "PessoaID", "Nome", movimentos.DonoDoMovimentoFK);
            ViewBag.FamiliasFK = new SelectList(db.Familias, "FamiliaID", "Nome", movimentos.FamiliasFK);
            return View(movimentos);
        }

        // GET: Movimentos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentos movimentos = await db.Movimentos.FindAsync(id);
            if (movimentos == null)
            {
                return HttpNotFound();
            }
            return View(movimentos);
        }

        // POST: Movimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Movimentos movimentos = await db.Movimentos.FindAsync(id);
            db.Movimentos.Remove(movimentos);
            await db.SaveChangesAsync();
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
