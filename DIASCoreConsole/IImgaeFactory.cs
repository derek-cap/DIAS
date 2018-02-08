using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIASCoreConsole
{
    interface IImgaeFactory
    {
        bool IsCompleted { get; }
        Task<byte[]> NextImageAsync();
    }
}
