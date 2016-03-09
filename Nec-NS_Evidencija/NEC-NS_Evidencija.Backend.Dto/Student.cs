using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC_NS_Evidencija.Backend.Dto
{
    public class Student
    {
        public Student()
        {
        }

        public Student(string student_Internal_Id, string firstName, string lastName, string email, string telephone,
            string paymenttype, double discount, string jmbg, string address, string bornDate, string country,
            string parentsFirstName, string parentsLastName, string parentsMail, string parentsTelephone, string imageUrl, Decimal toPay)
        {
            Student_Internal_Id = student_Internal_Id;

            FirstName = firstName;

            LastName = lastName;

            Email = email;

            Telephone = telephone;

            PaymentType = paymenttype;

            Discount = discount;

            Jmbg = jmbg;

            Address = address;

            BornDate = bornDate;

            Country = country;

            ParentFirstName = parentsFirstName;

            ParentLastName = parentsLastName;

            ParentsMail = parentsMail;

            ParentsTelephone = parentsTelephone;

            ImageUrl = imageUrl;

            ToPay = toPay;
        }

        public string Student_Internal_Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string PaymentType { get; set; }

        public double Discount { get; set; }

        public string Jmbg { get; set; }

        public string Address { get; set; }

        public string BornDate { get; set; }

        public string Country { get; set; }

        public string ParentFirstName { get; set; }

        public string ParentLastName { get; set; }

        public string ParentsMail { get; set; }

        public string ParentsTelephone { get; set; }

        public string ImageUrl { get; set; }

        public Decimal ToPay { get; set; }
    }
}
