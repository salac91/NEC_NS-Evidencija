using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC_NS_Evidencija.Backend.Dto
{
    public class Training
    {

        public Training()
        {

        }

        public Training(string training_Internal_Id, Coach coach, int trainingType, String startDate, string trainingTheme,
            double duration, string notes, string trainingQuality, string relationship)
        {
            Training_Internal_Id = training_Internal_Id;
            Coach = coach; 
            TrainingType = trainingType;
            TrainingTheme = trainingTheme;
            StartDate = startDate;
            Duration = duration;
            Notes = notes;
            TrainingQuality = trainingQuality;
            Relationship = relationship;
        }

        public Training(string training_Internal_Id, int trainingType, String startDate, string trainingTheme,
           double duration, string notes, string trainingQuality, string relationship)
        {
            Training_Internal_Id = training_Internal_Id;
            TrainingType = trainingType;
            TrainingTheme = trainingTheme;
            StartDate = startDate;
            Duration = duration;
            Notes = notes;
            TrainingQuality = trainingQuality;
            Relationship = relationship;
        }

        public string Training_Internal_Id { get; set; }

        public Coach Coach { get; set; }

        public int TrainingType { get; set; }

        public string TrainingTheme { get; set; }

        public String StartDate { get; set; }

        public double Duration { get; set; }

        public string Notes { get; set; }

        public string TrainingQuality { get; set; }

        public string Relationship { get; set; }

    }
}
