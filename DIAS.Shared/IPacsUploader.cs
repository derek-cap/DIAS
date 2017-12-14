using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIAS
{
    /// <summary>
    /// Interface representing for uploading dicom to PACS.
    /// </summary>
    public interface IPacsUploader
    {
        /// <summary>
        /// Upload one file to PACS.
        /// </summary>
        /// <param name="filename">Dicom file name</param>
        void Upload(string filename, DicomStation dicomStation);

        /// <summary>
        /// Asynchronously upload one file to PACS.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        Task UploadAsync(string filename, DicomStation dicomStation);

        /// <summary>
        /// Upload all the files to PACS.
        /// </summary>
        /// <param name="filenames">File name list</param>
        void Upload(IEnumerable<string> filenames, DicomStation dicomStation);

        /// <summary>
        /// Asynchronously upload all the files to PACS.
        /// </summary>
        /// <param name="filenames">File nam list</param>
        /// <returns></returns>
        Task UploadAsync(IEnumerable<string> filenames, DicomStation dicomStaion);
    }
}
