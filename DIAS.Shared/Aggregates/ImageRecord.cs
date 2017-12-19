using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIAS.Data
{
    [Table("Images")]
    [DataContract]
    public class ImageRecord : IAggregateRoot
    {
        [Key]
        public int ImageId { get; protected set; }

        [StringLength(100)]
        [DataMember]
        public string ImageUID { get; protected set; }             // 0

        [Required, StringLength(100)]
        [DataMember]
        public string SeriesUID { get; set; }           // 1  

        [DataMember]
        public int ImageNumber { get; set; }                   // 2   

        [DataMember]
        public double? SliceThickness { get; set; }               // 3

        [StringLength(20)]
        [DataMember]
        public string Matrix { get; set; }                     // 4

        [StringLength(200)]
        [DataMember]
        public string DcmFileName { get; set; }                 // 5

        [ForeignKey("SeriesInstanceId")]
        public virtual SeriesRecord Series { get; set; }

        protected ImageRecord()
        { }

        public ImageRecord(string imageUID, SeriesRecord series)
        {
            ImageUID = imageUID;
            Series = series;
            SeriesUID = series.SeriesUID;
        }

        public ImageRecord(string imageUID, string seriesUID)
        {
            ImageUID = imageUID;
            SeriesUID = seriesUID;
        }
    }
}
