using DIAS.Data;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DIAS.DataModel
{   
    class DcmConvert
    {
        public static string ToDcmTagString(UInt16 groupNumber, UInt16 elementNumber)
        {
            return string.Format("({0:X4},{1:X4})", groupNumber, elementNumber);
        }

        public static string ToPN(string dcmPN)
        {
            return string.IsNullOrEmpty(dcmPN) ? null : dcmPN.Replace('^', ' ');
        }

        public static string ToDcmPN(string fullName)
        {
            return string.IsNullOrEmpty(fullName) ? null : fullName.Replace(' ', '^');
        }

        public static DateTime? ToDate(string dcmDate)
        {
            if (string.IsNullOrEmpty(dcmDate))
                return null;
            else
            {
                DateTime theDate = new DateTime();
                if (DateTime.TryParseExact(dcmDate, "yyyyMMdd", null, DateTimeStyles.None, out theDate))
                {
                    return theDate;
                }
                else
                    return null;
            }
        }

        public static string ToDcmDate(DateTime? theDate)
        {
            if (theDate == null)
                return null;
            else
                return ((DateTime)theDate).ToString("yyyyMMdd");
        }

        public static DateTime? ToTime(string dcmTime)
        {
            if (string.IsNullOrEmpty(dcmTime))
                return null;
            else
            {
                DateTime theDate = new DateTime();
                if (DateTime.TryParseExact(dcmTime, "HHmmss.FFFFFF", null, DateTimeStyles.None, out theDate))
                {
                    return theDate;
                }
                else
                    return null;
            }
        }

        public static string ToDcmTime(DateTime? theTime)
        {
            if (theTime == null)
                return null;
            else
                return ((DateTime)theTime).ToString("HHmmss.FFF");
        }

        public static string CalculateAge(DateTime? birthDate)
        {
            if (birthDate == null)
                return null;

            int days = (DateTime.Now - (DateTime)birthDate).Days;
            if (days > 365 * 2)
                return string.Format("{0:D3}Y", days / 365);
            else if (days > 8 * 7)
                return string.Format("{0:D3}M", days / 30);
            else if (days > 2 * 7)
                return string.Format("{0:D3}W", days / 7);
            else
                return string.Format("{0:D3}D", days);
        }

        public static string ToDcmImagePosition(double d1, double d2, double d3)
        {
            return string.Format("{0:e6}\\{1:e6}\\{2:e6}", d1, d2, d3);
        }

        public static string ToDcmImageOrientation(double d1, double d2, double d3, double d4, double d5, double d6)
        {
            return string.Format("{0:e6}\\{1:e6}\\{2:e6}\\{3:e6}\\{4:e6}\\{5:e6}", d1, d2, d3, d4, d5, d6);
        }

        public static PatientSex GetPatientSex(string sex)
        {
            if (string.IsNullOrEmpty(sex))
            {
                return PatientSex.Unknown;
            }
            else if (sex.StartsWith("M"))
            {
                return PatientSex.Male;
            }
            else if (sex.StartsWith("F"))
            {
                return PatientSex.Female;
            }
            return PatientSex.Other;
        }

        public static string ToDcmSex(PatientSex theSex)
        {
            string res = "U";
            switch (theSex)
            {
                case PatientSex.Male:
                    res = "M";
                    break;
                case PatientSex.Female:
                    res = "F";
                    break;
                case PatientSex.Other:
                    res = "O";
                    break;
                default:
                    break;
            }
            return res;
        }

        /// <summary>
        /// Split the string (dicom format) to double array.
        /// </summary>
        /// <param name="multiValueString"></param>
        /// <returns></returns>
        public static double[] SplitToDoubleArray(string multiValueString)
        {
            List<double> doubleList = new List<double>();
            string[] valueStrings = multiValueString.Split('\\');
            foreach (var item in valueStrings)
            {
                doubleList.Add(Convert.ToDouble(item));
            }
            return doubleList.ToArray();
        }
    }
}
