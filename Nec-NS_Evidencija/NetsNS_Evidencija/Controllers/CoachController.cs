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
    public class CoachController : Controller
    {

        private Facade facade;

        public CoachController()
        {
            facade = new Facade();
        }

        // GET: /Coach/GetCoach

        [Authorize(Roles = "Mannager")]  
        public JsonResult GetCoach(string id)
        {
            var model = facade.GetCoach(id);

            return Json(model, JsonRequestBehavior.AllowGet);
        }   

        // GET: /Coach/GetAllCoaches

        [Authorize(Roles = "Mannager")]
        public JsonResult GetAllCoaches()
        {
            var model = facade.GetAllCoaches();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // POST: /Coach/CreateCoach

        [HttpPost]
        [Authorize(Roles = "Mannager")]
        public JsonResult CreateCoach(Coach coach)
        {
            facade.CreateCoach(coach);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        // DELETE: /Coach/DeleteCoach

        [HttpDelete]
        [Authorize(Roles = "Mannager")]
        public JsonResult DeleteCoach(string Id)
        {
            List<Training> allTrainings = facade.GetAllTrainings();

            foreach (Training training in allTrainings)
                if (training.Coach.Coach_Internal_Id.Trim() == Id.Trim())
                    return Json("error", JsonRequestBehavior.AllowGet);

            facade.DeleteCoach(Id);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        // Update: /Coach/UpdateCoach

        [HttpPost]
        [Authorize(Roles = "Mannager")]
        public JsonResult UpdateCoach(Coach coach)
        {
            facade.UpdateCoach(coach);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

    }
}