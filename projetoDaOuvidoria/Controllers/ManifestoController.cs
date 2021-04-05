using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using projetoDaOuvidoria.Models;
using PagedList;

namespace ouvidoriaProva.Controllers
{
    public class ManifestoController : Controller
    {
        //Contexto de conexão ao db
        private Contexto db = new Contexto();


        //Envio do Email
        [HttpPost]
        public ActionResult EnviaEmail()
        {
            string emailDestinatario = Request.Form["txtEmail"];
            string assuntoDestinatario = Request.Form["assuntoEmail"];
            string respostaDestinatario = Request.Form["respostaEmail"];
            string emailOuvidor = Request.Form["emailOuvidor"];
            string senhaOuvidor = Request.Form["senhaOuvidor"];
            string anexo = Request.Form["anexo"];
            SendMail(emailDestinatario, assuntoDestinatario, respostaDestinatario, emailOuvidor, senhaOuvidor, anexo);

            return RedirectToAction("Index");
        }
        public object SendMail(string email, string assunto, string resposta, string emailOuvidor, string senhaOuvidor, string anexo)
        {
            try
            {
                // Estancia da Classe de Mensagem
                MailMessage _mailMessage = new MailMessage();
                // Remetente
                _mailMessage.From = new MailAddress(emailOuvidor);
                // Destinatario seta no metodo abaixo
                //Contrói o MailMessage
                _mailMessage.CC.Add(email);
                _mailMessage.Subject = (assunto);
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = (resposta);
                Attachment anexar = new Attachment(anexo);
                _mailMessage.Attachments.Add(anexar);

                //CONFIGURAÇÃO COM PORTA PARA GMAIL
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                //CONFIGURAÇÃO SEM PORTA
                // SmtpClient _smtpClient = new SmtpClient(UtilRsource.ConfigSmtp);

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação)
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential(emailOuvidor, senhaOuvidor);
                _smtpClient.EnableSsl = true;
                //busca as informações já obtidas no _mailMessage
                _smtpClient.Send(_mailMessage);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.protocolo = String.IsNullOrEmpty(sortOrder) ? "protocolo" : "protocolo";
            ViewBag.nome = String.IsNullOrEmpty(sortOrder) ? "nome" : "nome";
            ViewBag.assunto = String.IsNullOrEmpty(sortOrder) ? "assunto" : "assunto";
            ViewBag.setor = String.IsNullOrEmpty(sortOrder) ? "setor" : "setor";
            //Paginação
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            //Busca da Lista o que nao obteve resposta
            var manif = from s in db.Manifesto
                        where s.RespostaOuvidoria.Equals(null)
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                //Barra de Busca por nome assunto ou setor
                manif = manif.Where(s => s.Nome.Contains(searchString)
                                       || s.Assunto.Contains(searchString)
                                        || s.Setor.Contains(searchString)
                                       );
            }
            //Ordenação por tipo de dado
            switch (sortOrder)
            {
                case "nome":
                    manif = manif.OrderBy(s => s.Nome);
                    break;
                case "assunto":
                    manif = manif.OrderBy(s => s.Assunto);
                    break;
                case "setor":
                    manif = manif.OrderBy(s => s.Setor);
                    break;
                case "dataCriacao":
                    manif = manif.OrderBy(s => s.DataCriacao);
                    break;
                default:
                    manif = manif.OrderBy(s => s.Protocolo);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(manif.ToPagedList(pageNumber, pageSize));
        }

        // GET: manifestos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manifesto manifesto = db.Manifesto.Find(id);
            if (manifesto == null)
            {
                return HttpNotFound();
            }
            return View(manifesto);
        }
        // POST: manifestos/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "Protocolo,Nome,Email,Telefone,Celular,Perfil,Campus," +
                                                    "Curso,TipoSolicitacao,Setor,Assunto,Manifestacao,DataCriacao," +
                                                    "RespostaOuvidoria")] Manifesto manifesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manifesto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manifesto);
        }
        // GET: manifestos/Create
        public ActionResult Create()
        {

            //Listas Pré Definidas de Perfil, Campus e Solicitação
            var perfilList = new List<string>() { "Aluno", "Pais", "Professor", "Funcionário", "Visitante" };
            ViewBag.perfilList = perfilList;
            var campuslList = new List<string>() { "Volta Redonda", "Barra do Piraí", "Niterói" };
            ViewBag.campusList = campuslList;
            var solicitacaolList = new List<string>() { "Elogio", "Sugestão", "Reclamação", "Outro" };
            ViewBag.solicitacaoList = solicitacaolList;
            return View();
        }
        // POST: manifestos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Protocolo,Nome,Email,Telefone,Celular,Perfil,Campus,Curso,TipoSolicitacao,Setor,Assunto,Manifestacao,DataCriacao,RespostaOuvidoria")] Manifesto manifesto)
        {
            if (ModelState.IsValid)
            {
               
                db.Manifesto.Add(manifesto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manifesto);
        }
        // GET: manifestos/Responder/5
        //E-mail
        public ActionResult Responder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manifesto manifesto = db.Manifesto.Find(id);
            if (manifesto == null)
            {
                return HttpNotFound();
            }
            return View(manifesto);
        }
        // POST: manifestos/Responder/5
        //E-mail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Responder([Bind(Include = "Protocolo")] Manifesto manifesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manifesto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(manifesto);
        }
        // GET: manifestos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manifesto manifesto = db.Manifesto.Find(id);
            if (manifesto == null)
            {
                return HttpNotFound();
            }
            return View(manifesto);
        }
        // POST: manifestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //Confirmação de Exclusão
        public ActionResult DeleteConfirmed(int id)
        {
            
            Manifesto manifesto = db.Manifesto.Find(id);
            db.Manifesto.Remove(manifesto);
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

/*







// GET: Manifestos/Edit/5
public ActionResult Edit(int? id)
{
    if (id == null)
    {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    }
    Manifesto manifesto = db.Manifestos.Find(id);
    if (manifesto == null)
    {
        return HttpNotFound();
    }
    return View(manifesto);
}

// POST: Manifestos/Edit/5
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Edit([Bind(Include = "Protocolo,Nome,Email,Telefone,Celular,Perfil,Campus,Curso,TipoSolicitacao,Setor,Assunto,Manifestacao,DataCriacao")] Manifesto manifesto)
{
    if (ModelState.IsValid)
    {
        db.Entry(manifesto).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
    }
    return View(manifesto);
}


//Gera apenas detalhes
  public ActionResult Details(int? id)
  {
      if (id == null)
      {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Manifesto manifesto = db.Manifesto.Find(id);
      if (manifesto == null)
      {
          return HttpNotFound();
      }
      return View(manifesto);
  }*/