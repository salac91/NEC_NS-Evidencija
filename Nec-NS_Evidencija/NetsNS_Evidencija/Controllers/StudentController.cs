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
    public class StudentController : Controller
    {

        private Facade facade;

        public StudentController()
        {
            facade = new Facade();
        }

        // GET: /Student/GetStudent

       [Authorize(Roles = "Mannager")]
        public JsonResult GetStudent(string studentId)
        {
            var model = facade.GetStudent(studentId);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: /Student/GetAllStudents

        [Authorize(Roles = "Mannager")]
        public JsonResult GetAllStudents()
        {
            var model = facade.GetAllStudents();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: /Student/GetAllStudentsForMontlyPayment

        [Authorize(Roles = "Mannager")]
        public JsonResult GetAllStudentsForMontlyPayment()
        {
            var model = facade.GetAllStudents();

            var modelTemp = facade.GetAllStudents(); 

            var montlyPayments = facade.GetAllMontlyPayments();

            foreach (Student student in modelTemp)
            {
                if (student.PaymentType.Trim() == "Po satu")
                {
                    Student studentPaymentPerHour = facade.GetStudent(student.Student_Internal_Id);
                    Student studentToRemove = model.Single(s => s.Student_Internal_Id == studentPaymentPerHour.Student_Internal_Id);
                    model.Remove(studentToRemove);
                }                    
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: /Student/GetAllPaymentPerHourStudents

        [Authorize(Roles = "Mannager")]
        public JsonResult GetAllPaymentPerHourStudents()
        {
            var model = facade.GetAllStudents();

            var modelTemp = facade.GetAllStudents();

            var montlyPayments = facade.GetAllMontlyPayments();

            foreach (Student student in modelTemp)
            {
                if (student.PaymentType.Trim() == "Mesecno")
                {
                    Student studentPaymentPerMonth = facade.GetStudent(student.Student_Internal_Id);
                    Student studentToRemove = model.Single(s => s.Student_Internal_Id == studentPaymentPerMonth.Student_Internal_Id);
                    model.Remove(studentToRemove);
                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // POST: /Student/CreateStudent

        [HttpPost]
        [Authorize(Roles = "Mannager")]
        public JsonResult CreateStudent(Student student)
        {
            facade.CreateStudent(student);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        // DELETE: /Student/DeleteStudent

        [HttpDelete]
        [Authorize(Roles = "Mannager")]
        public JsonResult DeleteStudent(string Id)
        {
            Student student = facade.GetStudent(Id);
            if (student.PaymentType.Trim() == "Mesecno")
                facade.DeleteMontlyPayment(Id);

            facade.deleteStudentInTraining(Id);

            facade.DeleteStudent(Id);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        // Update: /Student/UpdateStudent

        [HttpPost]
        [Authorize(Roles = "Mannager")]
        public JsonResult UpdateStudent(Student student)
        {
            facade.UpdateStudent(student);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

    }
}
