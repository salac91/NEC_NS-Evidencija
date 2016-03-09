using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Repositories;
using NEC_NS_Evidencija.Backend.Dto;
using NEC_NS_Evidencija.Backend.DatabaseLayer;
using AutoMapper;

namespace NEC_NS_Evidencija.Backend.DatabaseService
{
    public class Service : IService
    {
        protected DatabaseFactory _dbFactory;
        protected IStudentRepository _studentRepo;
        protected ICoachRepository _coachRepo;
        protected ITrainingRepository _trainingRepo;
        protected IMontlyPaymentsRepository _montlyPaymentRepo;
        protected IStudentTrainingRepository _studentTrainingRepo;
        protected IPersonRepository _personRepo;
        protected IUnitOfWork _unitOfWork;

        public Service(IStudentRepository studentRepo,ICoachRepository coachRepo, ITrainingRepository trainingRepo, IMontlyPaymentsRepository montlyPaymentRepo, 
            IStudentTrainingRepository studentTrainingRepo, IPersonRepository personRepo, IUnitOfWork unitOfWork)
        {
            _studentRepo = studentRepo;
            _coachRepo = coachRepo;
            _trainingRepo = trainingRepo;
            _montlyPaymentRepo = montlyPaymentRepo;
            _studentTrainingRepo = studentTrainingRepo;
            _personRepo = personRepo;
            _unitOfWork = unitOfWork;
        }


        // ----------------- CRUD FOR STUDENT ----------------------
        public Student GetStudent(string studentInternalId)
        {
            var dbStudent = _studentRepo.Get(s => s.STUDENT_INTERNAL_ID == studentInternalId);
         
            var student = new Student(dbStudent.STUDENT_INTERNAL_ID, dbStudent.PERSON.FIRSTNAME, dbStudent.PERSON.LASTNAME, dbStudent.PERSON.EMAIL,
                dbStudent.PERSON.TELEPHONE, dbStudent.PAYMENTTYPE, (double)dbStudent.DISCOUNT, dbStudent.JMBG, dbStudent.ADDRESS, dbStudent.BORN_DATE.ToString(),
                dbStudent.COUNTRY, dbStudent.PARENT_FIRST_NAME, dbStudent.PARENT_LAST_NAME, dbStudent.PARENTS_MAIL, dbStudent.PARENTS_TELEPHONE, dbStudent.IMAGE_URL, (Decimal) dbStudent.ToPay);
            
            return student;
        }

        public List<Student> GetAllStudents()
        {
            var dbstudents = _studentRepo.GetAll();

            List<Student> students = new List<Student>();

            foreach (STUDENT dbStudent in dbstudents)
            {              
                    students.Add(new Student(dbStudent.STUDENT_INTERNAL_ID, dbStudent.PERSON.FIRSTNAME, dbStudent.PERSON.LASTNAME, dbStudent.PERSON.EMAIL,
                    dbStudent.PERSON.TELEPHONE, dbStudent.PAYMENTTYPE, (double)dbStudent.DISCOUNT, dbStudent.JMBG, dbStudent.ADDRESS, dbStudent.BORN_DATE.ToString(),
                    dbStudent.COUNTRY, dbStudent.PARENT_FIRST_NAME, dbStudent.PARENT_LAST_NAME, dbStudent.PARENTS_MAIL, dbStudent.PARENTS_TELEPHONE, dbStudent.IMAGE_URL,(Decimal) dbStudent.ToPay));             
            }

            return students;
        }

        public void CreateStudent(Student student)
        {
       
            var dbStudent = new STUDENT();
            dbStudent.DISCOUNT = (Decimal) student.Discount;
            dbStudent.PAYMENTTYPE = student.PaymentType;
            dbStudent.STUDENT_INTERNAL_ID = student.Student_Internal_Id;
            dbStudent.ToPay = 0;
            dbStudent.IMAGE_URL = "../Content/Images/Unknown-person.gif";
            dbStudent.PERSON = new PERSON
            {
                FIRSTNAME = student.FirstName,
                LASTNAME = student.LastName,
                EMAIL = student.Email,
                TELEPHONE = student.Telephone
            };
         
            _studentRepo.Add(dbStudent);

            _unitOfWork.Commit();
        }

        public void DeleteStudent(string studentInternalId)
        {
             var dbStudent = _studentRepo.Get(s => s.STUDENT_INTERNAL_ID == studentInternalId);
            _studentRepo.Delete(s => s.STUDENT_INTERNAL_ID == studentInternalId);
            _personRepo.Delete(p => p.PERSON_ID == dbStudent.PERSON_ID);
            _unitOfWork.Commit();
           
        }

        public void UpdateStudent(NEC_NS_Evidencija.Backend.Dto.Student student)
        {
            var dbStudent = _studentRepo.Get(s => s.STUDENT_INTERNAL_ID == student.Student_Internal_Id);
            dbStudent.DISCOUNT = (Decimal)student.Discount;
            dbStudent.PAYMENTTYPE = student.PaymentType;
            dbStudent.PERSON.FIRSTNAME = student.FirstName;
            dbStudent.PERSON.LASTNAME = student.LastName;
            dbStudent.PERSON.EMAIL = student.Email;
            dbStudent.PERSON.TELEPHONE = student.Telephone;

            dbStudent.JMBG = student.Jmbg;
            dbStudent.ADDRESS = student.Address;   
            
            dbStudent.BORN_DATE = DateTime.ParseExact(student.BornDate, "yyyy-MM-dd",
                            System.Globalization.CultureInfo.InvariantCulture);
           
            dbStudent.COUNTRY = student.Country;
            dbStudent.IMAGE_URL = student.ImageUrl;
            dbStudent.PARENT_FIRST_NAME = student.ParentFirstName;
            dbStudent.PARENT_LAST_NAME = student.ParentLastName;
            dbStudent.PARENTS_MAIL = student.ParentsMail;
            dbStudent.PARENTS_TELEPHONE = student.ParentsTelephone;
            dbStudent.ToPay = student.ToPay;
          
            _studentRepo.Update(dbStudent);
            _unitOfWork.Commit();
        }

        // ----------------- CRUD FOR COACH ----------------------
        public Coach GetCoach(string coachInternalId)
        {
            var dbCoach = _coachRepo.Get(c => c.COACH_INTERNAL_ID == coachInternalId);

            var coach = new Coach(dbCoach.COACH_INTERNAL_ID, dbCoach.PERSON.FIRSTNAME, dbCoach.PERSON.LASTNAME, dbCoach.PERSON.EMAIL, dbCoach.PERSON.TELEPHONE,
                dbCoach.PAYOFFRATE, dbCoach.PAYMENTRATE, dbCoach.PAYOFFRATE);

            return coach;
        }

        public List<Coach> GetAllCoaches()
        {
            var dbcoaches = _coachRepo.GetAll();

            List<Coach> coaches = new List<Coach>();

            foreach (COACH dbcoach in dbcoaches)
            {
                coaches.Add(new Coach(dbcoach.COACH_INTERNAL_ID, dbcoach.PERSON.FIRSTNAME, dbcoach.PERSON.LASTNAME, dbcoach.PERSON.EMAIL,
                dbcoach.PERSON.TELEPHONE, dbcoach.PAYOFFRATE, dbcoach.PAYMENTRATE, (Decimal) dbcoach.PAYOFF));
            }

            return coaches;
        }

        public void CreateCoach(Coach coach)
        {
            var dbCoach = new COACH();
            dbCoach.PAYMENTRATE = coach.PaymentRate;
            dbCoach.PAYOFFRATE = coach.PayOffRate;
            dbCoach.COACH_INTERNAL_ID = coach.Coach_Internal_Id;
            dbCoach.PAYOFF = 0;
            dbCoach.PERSON = new PERSON
            {
                FIRSTNAME = coach.FirstName,
                LASTNAME = coach.LastName,
                EMAIL = coach.Email,
                TELEPHONE = coach.Telephone
            };

            _coachRepo.Add(dbCoach);

            _unitOfWork.Commit();
        }

        public void DeleteCoach(string coachInternalId)
        {
            var dbCoach = _coachRepo.Get(c => c.COACH_INTERNAL_ID == coachInternalId);
            _coachRepo.Delete(c => c.COACH_INTERNAL_ID == coachInternalId);
            _personRepo.Delete(p => p.PERSON_ID == dbCoach.PERSON_ID);
            _unitOfWork.Commit();
        }

        public void UpdateCoach(NEC_NS_Evidencija.Backend.Dto.Coach coach)
        {
            var dbCoach = _coachRepo.Get(c => c.COACH_INTERNAL_ID == coach.Coach_Internal_Id);
            dbCoach.PAYMENTRATE =  coach.PaymentRate;
            dbCoach.PAYOFFRATE = coach.PayOffRate;
            dbCoach.PERSON.FIRSTNAME = coach.FirstName;
            dbCoach.PERSON.LASTNAME = coach.LastName;
            dbCoach.PERSON.EMAIL = coach.Email;
            dbCoach.PERSON.TELEPHONE = coach.Telephone;
            dbCoach.PAYOFF = coach.PayOff;

            _coachRepo.Update(dbCoach);
            _unitOfWork.Commit();
        }

        // ----------------- CRUD FOR TRAINING ----------------------
        public Training GetTraining(string trainingInternalId)
        {
            var dbTraining = _trainingRepo.Get(t => t.TRAINING_INTERNAL_ID == trainingInternalId);
            Coach coach = null;
            Training training = null;

            if (dbTraining.COACH != null)
            {
                coach = new Coach(dbTraining.COACH.COACH_INTERNAL_ID, dbTraining.COACH.PERSON.FIRSTNAME, dbTraining.COACH.PERSON.LASTNAME,
                   dbTraining.COACH.PERSON.EMAIL, dbTraining.COACH.PERSON.TELEPHONE, dbTraining.COACH.PAYOFFRATE, dbTraining.COACH.PAYMENTRATE, (Decimal) dbTraining.COACH.PAYOFF);

                training = new Training(dbTraining.TRAINING_INTERNAL_ID, coach, dbTraining.TRAININGTYPE, dbTraining.STARTDATE.ToShortDateString(),
                   dbTraining.TRAININGTHEME, (double)dbTraining.DURATION, dbTraining.NOTES, dbTraining.TRAINING_QUALITY, dbTraining.RELATIONSHIP);
            }
            else
            {
                training = new Training(dbTraining.TRAINING_INTERNAL_ID, dbTraining.TRAININGTYPE, dbTraining.STARTDATE.ToShortDateString(),
                  dbTraining.TRAININGTHEME, (double)dbTraining.DURATION, dbTraining.NOTES, dbTraining.TRAINING_QUALITY, dbTraining.RELATIONSHIP);
            }
    
                return training;
        }

        public List<Training> GetAllTrainings()
        {
            var dbtrainings = _trainingRepo.GetAll();

            List<Training> trainings = new List<Training>();
            Coach coach = null;
            foreach (TRAINING dbtraining in dbtrainings)
            {
                if (dbtraining.COACH != null) coach = new Coach(dbtraining.COACH.COACH_INTERNAL_ID, dbtraining.COACH.PERSON.FIRSTNAME,
                     dbtraining.COACH.PERSON.LASTNAME, dbtraining.COACH.PERSON.EMAIL, dbtraining.COACH.PERSON.TELEPHONE, dbtraining.COACH.PAYOFFRATE,
                     dbtraining.COACH.PAYMENTRATE,(Decimal) dbtraining.COACH.PAYOFF);
                trainings.Add(new Training(dbtraining.TRAINING_INTERNAL_ID, coach, dbtraining.TRAININGTYPE, dbtraining.STARTDATE.ToShortDateString(), dbtraining.TRAININGTHEME, (double)dbtraining.DURATION, dbtraining.NOTES,
                    dbtraining.TRAINING_QUALITY, dbtraining.RELATIONSHIP));
            }

            return trainings;
        }

        public void CreateTraining(Training training,Coach coach)
        {

            var dbCoach = _coachRepo.Get(c => c.COACH_INTERNAL_ID == coach.Coach_Internal_Id);

            var dbTraining = new TRAINING();
            dbTraining.TRAINING_INTERNAL_ID = training.Training_Internal_Id;
            dbTraining.DURATION = training.Duration;
            dbTraining.NOTES = training.Notes;
            dbTraining.STARTDATE = DateTime.ParseExact(training.StartDate, "yyyy-MM-dd",
                                     System.Globalization.CultureInfo.InvariantCulture);
            dbTraining.TRAININGTHEME = training.TrainingTheme;
            dbTraining.TRAININGTYPE = training.TrainingType;
            dbTraining.TRAINING_QUALITY = training.TrainingQuality;
            dbTraining.RELATIONSHIP = training.Relationship;
            dbTraining.COACH = dbCoach;

            _trainingRepo.Add(dbTraining);

            _unitOfWork.Commit();
        }

        public void CreateTraining(Training training)
        {
          
            var dbTraining = new TRAINING();
            dbTraining.TRAINING_INTERNAL_ID = training.Training_Internal_Id;
            dbTraining.DURATION = training.Duration;
            dbTraining.NOTES = training.Notes;
            dbTraining.STARTDATE = DateTime.ParseExact(training.StartDate, "yyyy-MM-dd",
                                     System.Globalization.CultureInfo.InvariantCulture);
            dbTraining.TRAININGTHEME = training.TrainingTheme;
            dbTraining.TRAININGTYPE = training.TrainingType;
            dbTraining.TRAINING_QUALITY = training.TrainingQuality;
            dbTraining.RELATIONSHIP = training.Relationship;
        
            _trainingRepo.Add(dbTraining);

            _unitOfWork.Commit();
        }

        public void DeleteTraining(string trainingInternalId)
        {
            _studentTrainingRepo.Delete(st => st.TRAINING.TRAINING_INTERNAL_ID == trainingInternalId);
            _trainingRepo.Delete(t => t.TRAINING_INTERNAL_ID == trainingInternalId);
            _unitOfWork.Commit();
        }

        public void UpdateTraining(NEC_NS_Evidencija.Backend.Dto.Training training, Coach coach)
        {

            var dbTraining = _trainingRepo.Get(t => t.TRAINING_INTERNAL_ID == training.Training_Internal_Id);
            
            COACH dbCoach = null;
            if(coach != null)
                dbCoach = _coachRepo.Get(c => c.COACH_INTERNAL_ID == coach.Coach_Internal_Id);

            dbTraining.DURATION = training.Duration;
            dbTraining.NOTES = training.Notes;
            dbTraining.STARTDATE = DateTime.ParseExact(training.StartDate, "yyyy-MM-dd",
                                     System.Globalization.CultureInfo.InvariantCulture);
            if(dbCoach != null)
            dbTraining.COACH = dbCoach;
            dbTraining.TRAININGTHEME = training.TrainingTheme;
            dbTraining.TRAININGTYPE = training.TrainingType;
            dbTraining.TRAINING_QUALITY = training.TrainingQuality;
            dbTraining.RELATIONSHIP = training.Relationship;

            _trainingRepo.Update(dbTraining);
            _unitOfWork.Commit();
        }

        // ----------------- CRUD FOR MONTLYPAYMENT ----------------------

        public MontlyPayment GetMontlyPayment(string montlyPaymentInternalId)
        {
            var dbMontlyPayment = _montlyPaymentRepo.Get(m => m.MONTLYPAYMENT_INTERNAL_ID == montlyPaymentInternalId);

            var student = new Student(dbMontlyPayment.STUDENT.STUDENT_INTERNAL_ID, dbMontlyPayment.STUDENT.PERSON.FIRSTNAME, dbMontlyPayment.STUDENT.PERSON.LASTNAME,
                dbMontlyPayment.STUDENT.PERSON.EMAIL, dbMontlyPayment.STUDENT.PERSON.TELEPHONE, dbMontlyPayment.STUDENT.PAYMENTTYPE, (double)dbMontlyPayment.STUDENT.DISCOUNT, dbMontlyPayment.STUDENT.JMBG, dbMontlyPayment.STUDENT.ADDRESS, dbMontlyPayment.STUDENT.BORN_DATE.ToString(),
                dbMontlyPayment.STUDENT.COUNTRY, dbMontlyPayment.STUDENT.PARENT_FIRST_NAME, dbMontlyPayment.STUDENT.PARENT_LAST_NAME, dbMontlyPayment.STUDENT.PARENTS_MAIL, dbMontlyPayment.STUDENT.PARENTS_TELEPHONE, dbMontlyPayment.STUDENT.IMAGE_URL, (Decimal) dbMontlyPayment.STUDENT.ToPay);

            var montlyPayment = new MontlyPayment(dbMontlyPayment.MONTLYPAYMENT_INTERNAL_ID, student, dbMontlyPayment.AMOUNT, dbMontlyPayment.STARTDATE.ToShortDateString(), dbMontlyPayment.ENDDATE.ToShortDateString());

            return montlyPayment;
        }

        public List<MontlyPayment> GetAllMontlyPayments()
        {
            var dbmontly_payments = _montlyPaymentRepo.GetAll();

            List<MontlyPayment> montly_payments = new List<MontlyPayment>();
            Student student = null;

            foreach (MONTLYPAYMENT dbmontly_payment in dbmontly_payments)
            {
                if (dbmontly_payment.STUDENT != null) student = new Student(dbmontly_payment.STUDENT.STUDENT_INTERNAL_ID, dbmontly_payment.STUDENT.PERSON.FIRSTNAME,
                     dbmontly_payment.STUDENT.PERSON.LASTNAME, dbmontly_payment.STUDENT.PERSON.EMAIL, dbmontly_payment.STUDENT.PERSON.TELEPHONE, dbmontly_payment.STUDENT.PAYMENTTYPE,
                     (double)dbmontly_payment.STUDENT.DISCOUNT, dbmontly_payment.STUDENT.JMBG, dbmontly_payment.STUDENT.ADDRESS, dbmontly_payment.STUDENT.BORN_DATE.ToString(),
                dbmontly_payment.STUDENT.COUNTRY, dbmontly_payment.STUDENT.PARENT_FIRST_NAME, dbmontly_payment.STUDENT.PARENT_LAST_NAME, dbmontly_payment.STUDENT.PARENTS_MAIL, dbmontly_payment.STUDENT.PARENTS_TELEPHONE, dbmontly_payment.STUDENT.IMAGE_URL, (Decimal) dbmontly_payment.STUDENT.ToPay);
                montly_payments.Add(new MontlyPayment(dbmontly_payment.MONTLYPAYMENT_INTERNAL_ID,student, dbmontly_payment.AMOUNT, dbmontly_payment.STARTDATE.ToShortDateString(), dbmontly_payment.ENDDATE.ToShortDateString()));
            }

            return montly_payments;
        }

        public void CreateMontlyPayment(NEC_NS_Evidencija.Backend.Dto.MontlyPayment montlyPayment, Student student)
        {
            var dbstudent = _studentRepo.Get(s => s.STUDENT_INTERNAL_ID == student.Student_Internal_Id);
            var dbMontlyPayment = new MONTLYPAYMENT
            {
                MONTLYPAYMENT_INTERNAL_ID = montlyPayment.MontlyPayment_Internal_Id,
                AMOUNT = montlyPayment.Amount,
                ENDDATE = DateTime.ParseExact(montlyPayment.EndDate, "yyyy-MM-dd",
                                     System.Globalization.CultureInfo.InvariantCulture),
                STARTDATE = DateTime.ParseExact(montlyPayment.StartDate, "yyyy-MM-dd",
                                     System.Globalization.CultureInfo.InvariantCulture),
                STUDENT = dbstudent
            };   

            _montlyPaymentRepo.Add(dbMontlyPayment);
            _unitOfWork.Commit();
        }

        public void DeleteMontlyPayment(string montlyPaymentInternalId)
        {
            _montlyPaymentRepo.Delete(m => m.MONTLYPAYMENT_INTERNAL_ID == montlyPaymentInternalId);
            _unitOfWork.Commit();
        }

 
        public void UpdateMontlyPayment(NEC_NS_Evidencija.Backend.Dto.MontlyPayment montlyPayment)
        {

            var dbMontlyPayment = _montlyPaymentRepo.Get(m => m.MONTLYPAYMENT_INTERNAL_ID == montlyPayment.MontlyPayment_Internal_Id);
            dbMontlyPayment.MONTLYPAYMENT_INTERNAL_ID = montlyPayment.MontlyPayment_Internal_Id;
            dbMontlyPayment.AMOUNT =  montlyPayment.Amount;
            dbMontlyPayment.ENDDATE = DateTime.ParseExact(montlyPayment.EndDate, "yyyy-MM-dd",
                                     System.Globalization.CultureInfo.InvariantCulture);
            dbMontlyPayment.STARTDATE = DateTime.ParseExact(montlyPayment.StartDate, "yyyy-MM-dd",
                                     System.Globalization.CultureInfo.InvariantCulture);
             
            _montlyPaymentRepo.Update(dbMontlyPayment);
            _unitOfWork.Commit();
        }

        // ----------------- TRAINING SCHEDULING ----------------------
        public void TrainingScheduling(List<Student> students, Training training, Coach coach)
        {
             
            foreach (Student student in students)
            {

                _studentTrainingRepo.Add(new STUDENT_TRAINING
                {
                    STUDENT = new STUDENT
                    {
                        DISCOUNT = (Decimal)student.Discount,
                        PAYMENTTYPE = student.PaymentType,
                        PERSON = new PERSON
                        {
                            FIRSTNAME = student.FirstName,
                            LASTNAME = student.LastName,
                            EMAIL = student.Email,
                            TELEPHONE = student.Telephone
                        }
                    },
                    TRAINING = new TRAINING
                    {
                        TRAINING_INTERNAL_ID = training.Training_Internal_Id,
                        DURATION = training.Duration,
                        NOTES = training.Notes,
                        STARTDATE = DateTime.ParseExact(training.StartDate, "yyyy-MM-dd",
                                     System.Globalization.CultureInfo.InvariantCulture),
                        TRAININGTHEME = training.TrainingTheme,
                        TRAININGTYPE = training.TrainingType,
                        COACH = new COACH 
                        {
                             PAYMENTRATE = coach.PaymentRate,
                             PAYOFFRATE = coach.PayOffRate,
                             COACH_INTERNAL_ID = coach.Coach_Internal_Id,
                             PERSON = new PERSON
                             {
                                  FIRSTNAME = coach.FirstName,
                                  LASTNAME = coach.LastName,
                                  EMAIL = coach.Email,
                                  TELEPHONE = coach.Telephone
                             } 

                        }
                    },
                    AMOUNT = 100,
                    PAYMENTTYPE = "mesecno",
                    RELATIONSHIP = "something",
                    TRAININGQUALITY = "something"
                });
            }
            _unitOfWork.Commit();
        }

        public void TrainingScheduling(List<Student> students, Training training)
        {
            var dbTraining = _trainingRepo.Get(t => t.TRAINING_INTERNAL_ID == training.Training_Internal_Id);
            if (students != null)
            {
                foreach (Student student in students)
                {
                    STUDENT dbstudent = _studentRepo.Get(s => s.STUDENT_INTERNAL_ID == student.Student_Internal_Id);

                    _studentTrainingRepo.Add(new STUDENT_TRAINING
                    {
                        STUDENT = dbstudent,
                        TRAINING = dbTraining,
                        AMOUNT = 100,
                        PAYMENTTYPE = "mesecno",
                        RELATIONSHIP = "something",
                        TRAININGQUALITY = "something"
                    });
                }
            }
            _unitOfWork.Commit();
        }

        public void UpdateTrainingScheduling(List<Student> students, Training training)
        {

            var dbTraining = _trainingRepo.Get(t => t.TRAINING_INTERNAL_ID == training.Training_Internal_Id);

            _studentTrainingRepo.Delete(ts => ts.TRAINING_ID == dbTraining.TRAINING_ID); 

            if (students != null)
            {
                foreach (Student student in students)
                {
                    STUDENT dbstudent = _studentRepo.Get(s => s.STUDENT_INTERNAL_ID == student.Student_Internal_Id);

                    _studentTrainingRepo.Add(new STUDENT_TRAINING
                    {
                        STUDENT = dbstudent,
                        TRAINING = dbTraining,
                        AMOUNT = 100,
                        PAYMENTTYPE = "mesecno",
                        RELATIONSHIP = "something",
                        TRAININGQUALITY = "something"
                    });
                }
            }
            _unitOfWork.Commit();
        }

        public List<Student> getAllStudentsInTraining(string trainingInternalId)
        {
            var allStudensTraining = _studentTrainingRepo.GetAll();
            List<Student> studentsInTraining = new List<Student>();
            TRAINING dbTraining = _trainingRepo.Get(t => t.TRAINING_INTERNAL_ID == trainingInternalId);
            foreach (STUDENT_TRAINING student_training in allStudensTraining)
            {
                if (student_training.TRAINING_ID == dbTraining.TRAINING_ID)
                {
                    STUDENT dbStudent = _studentRepo.Get(s => s.PERSON.PERSON_ID == student_training.PERSON_ID);
                    var student = new Student(dbStudent.STUDENT_INTERNAL_ID, dbStudent.PERSON.FIRSTNAME, dbStudent.PERSON.LASTNAME, dbStudent.PERSON.EMAIL,
                     dbStudent.PERSON.TELEPHONE, dbStudent.PAYMENTTYPE, (double)dbStudent.DISCOUNT, dbStudent.JMBG, dbStudent.ADDRESS, dbStudent.BORN_DATE.ToString(),
                     dbStudent.COUNTRY, dbStudent.PARENT_FIRST_NAME, dbStudent.PARENT_LAST_NAME, dbStudent.PARENTS_MAIL, dbStudent.PARENTS_TELEPHONE, dbStudent.IMAGE_URL, (Decimal)dbStudent.ToPay);
                    studentsInTraining.Add(student);
                }
            }

            return studentsInTraining;
        }

        public void deleteStudentInTraining(string studentInternalId)
        {
            var dbStudent = _studentRepo.Get(s => s.STUDENT_INTERNAL_ID == studentInternalId);
            _studentTrainingRepo.Delete(st => st.PERSON_ID == dbStudent.PERSON.PERSON_ID);
        }
    }
}
