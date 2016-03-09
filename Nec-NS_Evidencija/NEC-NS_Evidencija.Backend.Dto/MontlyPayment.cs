using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC_NS_Evidencija.Backend.Dto
{
    public class MontlyPayment
    {

        public MontlyPayment()
        {
        }

        public MontlyPayment(String montlyPayment_Internal_Id,Student student, Decimal amount, String startDate, String endDate)
        {
            MontlyPayment_Internal_Id = montlyPayment_Internal_Id;

            Student = student;

            Amount = amount;

            StartDate = startDate;

            EndDate = endDate;
        }

        public MontlyPayment(String montlyPayment_Internal_Id,Decimal amount, String startDate, String endDate)
        {
            MontlyPayment_Internal_Id = montlyPayment_Internal_Id;

            Amount = amount;

            StartDate = startDate;

            EndDate = endDate;
        }

        public String MontlyPayment_Internal_Id { get; set; }

        public Student Student { get; set; }

        public Decimal Amount { get; set; }

        public String StartDate { get; set; }

        public String EndDate { get; set; }
    }
}
