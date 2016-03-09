using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC_NS_Evidencija.Backend.Dto
{
    public class Coach
    {

        public Coach()
        {
        }

        public Coach(string coach_Internal_Id, string firstName, string lastName, string email, string telephone,
            Decimal payOffRate, Decimal paymentRate, Decimal payOff)
        {
            Coach_Internal_Id = coach_Internal_Id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Telephone = telephone;
            PayOffRate = payOffRate;
            PaymentRate = paymentRate;
            PayOff = payOff;
        }

        public string Coach_Internal_Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public Decimal PayOffRate { get; set; }

        public Decimal PaymentRate { get; set; }

        public Decimal PayOff { get; set; }

    }
}
