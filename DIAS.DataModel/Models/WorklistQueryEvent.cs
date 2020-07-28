using System;
using System.Collections.Generic;
using System.Text;

namespace DIAS.DataModel.Models
{
    public enum QueryStates { Start, Pending, End }

    public class WorklistQueryEvent
    {
        public QueryStates State { get; private set; }
        public Patient Patient { get; private set; }

        private WorklistQueryEvent(QueryStates state, Patient patient) 
        {
            State = state;
            Patient = patient;
        }

        public static WorklistQueryEvent NewPatient(Patient patient)
        {
            return new WorklistQueryEvent(QueryStates.Pending, patient);
        }

        public static WorklistQueryEvent StartEvent()
        {
            return new WorklistQueryEvent(QueryStates.Start, null);
        }

        public static WorklistQueryEvent EndEvent()
        {
            return new WorklistQueryEvent(QueryStates.End, null);
        }
    }
}
