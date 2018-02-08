using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace DIAS.Data
{
    public enum PatientSex
    {
        Unknown = 0,
        Male = 1,
        Female = 2,
        Other = 3
    }


    [DataContract]
    [Table("Studies")]
    public partial class StudyRecord : IAggregateRoot
    {
        [Key]
        [DataMember]
        public int InstanceId { get; protected set; }

        [StringLength(100)]
        [DataMember]
        [Required]
        public string StudyUID { get; protected set; }         // 1

        [StringLength(30)]
        [DataMember]
        public string StudyID { get; set; }             // 2

        [StringLength(20)]
        [DataMember]
        public string PatientName { get; set; }         // 3
        
        [DataMember]
        public DateTime? DateTime { get; set; }        // 4

        [StringLength(20)]
        [DataMember]
        public string PatientID { get; set; }           // 5

        [DataMember]
        public int PatientSex { get; set; }             // 6

        [StringLength(5)]
        [DataMember]
        public string PatientAge { get; set; }          // 7

        [StringLength(10)]
        [DataMember]
        public string Modality { get; set; }            // 8

        [StringLength(200)]
        [DataMember]
        public string StudyDescription { get; set; }    // 9

        [DataMember]
        public int RecordState { get; set; }            // 10

        public virtual ICollection<SeriesRecord> Series { get; set; } = new HashSet<SeriesRecord>();

        protected StudyRecord()
        { }

        public StudyRecord(string studyUID)
        {
            StudyUID = studyUID;
        }
    }
}
