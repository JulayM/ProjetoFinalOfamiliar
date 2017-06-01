using OFamiliar.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OFamiliar.Controllers
{
    public class ConvitesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Convites
        public async Task<ActionResult> Index()
        {
            var convites = db.Convites.Include(c => c.Destinatario).Include(c => c.Emissor).Include(c => c.Familiar);
            return View(await convites.ToListAsync());
        }

        // GET: Convites/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convite convite = await db.Convites.FindAsync(id);
            if (convite == null)
            {
                return HttpNotFound();
            }
            return View(convite);
        }

        // GET: Convites/Create
        public ActionResult Create()
        {
            ViewBag.DestinatarioFK = new SelectList(db.Pessoas, "PessoaID", "Nome");
            ViewBag.EmissorFK = new SelectList(db.Pessoas, "PessoaID", "Nome");
            ViewBag.FamiliarFK = new SelectList(db.Familias, "FamiliaID", "Nome");
            return View();
        }

        // POST: Convites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ConviteID,Data,Token,EmissorFK,DestinatarioFK,FamiliarFK")] Convite convite)
        {
            if (ModelState.IsValid)
            {
                db.Convites.Add(convite);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DestinatarioFK = new SelectList(db.Pessoas, "PessoaID", "Nome", convite.DestinatarioFK);
            ViewBag.EmissorFK = new SelectList(db.Pessoas, "PessoaID", "Nome", convite.EmissorFK);
            ViewBag.FamiliarFK = new SelectList(db.Familias, "FamiliaID", "Nome", convite.FamiliarFK);
            return View(convite);
        }

        // GET: Convites/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convite convite = await db.Convites.FindAsync(id);
            if (convite == null)
            {
                return HttpNotFound();
            }
            ViewBag.DestinatarioFK = new SelectList(db.Pessoas, "PessoaID", "Nome", convite.DestinatarioFK);
            ViewBag.EmissorFK = new SelectList(db.Pessoas, "PessoaID", "Nome", convite.EmissorFK);
            ViewBag.FamiliarFK = new SelectList(db.Familias, "FamiliaID", "Nome", convite.FamiliarFK);
            return View(convite);
        }

        // POST: Convites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ConviteID,Data,Token,EmissorFK,DestinatarioFK,FamiliarFK")] Convite convite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(convite).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DestinatarioFK = new SelectList(db.Pessoas, "PessoaID", "Nome", convite.DestinatarioFK);
            ViewBag.EmissorFK = new SelectList(db.Pessoas, "PessoaID", "Nome", convite.EmissorFK);
            ViewBag.FamiliarFK = new SelectList(db.Familias, "FamiliaID", "Nome", convite.FamiliarFK);
            return View(convite);
        }

        // GET: Convites/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convite convite = await db.Convites.FindAsync(id);
            if (convite == null)
            {
                return HttpNotFound();
            }
            return View(convite);
        }

        // POST: Convites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Convite convite = await db.Convites.FindAsync(id);
            db.Convites.Remove(convite);
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
