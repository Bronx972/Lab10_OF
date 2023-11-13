using Demo09C_Guerrero.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business;
using Entity;
using Data;
using System.Xml.Schema;

namespace Demo09C_Guerrero.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: InvoiceController
        public ActionResult Index()
        {
            
            BInvoice bInvoice = new BInvoice();
            List<invoices> invoicesEntity = bInvoice.GetInvoice();

            List<InvoiceModel> invoices = invoicesEntity.Select(x => new InvoiceModel
            {
                Id = x.invoice_id,
                Total = x.total,
                Igv = x.total * 0.18M,
            }).ToList();


            return View(invoices);
        }

        // GET: InvoiceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InvoiceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceModel model)
        {
            try
            {
                DInvoice dinvoice = new DInvoice();
                invoices invoice = new invoices
                {
                    customer_id = 3,
                    
                    total = model.Total,
                    date = DateTime.Now,
                };
                dinvoice.Put(invoice.customer_id,invoice.date, invoice.total);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InvoiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController/Delete/5
        public ActionResult Delete(int id)
        {
            BInvoice bInvoice = new BInvoice();
            invoices invoice = bInvoice.GetInvoice().Where(x => x.invoice_id == id).FirstOrDefault();

            InvoiceModel model = new InvoiceModel
            {
                Id = invoice.invoice_id,
                Total = invoice.total,
                Igv = invoice.total * 0.18M,

            };

            //el modelo por id
            return View(model);
        }

        // POST: InvoiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, InvoiceModel model)
        {
            try
            {
                BInvoice binvoice = new BInvoice();
                binvoice.DeleteOf(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
