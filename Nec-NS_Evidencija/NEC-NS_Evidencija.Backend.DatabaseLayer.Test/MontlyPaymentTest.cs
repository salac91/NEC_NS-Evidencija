using System;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Repositories;
using NEC_NS_Evidencija.Backend.DatabaseLayer;
using NEC_NS_Evidencija.Backend.Dto;
using NUnit.Framework;

namespace NEC_NS_Evidencija.Backend.DatabaseLayer.Test
{
    [TestFixture]
    public class MontlyPaymentTest
    {

        [SetUp]
        public void SetUp()
        {

            TestUtils.TruncateDatabase();

        }

        [Test]
        public void CanReadMontlyPayment()
        {
            // Arange
            var dbFactory = new DatabaseFactory();
            var montlyPaymentsRepository = new MontlyPaymentsRepository(dbFactory);
            var unitOfWork = new UnitOfWork(dbFactory);

            montlyPaymentsRepository.Add(new MONTLYPAYMENT
            {
                AMOUNT = 12000.20M,
                ENDDATE = System.DateTime.Now,
                STARTDATE = System.DateTime.Now,
                STUDENT = new STUDENT
                {
                    STUDENT_INTERNAL_ID = "1",
                    DISCOUNT = 10.2M,
                    PAYMENTTYPE = "Rate",
                    PERSON = new PERSON
                    {
                        FIRSTNAME = "Jovan",
                        LASTNAME = "Jovanovic",
                        EMAIL = "something@gmail.com",
                        TELEPHONE = "0644985665"
                    }
                }
            });

            unitOfWork.Commit();

            // Act
            var montlyPayment = montlyPaymentsRepository.Get(m => m.STUDENT.STUDENT_INTERNAL_ID == "1");

            // Assert
            Assert.IsNotNull(montlyPayment, "montly payment doesn't exist");
        }
    }
}
