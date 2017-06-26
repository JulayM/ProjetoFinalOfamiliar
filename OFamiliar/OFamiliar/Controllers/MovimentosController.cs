using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OFamiliar.Models;

using System.Linq;



namespace OFamiliar.Controllers
{
    //Organizar
    public class MovimentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movimentos
        public async Task<ActionResult> Index()
        {
            var movimentos = db.Movimentos
                                     .Include(m => m.Categoria)
                                     .Include(m => m.DonoDoMovimento)
                                     .Include(m => m.Familia)
                                     .Where(p => p.DonoDoMovimento.UserName.Equals(User.Identity.Name))
                                     .OrderByDescending(m => m.Data)
                                     .ToListAsync();

            return View(await movimentos);
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

            // lista de famílias da pessoa q se autentica
            var listaDeFamilias = (from f in db.Familias
                                   from p in f.ListaDeMembros
                                   where p.UserName.Equals(User.Identity.Name)
                                   select f);
            // mostra no ecrã a lista de famílias
            ViewBag.FamiliasFK = new SelectList(listaDeFamilias, "FamiliaID", "Nome");

            return View();
        }

        // POST: Movimentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Data,Valor,Moeda,Descricao,FamiliasFK,CategoriaFK")] Movimentos movimento)
        {

            try
            {

                // adicionar o 'dono do movimento'
                movimento.DonoDoMovimento = db.Pessoas.Where(p => p.UserName.Equals(User.Identity.Name)).FirstOrDefault();

                if (ModelState.IsValid)
                {
                    db.Movimentos.Add(movimento);
                    await db.SaveChangesAsync();
                    // falta msg de aviso q correu bem
                    return RedirectToAction("Index");
                }
                            }
            catch (System.Exception)
            {
                // escrever msg a dizer que correu mal ...
                ModelState.AddModelError("", "Ocorreu um erro na operação... ");
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", movimento.CategoriaFK);
            ViewBag.DonoDoMovimentoFK = new SelectList(db.Pessoas, "PessoaID", "Nome", movimento.DonoDoMovimentoFK);
            ViewBag.FamiliasFK = new SelectList(db.Familias, "FamiliaID", "Nome", movimento.FamiliasFK);
            return View(movimento);
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
            db.Entry(movimentos).State = EntityState.Modified;

            if (ModelState.IsValid)
            {

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
