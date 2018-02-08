using DIAS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIAS.Infrasturcture
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StudyContext(serviceProvider.GetRequiredService<DbContextOptions<StudyContext>>()))
            {
                // Look for any studies.
                if (context.Study.Any())
                {
                    return;     // DB has been seeded
                }

                context.Study.AddRange(
                    new StudyRecord("1.0.0.1")
                    {
                        StudyID = "0001",
                        PatientName = "Jack1",
                        PatientAge = "018Y"
                    },
                    new StudyRecord("1.0.0.2")
                    {
                        StudyID = "0002",
                        PatientName = "Jack2",
                        PatientAge = "018Y"
                    },
                    new StudyRecord("1.0.0.3")
                    {
                        StudyID = "0003",
                        PatientName = "Jack3",
                        PatientAge = "018Y"
                    },
                    new StudyRecord("1.0.0.4")
                    {
                        StudyID = "0004",
                        PatientName = "Jack4",
                        PatientAge = "018Y"
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
