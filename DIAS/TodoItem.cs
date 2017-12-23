using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public override string ToString()
        {
            return "{" + $"Id: {Id}, Name: {Name}, IsComplete: {IsComplete}" + "}";
        }
    }
}
