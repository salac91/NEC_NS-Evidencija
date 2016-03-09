using NEC_NS_Evidencija.Backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetsNS_Evidencija.Models
{
    public class TrainingModel
    {
        public Training Training { get; set; }
        public Coach Coach { get; set; }
        public List<Student> Students { get; set; }
    }
}