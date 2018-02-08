using System;
using System.Collections.Generic;
using System.Text;

namespace DIASCoreConsole
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
            Data = new byte[Rows * Columns * 2];
        }

        public static DImage NewImage(int index)
        {
            return new DImage() { Rows = 1024, Columns = 1024, Data = new byte[1024 * 1024 * 2], Index = index };
        }

        public static DImage SmallImage(int index)
        {
            return new DImage() { Rows = 1, Columns = 512, Data = new byte[512 * 2], Index = index };
        }
    }
}
