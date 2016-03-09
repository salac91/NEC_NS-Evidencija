using System;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Repositories;
using NEC_NS_Evidencija.Backend.DatabaseLayer;
using NEC_NS_Evidencija.Backend.Dto;
using NUnit.Framework;

namespace NEC_NS_Evidencija.Backend.DatabaseLayer.Test
{
    [TestFixture]
    public class TrainingTest
    {

        [SetUp]
        public void SetUp()
        {

            TestUtils.TruncateDatabase();

        }

        [Test]
        public void CanReadTraining()
        {
            // Arange
            var dbFactory = new DatabaseFactory();
            var trainingRepository = new TrainingRepository(dbFactory);
            var unitOfWork = new UnitOfWork(dbFactory);

            trainingRepository.Add(new TRAINING
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
                
            });

            unitOfWork.Commit();

            // Act
            var training = trainingRepository.Get(t => t.TRAINING_INTERNAL_ID == "1");

            // Assert
            Assert.IsNotNull(training, "training doesn't exist");
        }
    }
}
