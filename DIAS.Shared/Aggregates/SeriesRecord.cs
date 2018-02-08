using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIAS.Data
{
    [Table("Series")]
    [DataContract]
    public class SeriesRecord : IAggregateRoot
    {
        [Key]
        [DataMember]
        public int InstanceId { get; protected set; }

        [StringLength(100)]
        [DataMember]
        [Required]
        public string SeriesUID { get; set; }         // 1

        [StringLength(20)]
        [DataMember]
        public string SeriesNumber { get; set; }        // 2

        [StringLength(20)]
        [DataMember]
        public string SeriesType { get; set; }     // 3

        [DataMember]
        public int ImageCount { get; set; }         // 4

        [StringLength(20)]
        [DataMember]
        public string Kernel { get; set; }          // 5

        [StringLength(200)]
        [DataMember]
        public string SeriesDescription { get; set; }   // 6

        [StringLength(100)]
        [DataMember]
        public string ProtocolName { get; set; }        // 7

        [StringLength(50)]
        [DataMember]
        public string BodyPartExamined { get; set; }    // 8

        [Required, StringLength(100)]
        [DataMember]
        public string StudyUID { get; set; }    // 9

        [DataMember]
        public int RecordState { get; set; } // 10

        [DataMember]
        public bool IsContrast { get; set; }   // 11

        [ForeignKey("InstanceId")]
        public virtual StudyRecord Study { get; set; }

        public virtual ICollection<ImageRecord> Images { get; set; } = new HashSet<ImageRecord>();

        protected SeriesRecord()
        { }

        public SeriesRecord(string seriesUID, StudyRecord study)
        {
            SeriesUID = seriesUID;
            StudyUID = Study.StudyUID;
            Study = study;
        }

        public SeriesRecord(string seriesUID, string studyUID)
        {
            SeriesUID = seriesUID;
            StudyUID = studyUID;
        }

    }
}
