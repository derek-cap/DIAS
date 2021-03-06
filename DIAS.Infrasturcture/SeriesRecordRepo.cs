﻿using DIAS.Data;
using ModelRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Infrasturcture
{
    public class SeriesRecordRepo: BaseRepo<SeriesRecord>, IRepo<SeriesRecord>
    {
        public SeriesRecordRepo()
        {
            Table = Context.SeriesRecords;
        }

        public int Delete(int id)
        {
            SeriesRecord record = GetOne(id);
            if (record == null)
            {
                return -1;
            }
            return base.Delete(record);
        }

        public Task<int> DeleteAsync(int id)
        {
            SeriesRecord record = GetOne(id);
            if (record == null)
            {
                return Task.FromResult(-1);
            }
            return DeleteAsync(record);
        }

        async Task<IEnumerable<SeriesRecord>> IRepo<SeriesRecord>.GetAllAsync()
        {
            return await GetAllAsync();
        }

        int IRepo<SeriesRecord>.Save(SeriesRecord entity)
        {
            return Save(entity);
        }

        Task<int> IRepo<SeriesRecord>.SaveAsync(SeriesRecord entity)
        {
            return SaveAsync(entity);
        }
    }
}
