using DIAS.Data;
using Dicom;
using System;

namespace DIAS.DataModel
{
    public static class DicomTransformer
    {
        /// <summary>
        /// Create a study record from <see cref="DicomDataset"/>.
        /// </summary>
        /// <param name="dcmData"><see cref="DicomDataset"/></param>
        /// <returns>Study record <see cref="{DataModel.StudyRecord}"/></returns>
        public static StudyRecord CreateStudy(this DicomDataset dcmData)
        {
            string studyUID = dcmData.GetString(DicomTag.StudyInstanceUID);
            var record = new StudyRecord(studyUID);           // 1

            record.StudyID = dcmData.GetString(DicomTag.StudyID);           // 2
            record.PatientName = dcmData.GetString(DicomTag.PatientName);   // 3

            string dcmDate = null;
            dcmDate = dcmData.GetString(DicomTag.StudyDate);
            string dcmTime = null;
            dcmTime = dcmData.GetString(DicomTag.StudyTime);
            record.DateTime = CombineDateTime(dcmDate, dcmTime);            // 4

            record.PatientID = dcmData.GetString(DicomTag.PatientID);       // 5

            string sex = dcmData.GetString(DicomTag.PatientSex);
            record.PatientSex = (int)DcmConvert.GetPatientSex(sex);                // 6       

            record.PatientAge = dcmData.GetString(DicomTag.PatientAge);     // 7
            record.Modality = dcmData.GetString(DicomTag.Modality);         // 8    
            record.StudyDescription = dcmData.GetString(DicomTag.StudyDescription);     // 9

            return record;
        }

        /// <summary>
        /// Create a series record from <see cref="DicomDataset"/>.
        /// </summary>
        /// <param name="dcmData"><see cref="DicomDataset"/></param>
        /// <returns>Series record <see cref="{DataModel.SeriesRecord}"/></returns>
        public static SeriesRecord CreateSeries(this DicomDataset dcmData)
        {
            string studyUID = dcmData.GetString(DicomTag.StudyInstanceUID);
            string seriesUID = dcmData.Get<string>(DicomTag.SeriesInstanceUID);
            var record = new SeriesRecord(seriesUID, studyUID);           // 0, 1

            record.SeriesNumber = dcmData.Get<string>(DicomTag.SeriesNumber, 0, null);      // 2
            record.SeriesType = dcmData.Get<string>(DicomTag.SeriesType, 0, null);          //  3
            record.ImageCount = dcmData.Get<ushort>(new DicomTag(0x1011, 0x0008), 0, 0);      // 4

            record.Kernel = dcmData.Get<string>(DicomTag.ConvolutionKernel, 0, null);      // 5
            record.SeriesDescription = dcmData.Get<string>(DicomTag.SeriesDescription, 0, null);      // 6
            record.ProtocolName = dcmData.Get<string>(DicomTag.ProtocolName, 0, null);      // 7
            record.BodyPartExamined = dcmData.Get<string>(DicomTag.BodyPartExamined, 0, null);      // 8

            return record;
        }

        /// <summary>
        /// Create a image record from <see cref="DicomDataset"/>.
        /// Becareful: filename is null.
        /// </summary>
        /// <param name="dcmData"><see cref="DicomDataset"/></param>
        /// <returns>Image record <see cref="{DataModel.ImageRecord}"/></returns>
        public static ImageRecord CreateImage(this DicomDataset dcmData)
        {
            string imageUID = dcmData.Get<string>(DicomTag.SOPInstanceUID);
            string seriesUID = dcmData.Get<string>(DicomTag.SeriesInstanceUID);
            var record = new ImageRecord(imageUID, seriesUID);        // 0, 1

            record.ImageNumber = dcmData.Get<ushort>(DicomTag.InstanceNumber, 0, 0);    // 2

            string sliceThickness = dcmData.Get<string>(DicomTag.SliceThickness, 0, null);      // 3
            if (string.IsNullOrEmpty(sliceThickness) == false)
            {
                double st = 0;
                double.TryParse(sliceThickness, out st);
                record.SliceThickness = st;
            }

            ushort rows = dcmData.Get<ushort>(DicomTag.Rows, 0, 0);
            ushort columns = dcmData.Get<ushort>(DicomTag.Columns, 0, 0);
            record.Matrix = $"{rows}x{columns}";

            return record;
        }

        /// <summary>
        /// Get string value of DicomDataset after checking the tag.
        /// </summary>
        /// <param name="dcmData"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetString(this DicomDataset dcmData, DicomTag tag)
        {
            return dcmData.Get<string>(tag, null);
        }

        private static DateTime? CombineDateTime(string dcmDate, string dcmTime)
        {
            DateTime? date = DcmConvert.ToDate(dcmDate);
            DateTime? time = DcmConvert.ToTime(dcmTime);
            if (date == null)
            {
                return time;
            }
            else if (time == null)
            {
                return date;
            }
            return new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hour, time.Value.Minute, time.Value.Second);
        }
    }
}
