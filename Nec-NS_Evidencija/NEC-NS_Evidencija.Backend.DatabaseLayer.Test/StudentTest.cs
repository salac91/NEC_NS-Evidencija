using System;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Repositories;
using NEC_NS_Evidencija.Backend.DatabaseLayer;
using NEC_NS_Evidencija.Backend.Dto;
using NUnit.Framework;

namespace NEC_NS_Evidencija.Backend.DatabaseLayer.Test
{
    [TestFixture]
    public class StudentTest
    {

        [SetUp]
        public void SetUp()
        {

            TestUtils.TruncateDatabase();

        }

        [Test]
        public void CanReadStudent()
        {
            // Arange
            var dbFactory = new DatabaseFactory();
            var studentRepository = new StudentRepository(dbFactory);
            var unitOfWork = new UnitOfWork(dbFactory);

            studentRepository.Add(new STUDENT
            {
               STUDENT_INTERNAL_ID = "1",
               DISCOUNT = 10.2M,
               PAYMENTTYPE = "Rate",
               PERSON = new PERSON {
                   FIRSTNAME = "Jovan",
                   LASTNAME = "Jovanovic",
                   EMAIL = "something@gmail.com",
                   TELEPHONE = "0644985665"
               }
            });

            unitOfWork.Commit();

            // Act
            var student = studentRepository.Get(s => s.STUDENT_INTERNAL_ID == "1");

            // Assert
            Assert.IsNotNull(student, "student doesn't exist");
        }
    }
}
