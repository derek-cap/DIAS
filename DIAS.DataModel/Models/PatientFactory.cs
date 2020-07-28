using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIAS.DataModel.Models
{
    class PatientFactory
    {
        private static IEnumerable<Patient> GeneratePatient(int number)
        {
            Random random = new Random();

            return
            Enumerable.Range(0, number)
                .Select(index =>
                {
                    Task.Delay(20).Wait();
                    return new Patient()
                    {
                        AccessionNumber = index,
                        Name = "Jack",
                        Age = random.Next(10, 40)
                    };
                });
        }

        public static IEnumerable<Patient> DoQuery(CancellationToken token)
        {
            Random random = new Random();
            foreach (var item in PatientFactory.GeneratePatient(random.Next(20, 100)))
            {
                if (token.IsCancellationRequested)
                    yield break;
                yield return item;
            }
        }
    }
}
