//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NEC_NS_Evidencija.Backend.DatabaseLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class MONTLYPAYMENT
    {
        public int MONTLYPAYMENT_ID { get; set; }
        public int PERSON_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public System.DateTime STARTDATE { get; set; }
        public System.DateTime ENDDATE { get; set; }
        public string MONTLYPAYMENT_INTERNAL_ID { get; set; }
    
        public virtual STUDENT STUDENT { get; set; }
    }
}
