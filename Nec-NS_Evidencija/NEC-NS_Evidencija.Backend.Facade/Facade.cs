using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Repositories;
using NEC_NS_Evidencija.Backend.DatabaseService;
using Autofac;
using NEC_NS_Evidencija.Backend.Dto;

namespace NEC_NS_Evidencija.Backend.Facade
{
    public class Facade : IFacade
    {
        private IService _dbService;
        private ILifetimeScope _scope;
        private IContainer _container;

        public Facade() 
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(StudentRepository).Assembly, typeof(CoachRepository).Assembly, typeof(TrainingRepository).Assembly,
              typeof(StudentTrainingRepository).Assembly, typeof(MontlyPaymentsRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterType<Service>().As<IService>();
            _container = builder.Build();

            _scope = _container.BeginLifetimeScope();

            _dbService = _scope.Resolve<IService>();
        }

        public Dto.Student GetStudent(string studentInternalId)
        {
            var student = _dbService.GetStudent(studentInternalId);

            return student;
        }

        public List<Dto.Student> GetAllStudents()
        {
            return _dbService.GetAllStudents();
        }

        public void CreateStudent(Dto.Student student)
        {
            _dbService.CreateStudent(student);
        }

        public void DeleteStudent(string studentInternalId)
        {
            _dbService.DeleteStudent(studentInternalId);
        }

        public void UpdateStudent(Dto.Student student)
        {
            _dbService.UpdateStudent(student);
        }

        public Dto.Coach GetCoach(string coachInternalId)
        {
            var coach = _dbService.GetCoach(coachInternalId);

            return coach;
        }

        public List<Dto.Coach> GetAllCoaches()
        {
            return _dbService.GetAllCoaches();
        }

        public void CreateCoach(Dto.Coach coach)
        {
            _dbService.CreateCoach(coach);
        }

        public void DeleteCoach(string coachInternalId)
        {
            _dbService.DeleteCoach(coachInternalId);
        }

        public void UpdateCoach(Dto.Coach coach)
        {
            _dbService.UpdateCoach(coach);
        }

        public Dto.Training GetTraining(string trainingInternalId)
        {
            var training = _dbService.GetTraining(trainingInternalId);

            return training;
        }

        public List<Dto.Training> GetAllTrainings()
        {
            return _dbService.GetAllTrainings();
        }

        public void CreateTraining(Dto.Training training)
        {
            _dbService.CreateTraining(training);
        }

        public void CreateTraining(Dto.Training training, Dto.Coach coach)
        {
            _dbService.CreateTraining(training,coach);
        }

        public void DeleteTraining(string trainingInternalId)
        {
            _dbService.DeleteTraining(trainingInternalId);
        }

        public void UpdateTraining(Dto.Training training, Coach coach)
        {
            _dbService.UpdateTraining(training, coach);
        }

        public Dto.MontlyPayment GetMontlyPayment(string montlyPaymentInternalId)
        {
            return _dbService.GetMontlyPayment(montlyPaymentInternalId);
        }

        public List<Dto.MontlyPayment> GetAllMontlyPayments()
        {
            return _dbService.GetAllMontlyPayments();
        }

        public void CreateMontlyPayment(NEC_NS_Evidencija.Backend.Dto.MontlyPayment montlyPayment, Dto.Student student)
        {
            _dbService.CreateMontlyPayment(montlyPayment, student);
        }

        public void UpdateMontlyPayment(Dto.MontlyPayment montlyPayment)
        {
            _dbService.UpdateMontlyPayment(montlyPayment);
        }

        public void DeleteMontlyPayment(string montlyPaymentInternalId)
        {
            _dbService.DeleteMontlyPayment(montlyPaymentInternalId);
        }

        public void TrainingScheduling(List<Dto.Student> students, Dto.Training training)
        {
            _dbService.TrainingScheduling(students, training);
        }

        public void TrainingScheduling(List<Dto.Student> students, Dto.Training training, Dto.Coach coach)
        {
            _dbService.TrainingScheduling(students, training, coach);
        }

        public void UpdateTrainingScheduling(List<Student> students, Training training)
        {
            _dbService.UpdateTrainingScheduling(students, training);
        }

        public List<Student> getAllStudentsInTraining(string trainingInternalId)
        {
            return _dbService.getAllStudentsInTraining(trainingInternalId);
        }

        public void deleteStudentInTraining(string studentInternalId)
        {
             _dbService.deleteStudentInTraining(studentInternalId);
        }
    }
}
