using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIASCoreConsole
{
    class FakeImageFactory : IImgaeFactory
    {
        private int _rows;
        private int _columns;
        private int _MaxCount;
        private int _currentIndex;

        public FakeImageFactory(int row, int columns, int maxSize = 1024)
        {
            _rows = row;
            _columns = columns;
            _MaxCount = maxSize * (1024 * 1024 / 2 / _rows / _columns);
            _currentIndex = 0;
        }

        public bool IsCompleted => (_currentIndex >= _MaxCount);

        public async Task<byte[]> NextImageAsync()
        {
            DImage image = new DImage(_rows, _columns, _currentIndex);
            _currentIndex++;
            string message = JsonConvert.SerializeObject(image);
            return await Task.FromResult(Encoding.UTF8.GetBytes(message));
        }
    }
}
