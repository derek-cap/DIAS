using System;
using System.Collections.Generic;
using System.Text;

namespace DIASCoreConsole2
{
    class DImage
    {
        public int Index { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public byte[] Data { get; private set; }

        protected DImage() { }

        public DImage(int rows, int columns, int index)
        {
            Rows = rows;
            Columns = columns;
            Index = index;
        }
    }
}
