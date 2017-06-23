using OFamiliar.App_Code;
using OFamiliar.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace OFamiliar.Controllers
{
    // [Authorize]
    public class ConvitesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Convites
        public async Task<ActionResult> Index()
        {
            var convites = db.Convites
                .Include(c => c.Destinatario)
                .Include(c => c.Emissor)
                .Include(c => c.Familiar) // os includes 'ligam' as chaves foranteiras das diversas ligações
                .Where(p => p.Emissor.UserName.Equals(User.Identity.Name)); // este 'where' mostra apenas os convites feitos pela pessoa autenticada...
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
            // lista dos destinatários
            ViewBag.DestinatarioFK = new SelectList(db.Pessoas, "PessoaID", "Nome");
            // quem faz o convite
            var emissor = db.Pessoas.Where(p => p.UserName.Equals(User.Identity.Name));
            ViewBag.EmissorFK = new SelectList(emissor, "PessoaID", "Nome");
            // famílias a que o emissor pertence
            var familiasDoEmissor = db.Familias.Where(f => f.ListaDeMembros.ToList().FirstOrDefault().UserName.Equals(User.Identity.Name)).ToList();
            ViewBag.FamiliarFK = new SelectList(familiasDoEmissor, "FamiliaID", "Nome");

            return View();
        }

        // POST: Convites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmissorFK,DestinatarioFK,FamiliarFK")] Convite convite)
        {
            try
            {
                // obtem a data em que o convite é feito
                convite.Data = DateTime.Now;
                // obtem o TOKEN para o convite
                convite.Token = Guid.NewGuid().ToString();
                // define o 'estado' do convite
                convite.EstadoDoConvite = "pendente";

                if (ModelState.IsValid)
                {
                    // guardar os dados na BD
                    db.Convites.Add(convite);
                    await db.SaveChangesAsync();
                    // enviar um email ao Destinatário, para q ele possa aceitar o convite
                    var destinatario = db.Pessoas.Find(convite.DestinatarioFK);
                    var convidante = db.Pessoas.Find(convite.EmissorFK);
                    // email do destinatário
                    string emailDoDestinario = destinatario.Email;
                    // 'subject' da mensagem de email
                    string subjectEmail = "Orçamento Familiar: Confirmação de convite";
                    // 'body' da mensagem de email
                    string bodyEmail = "Olá " + destinatario.Nome + ",<br /><br />" +
                                       "O nosso utilizador " + convidante.Nome + " convidou-o para fazer parte da sua família. <br />" +
                                       "Para aceitar o convite, clique no seguinte link: " +
                                       "<a href=\"" + Request.Url.Host + "/Convites/ConfirmarConvite/" + convite.Token + "\">" + Request.Url.Host + "/Convites/ConfirmarConvite/" + convite.Token + "</a>." +
                                       "<br /><br />" +
                                       "Com os nossos melhores cumprimentos<br />" +
                                       "A equipa do Orçamento Familiar";
                    // enviar o email
                    Ferramentas.sendEmail(emailDoDestinario, subjectEmail, bodyEmail);

                    // mostra a página de INDEX com a lista dos convites efetuados pelo Emissor
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {
                // gerar uma mensagem de erro
                // a ser entregue ao utilizador
                ModelState.AddModelError("", "Ocorreu um erro na operação ");
            }

            // lista dos destinatários
            ViewBag.DestinatarioFK = new SelectList(db.Pessoas, "PessoaID", "Nome");
            // quem faz o convite
            var emissor = db.Pessoas.Where(p => p.UserName.Equals(User.Identity.Name));
            ViewBag.EmissorFK = new SelectList(emissor, "PessoaID", "Nome");
            // famílias a que o emissor pertence
            var familiasDoEmissor = db.Familias.Where(f => f.ListaDeMembros.ToList().FirstOrDefault().UserName.Equals(User.Identity.Name)).ToList();
            ViewBag.FamiliarFK = new SelectList(familiasDoEmissor, "FamiliaID", "Nome");

            return View(convite);
        }


        /// <summary>
        /// método para validar a aceitação de um convite
        /// </summary>
        /// <param name="id">Token enviado ao utilizador</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ConfirmarConvite(string id)
        {
            try
            {
                if (id == null)
                { // não forneci um TOKEN
                  // redireciono para a página de início
                  // enviar MSG de informação de falta de Tokn ao utilizador
                    Session["Erro"] = true;
                    Session["mensagem"] = "Não foi fornecido um TOKEM válido";
                    return RedirectToAction("Index", "Home");
                }
                //procurar o convite q tem este TOKEN
                Convite convite = db.Convites.Where(c => c.Token.Equals(id)).Where(c => c.EstadoDoConvite.Equals("pendente")).FirstOrDefault();

                if (convite == null)
                {   // O TOKEN não identificou nenhum Convite válido
                    // redireciono para a página de início
                    // enviar MSG de informação de não foi encontrado convite válido
                    Session["Erro"] = true;
                    Session["mensagem"] = "Não foi encontrado um Convite válido";
                    return RedirectToAction("Index", "Home");
                }

                // se cheguei aqui é pq há um convite para aceitar
                // por isso:
                // - marcar o convite como 'aceite'
                convite.EstadoDoConvite = "aceite";
                // - associar o distinatário do convite à família do convidante
                //     - identificar o destinatário
                Pessoas destinatario = db.Pessoas.Find(convite.DestinatarioFK);
                //     - identificar a família
                Familia familia = db.Familias.Find(convite.FamiliarFK);
                //     - juntar o destinatário à família
                familia.ListaDeMembros.Add(destinatario);

                // tornar as alterações definitivas...
                if (ModelState.IsValid)
                {
                    db.Entry(convite).State = EntityState.Modified;
                    db.Entry(familia).State = EntityState.Modified;
                    db.SaveChanges();
                }
                // enviar MSG de informação de sucesso ao utilizador
                Session["Sucesso"] = true;
                Session["mensagem"] = "A pessoa " + destinatario.Nome + " foi adicionada com SUCESSO à família " + familia.Nome + ".";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {                // enviar MSG de informação de q ocorreu um erro ao utilizador
                Session["Erro"] = true;
                Session["mensagem"] = "Ocorreu um erro não especificado.";
                return RedirectToAction("Index", "Home");
            }
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
            db.Entry(convite).State = EntityState.Modified;

            try
            {
                if (ModelState.IsValid)
                {

                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {

                // gerar uma mensagem de erro
                // a ser entregue ao utilizador
                ModelState.AddModelError("",
                   "Ocorreu um erro na operação ");

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
            try
            {

                db.Convites.Remove(convite);
                await db.SaveChangesAsync();

            }
            catch (System.Exception)
            {


                // gerar uma mensagem de erro
                // a ser entregue ao utilizador
                ModelState.AddModelError("",
               "Ocorreu um erro na operação ");

            }
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
