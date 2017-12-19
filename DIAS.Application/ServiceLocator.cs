using Autofac;
using DIAS.Data;
using DIAS.Infrasturcture;
using System;
using System.Collections.Generic;

namespace DIAS.Application
{
    public class ServiceLocator
    {
        private static object _mutex = new object();

        private static IContainer _container;
        /// <summary>
        /// Single instance of <see cref="IContainer"/>.
        /// </summary>
        public static IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (_mutex)
                    {
                        _container = Initialize();
                    }
                }
                return _container;
            }
        }

        /// <summary>
        /// Initialize an <see cref="Autofac.IContainer"/>.
        /// </summary>
        /// <returns></returns>
        protected static IContainer Initialize()
        {
            var builder = new ContainerBuilder();
            // Register 
            builder.RegisterType<StudyRecordRepo>().As<IRepo<StudyRecord>>();
            builder.RegisterType<SeriesRecordRepo>().As<IRepo<SeriesRecord>>();
            builder.RegisterType<ImageRecordRepo>().As<IRepo<ImageRecord>>();
            // 
            return builder.Build();
        }
    }
}
