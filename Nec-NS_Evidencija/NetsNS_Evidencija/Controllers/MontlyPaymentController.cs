using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEC_NS_Evidencija.Backend.Facade;
using NetsNS_Evidencija.Models;
using NEC_NS_Evidencija.Backend.Dto;

namespace NetsNS_Evidencija.Controllers
{
    public class MontlyPaymentController : Controller
    {

        private Facade facade;

        public MontlyPaymentController()
        {
            facade = new Facade();
        }

        // GET: /MontlyPayment/GetMontlyPayment

        [Authorize(Roles = "Mannager")]
        public JsonResult GetMontlyPayment(string studentId)
        {
            var model = facade.GetMontlyPayment(studentId);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: /MontlyPayment/GetAllMontlyPayments

        [Authorize(Roles = "Mannager")]
        public JsonResult GetAllMontlyPayments()
        {
            var model = facade.GetAllMontlyPayments();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // POST: /MontlyPayment/CreateMontlyPayment

        [HttpPost]
        [Authorize(Roles = "Mannager")]
        public JsonResult CreateMontlyPayment(MontlyPaymentModel montlyPaymentModel)
        {        
            facade.CreateMontlyPayment(montlyPaymentModel.MontlyPayment, montlyPaymentModel.Student);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        // DELETE: /MontlyPayment/DeleteMontlyPayment

        [HttpDelete]
        [Authorize(Roles = "Mannager")]
        public JsonResult DeleteMontlyPayment(string Id)
        {
            facade.DeleteMontlyPayment(Id);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        // Update: /MontlyPayment/UpdateMontlyPayment

        [HttpPost]
        [Authorize(Roles = "Mannager")]
        public JsonResult UpdateMontlyPayment(MontlyPaymentModel montlyPaymentModel)
        {
            montlyPaymentModel.MontlyPayment.Student = montlyPaymentModel.Student;
            facade.UpdateMontlyPayment(montlyPaymentModel.MontlyPayment);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

    }
}
