using System;
using System.Collections.Generic;
using System.Text;

namespace DIAS.DataModel.Models
{
    public class Patient
    {
        public string Name { get; set; }
        public int AccessionNumber { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"AccessionNumber: {AccessionNumber}, Name: {Name}, Age: {Age}";
        }
    }
}
