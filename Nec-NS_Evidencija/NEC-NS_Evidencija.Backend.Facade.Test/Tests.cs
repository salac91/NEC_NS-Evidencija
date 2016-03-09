using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common;
using NEC_NS_Evidencija.Backend.Dto;
using NUnit.Framework;

namespace NEC_NS_Evidencija.Backend.Facade.Test
{
    public class Tests
    {
        private Facade _facade;

        [SetUp]
        public void SetUp()
        {
            _facade = new Facade();

            TestUtils.TruncateDatabase();

        }

        [Test]
        public void CanFacadeReadStudent()
        {
            // Arrange
            _facade.CreateStudent(new Student("1","Jovan","Jovanovic","something@gmail.com","062132136","rate",0.2));

            // Act
            var student = _facade.GetStudent("1");

            // Assert
            Assert.IsNotNull(student, "student doesn't exist");
        }

        [Test]
        public void CanFacadeDeleteStudent()
        {
            // Arrange
            _facade.CreateStudent(new Student("1", "Jovan", "Jovanovic", "something@gmail.com", "062132136", "rate", 0.2));

            Assert.IsNotNull(_facade.GetStudent("1"));
            // Act
            _facade.DeleteStudent("1");

        }

        [Test]
        public void CanFacadeUpdateStudent()
        {
            // Arrange
            _facade.CreateStudent(new Student("1", "Jovan", "Jovanovic", "something@gmail.com", "062132136", "rate", 0.2));

            // Act
            _facade.UpdateStudent(new Student("1", "Zoran", "Jovanovic", "zoki@gmail.com", "062132136", "rate", 0.6));

            // Assert
            var dbStudent = _facade.GetStudent("1");
            Assert.IsNotNull(dbStudent);

            Assert.AreEqual(dbStudent.Student_Internal_Id,"1");
            Assert.AreEqual(dbStudent.Discount, 0.6);
            Assert.AreEqual(dbStudent.PaymentType, "rate");
            Assert.AreEqual(dbStudent.FirstName, "Zoran");
            Assert.AreEqual(dbStudent.LastName, "Jovanovic");
            Assert.AreEqual(dbStudent.Email, "zoki@gmail.com");
            Assert.AreEqual(dbStudent.Telephone, "062132136");
        }

        [Test]
        public void CanFacadeReadTrainingNoCoach()
        {
            // Arrange
            _facade.CreateTraining(new Training("1", "something", System.DateTime.Now.ToString(), "something", 10.2f, "something"));

            // Act
            var training = _facade.GetTraining("1");

            // Assert
            Assert.IsNotNull(training, "training doesn't exist");
        }

        [Test]
        public void CanFacadeReadTraining()
        {
            // Arrange
            _facade.CreateTraining(new Training("1", "something", System.DateTime.Now.ToString(), "something", 10.2f, "something"), new Coach("1", "Jovan", "Jovanovic", "something@gmail.com", "062132136", 120.20m, 140));

            // Act
            var training = _facade.GetTraining("1");

            // Assert
            Assert.IsNotNull(training, "training doesn't exist");
        }

        [Test]
        public void CanFacadeDeleteTraining()
        {
            // Arrange
            _facade.CreateTraining(new Training("1", "something", System.DateTime.Now.ToString(), "something", 10.2f, "something"), new Coach("1", "Jovan", "Jovanovic", "something@gmail.com", "062132136", 120.20m, 140));

            Assert.IsNotNull(_facade.GetTraining("1"));
            // Act
            _facade.DeleteTraining("1");

        }

        [Test]
        public void CanFacadeUpdateTraining()
        {
            // Arrange
            _facade.CreateTraining(new Training("1", "something", System.DateTime.Now.ToString(), "something", 10.2f, "something"), new Coach("1", "Jovan", "Jovanovic", "something@gmail.com", "062132136", 120.20m, 140));

            // Act
            _facade.UpdateTraining(new Training("1", "something2", System.DateTime.Now.ToString(), "something3", 10.2f, "something"));

            // Assert
            var dbTraining = _facade.GetTraining("1");
            Assert.IsNotNull(dbTraining);

            Assert.AreEqual(dbTraining.Training_Internal_Id, 1);
            Assert.AreEqual(dbTraining.Notes, "something");
            Assert.AreEqual(dbTraining.TrainingTheme, "something3");
            Assert.AreEqual(dbTraining.TrainingType, "something2");
            Assert.AreEqual(dbTraining.Duration, 10.2f);
           
        }

        [Test]
        public void CanFacadReadCoach()
        {
            // Arrange
            _facade.CreateCoach(new Coach("1", "Jovan", "Jovanovic", "something@gmail.com", "062132136", 120.20m, 140));

            // Act
            var coach = _facade.GetCoach("1");

            // Assert
            Assert.IsNotNull(coach, "coach doesn't exist");
        }

        [Test]
        public void CanFacadeUpdateCoach()
        {
            // Arrange
            _facade.CreateCoach(new Coach("1", "Jovan", "Jovanovic", "something@gmail.com", "062132136", 120.2m, 140));

            // Act
            _facade.UpdateCoach(new Coach("1", "Goran", "Goranovic", "something@gmail.com", "062132136", 120.2m, 140));

            // Assert
            var dbCoach = _facade.GetCoach("1");
            Assert.IsNotNull(dbCoach);

            Assert.AreEqual(dbCoach.Coach_Internal_Id, "1");
            Assert.AreEqual(dbCoach.PaymentRate, 140);
            Assert.AreEqual(dbCoach.PayOffRate, 120.2);
            Assert.AreEqual(dbCoach.FirstName, "Goran");
            Assert.AreEqual(dbCoach.LastName, "Goranovic");
            Assert.AreEqual(dbCoach.Email, "something@gmail.com");
            Assert.AreEqual(dbCoach.Telephone, "062132136");
        }

        [Test]
        public void CanFacadeDeleteCoach()
        {
            // Arrange
            _facade.CreateCoach(new Coach("1", "Jovan", "Jovanovic", "something@gmail.com", "062132136", 120.2m, 140));

            Assert.IsNotNull(_facade.GetCoach("1"));
            // Act
            _facade.DeleteCoach("1");

        }

        [Test]
        public void CanFacadeReadMontlyPayment()
        {
            // Arrange
            _facade.CreateMontlyPayment(new MontlyPayment(120000.20M, System.DateTime.Now.ToShortDateString(), System.DateTime.Now.ToShortDateString()),
                new Student("1", "Jovan", "Jovanovic", "something@gmail.com", "062132136", "rate", 0.2));

            // Act
            var montlyPayment = _facade.GetMontlyPayment("1");

            // Assert
            Assert.IsNotNull(montlyPayment, "montlyPayment doesn't exist");

        }

        [Test]
        public void CanFacadeUpdateMontlyPayment()
        {
            // Arrange
            _facade.CreateMontlyPayment(new MontlyPayment(120000.20M, System.DateTime.Now.ToShortDateString(), System.DateTime.Now.ToShortDateString()), new Student("1", "Miki", "Mikic", "miki@gmail.com", "063213312", "Rate", 0.2));

            // Act
            _facade.UpdateMontlyPayment(new MontlyPayment(150000.20M, System.DateTime.Now.ToShortDateString(), System.DateTime.Now.ToShortDateString()));

            // Assert
            var dbMontlyPayment = _facade.GetMontlyPayment("1");
            Assert.IsNotNull(dbMontlyPayment);

            Assert.AreEqual(dbMontlyPayment.Amount, 150000.20M);
                 
        }

        [Test]
        public void CanFacadeCreateTrainingScheduling()
        {
             // Arrange
            List<Student> students = new List<Student>();
            students.Add(new Student("1", "Miki", "Mikic", "miki@gmail.com", "063213312", "Rate", 0.2));
            students.Add(new Student("2", "Zoki", "Zokic", "zoki@gmail.com", "063212312", "Mesecno", 0.4));
            students.Add(new Student("3", "Boki", "Bokic", "boki@gmail.com", "063211312", "Rate", 0.7));

            Training training = new Training("1", "something", System.DateTime.Now.ToString(), "something", 4.2f, "something");

            Coach coach = new Coach("1", "Jovan", "Jovanovic", "something@gmail.com", "062132136", 120.2m, 140);

            // Act
            _facade.TrainingScheduling(students, training, coach);

        }

        [Test]
        public void CanFacadeCreateTrainingSchedulingNoCoach()
        {
            // Arrange
            List<Student> students = new List<Student>();
            students.Add(new Student("1", "Miki", "Mikic", "miki@gmail.com", "063213312", "Rate", 0.2));
            students.Add(new Student("2", "Zoki", "Zokic", "zoki@gmail.com", "063212312", "Mesecno", 0.4));
            students.Add(new Student("3", "Boki", "Bokic", "boki@gmail.com", "063211312", "Rate", 0.7));

            Training training = new Training("1", "something", System.DateTime.Now.ToString(), "something", 4.2f, "something");

            // Act
            _facade.TrainingScheduling(students, training);

        }

    }
}
