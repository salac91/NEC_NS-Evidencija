using System;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Repositories;
using NEC_NS_Evidencija.Backend.DatabaseLayer;
using NEC_NS_Evidencija.Backend.Dto;
using NUnit.Framework;

namespace NEC_NS_Evidencija.Backend.DatabaseLayer.Test
{
    [TestFixture]
    public class StudentTrainingTest
    {

        [SetUp]
        public void SetUp()
        {

            TestUtils.TruncateDatabase();

        }
        
        
        [Test]
        public void CanReadStudentTraining()
        {
            // Arange
            var dbFactory = new DatabaseFactory();
            var studentTrainingRepository = new StudentTrainingRepository(dbFactory);
            var unitOfWork = new UnitOfWork(dbFactory);

            studentTrainingRepository.Add(new STUDENT_TRAINING
            {
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
                },
                TRAINING = new TRAINING
                {
                    TRAINING_INTERNAL_ID = "1",
                    DURATION = 3,
                    NOTES = "something",
                    TRAININGTHEME = "something",
                    TRAININGTYPE = "something",
                    STARTDATE = System.DateTime.Now,
                    COACH = new COACH {
                        COACH_INTERNAL_ID = "1",
                        PAYMENTRATE = 120.2M,
                        PAYOFFRATE = 100.2M,
                        PERSON = new PERSON
                        {
                            FIRSTNAME = "Milan",
                            LASTNAME = "Milanovic",
                            EMAIL = "something@gmail.com",
                            TELEPHONE = "0644325665"
                        }
                    }
                },
                AMOUNT = 12000,
                PAYMENTTYPE = "something",
                RELATIONSHIP = "something",
                TRAININGQUALITY = "4+"
            });

            unitOfWork.Commit();

            // Act
            var student_training = studentTrainingRepository.Get(s => s.STUDENT.STUDENT_INTERNAL_ID == "1");

            // Assert
            Assert.IsNotNull(student_training, "student_training doesn't exist");
        }
        
    }
}
