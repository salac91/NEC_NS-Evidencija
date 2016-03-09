using System;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Repositories;
using NEC_NS_Evidencija.Backend.DatabaseLayer;
using NEC_NS_Evidencija.Backend.Dto;
using NUnit.Framework;

namespace NEC_NS_Evidencija.Backend.DatabaseLayer.Test
{
    [TestFixture]
    public class CoachTest
    {

        [SetUp]
        public void SetUp()
        {

            TestUtils.TruncateDatabase();

        }

        [Test]
        public void CanReadCoach()
        {
            // Arange
            var dbFactory = new DatabaseFactory();
            var coachRepository = new CoachRepository(dbFactory);
            var unitOfWork = new UnitOfWork(dbFactory);

            coachRepository.Add(new COACH
            {
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
            });

            unitOfWork.Commit();

            // Act
            var coach = coachRepository.Get(c => c.COACH_INTERNAL_ID == "1");

            // Assert
            Assert.IsNotNull(coach, "coach doesn't exist");
        }
    }
}
