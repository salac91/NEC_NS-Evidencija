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
    public class TrainingController : Controller
    {

        private Facade facade;

        public TrainingController()
        {
            facade = new Facade();
        }

        // GET: /Training/GetTraining

        [Authorize(Roles = "Mannager")]
        public JsonResult GetTraining(string trainingId)
        {
            var model = facade.GetTraining(trainingId);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: /Training/GetAllTrainings

        [Authorize(Roles = "Mannager")]
        public JsonResult GetAllTrainings()
        {
            var model = facade.GetAllTrainings();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: /Training/GetAllStudentsInTraining

        [Authorize(Roles = "Mannager")]
        public JsonResult GetAllStudentsInTraining(string Id)
        {
            var model = facade.getAllStudentsInTraining(Id);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // POST: /Training/CreateTraining

        [HttpPost]
        [Authorize(Roles = "Mannager")]
        public JsonResult CreateTraining(TrainingModel trainingModel)
        {
            if (trainingModel.Coach == null)
                facade.CreateTraining(trainingModel.Training);             
            else
                facade.CreateTraining(trainingModel.Training, trainingModel.Coach);

            facade.TrainingScheduling(trainingModel.Students, trainingModel.Training);

            //if (trainingModel.Students != null)
            //{
            //    foreach (Student student in trainingModel.Students)
            //    {
            //        student.ToPay = student.ToPay + ((decimal)(trainingModel.Training.Duration * (double)trainingModel.Coach.PaymentRate) / trainingModel.Training.TrainingType);
            //        facade.UpdateStudent(student);
            //    }
            //}
            //trainingModel.Coach.PayOff = trainingModel.Coach.PayOff + (decimal)(trainingModel.Training.Duration * (double)trainingModel.Coach.PayOffRate);
            //facade.UpdateCoach(trainingModel.Coach);
            
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        // DELETE: /Training/DeleteTraining

        [HttpDelete]
        [Authorize(Roles = "Mannager")]
        public JsonResult DeleteTraining(string Id)
        {
            //Training training = facade.GetTraining(Id);
            //if (training.Coach != null)
            //{
            //    training.Coach.PayOff = training.Coach.PayOff - (decimal)(training.Duration * (double)training.Coach.PayOffRate);
            //    facade.UpdateCoach(training.Coach);
            //}
            //List<Student> studentsInTraining = facade.getAllStudentsInTraining(Id);
            //foreach (Student student in studentsInTraining)
            //{
            //    student.ToPay = student.ToPay - ((decimal)(training.Duration * (double)training.Coach.PaymentRate) / training.TrainingType);
            //    facade.UpdateStudent(student);
            //}
            facade.DeleteTraining(Id);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        // Update: /Training/UpdateTraining

        [HttpPost]
        [Authorize(Roles = "Mannager")]
        public JsonResult UpdateTraining(TrainingModel trainingModel)
        {
            facade.UpdateTraining(trainingModel.Training, trainingModel.Coach);

            facade.UpdateTrainingScheduling(trainingModel.Students, trainingModel.Training);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

    }
}
