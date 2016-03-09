using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEC_NS_Evidencija.Backend.Dto;

namespace NEC_NS_Evidencija.Backend.DatabaseService
{
    public interface IService
    {
         Student GetStudent(string studentInternalId);
         List<Student> GetAllStudents();
         void CreateStudent(Student student);
         void DeleteStudent(string studentInternalId);
         void UpdateStudent(NEC_NS_Evidencija.Backend.Dto.Student student);
         Coach GetCoach(string coachInternalId);
         List<Coach> GetAllCoaches();
         void CreateCoach(Coach coach);
         void DeleteCoach(string coachInternalId); 
         void UpdateCoach(NEC_NS_Evidencija.Backend.Dto.Coach coach);
         Training GetTraining(string trainingInternalId);
         List<Training> GetAllTrainings();
         void CreateTraining(Training training);
         void CreateTraining(Training training, Coach coach);
         void DeleteTraining(string trainingInternalId);
         void UpdateTraining(NEC_NS_Evidencija.Backend.Dto.Training training, Coach coach);
         MontlyPayment GetMontlyPayment(string montlyPaymentInternalId);
         List<MontlyPayment> GetAllMontlyPayments();
         void UpdateMontlyPayment(NEC_NS_Evidencija.Backend.Dto.MontlyPayment montlyPayment);
         void CreateMontlyPayment(NEC_NS_Evidencija.Backend.Dto.MontlyPayment montlyPayment, Student student);
         void DeleteMontlyPayment(string montlyPaymentInternalId);
         void TrainingScheduling(List<Student> students, Training training);
         void TrainingScheduling(List<Student> students, Training training,Coach coach);
         void UpdateTrainingScheduling(List<Student> students, Training training);
         List<Student> getAllStudentsInTraining(string trainingInternalId);
         void deleteStudentInTraining(string studentInternalId);
       
    }
}
