using DIAS.Data;
using ModelRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Infrasturcture
{
    public class StudyRecordRepo : BaseRepo<StudyRecord>, IRepo<StudyRecord>
    {
        public StudyRecordRepo()
        {
            Table = Context.StudyRecords;
        }

        public int Delete(int id)
        {
            StudyRecord record = GetOne(id);
            if (record == null)
            {
                return -1;
            }
            return Delete(record);
        }

        public Task<int> DeleteAsync(int id)
        {
            StudyRecord record = GetOne(id);
            if (record == null)
            {
                return Task.FromResult(-1);
            }
            return DeleteAsync(record);
        }

        async Task<IEnumerable<StudyRecord>> IRepo<StudyRecord>.GetAllAsync()
        {
            return await GetAllAsync();
        }

        int IRepo<StudyRecord>.Save(StudyRecord entity)
        {
            return Save(entity);
        }

        Task<int> IRepo<StudyRecord>.SaveAsync(StudyRecord entity)
        {
            return SaveAsync(entity);
        }
    }
}
