using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NEC_NS_Evidencija.Backend.Dto;

namespace NetsNS_Evidencija.Models
{
    public class MontlyPaymentModel
    {
        public MontlyPayment MontlyPayment { get; set; }
        public Student Student { get; set; }
    }
}