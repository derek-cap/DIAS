using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIAS
{
    public enum StationCategories
    {
        None = 0 ,
        Ris = 1,
        Pacs = 2,
        Printer = 3,
        Server = 4
    }

    /// <summary>
    /// Class representing of Dicom station, like RIS, PACS, Printer.
    /// </summary>
    [Table("DicomStation")]
    public partial class DicomStation : IAggregateRoot
    {
        [Key]
        public int StationId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }

        public int Port { get; set; }

        [StringLength(50)]
        public string AE { get; set; }

        public int CategoryInt { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [NotMapped]
        public StationCategories Category => (StationCategories)CategoryInt;

        public DicomStation()
        { }

        public DicomStation(StationCategories category)
        {
            CategoryInt = (int)category;
        }

        public DicomStation Clone()
        {
            return (DicomStation)MemberwiseClone();
        }
    }
}
