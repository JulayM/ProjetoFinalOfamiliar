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

namespace OFamiliar.Controllers
{
    public class FamiliasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Familias
        public async Task<ActionResult> Index()
        {
            return View(await db.Familias.ToListAsync());
        }

        // GET: Familias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = await db.Familias.FindAsync(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // GET: Familias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Familias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FamiliaID,Nome,DataDeCriacao")] Familia familia)
        {
            if (ModelState.IsValid)
            {
                db.Familias.Add(familia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(familia);
        }

        // GET: Familias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = await db.Familias.FindAsync(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // POST: Familias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FamiliaID,Nome,DataDeCriacao")] Familia familia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(familia);
        }

        // GET: Familias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = await db.Familias.FindAsync(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // POST: Familias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Familia familia = await db.Familias.FindAsync(id);
            db.Familias.Remove(familia);
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
