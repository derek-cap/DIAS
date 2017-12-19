using DIAS.Data;
using ModelRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DIAS.Infrasturcture
{
    public class ImageRecordRepo : BaseRepo<ImageRecord>, IRepo<ImageRecord>
    {
        public ImageRecordRepo()
        {
            Table = Context.ImageRecords;
        }

        public int Delete(int id)
        {
            ImageRecord record = base.GetOne(id);
            if (record == null)
            {
                return -1;
            }
            return Delete(record);
        }

        public Task<int> DeleteAsync(int id)
        {
            ImageRecord record = base.GetOne(id);
            if (record == null)
            {
                return Task.FromResult(-1);
            }
            return DeleteAsync(record);
        }

        public new ImageRecord GetOne(int? id)
        {
            return base.GetOne(id);
        }

        IEnumerable<ImageRecord> IRepo<ImageRecord>.GetAll()
        {
            return GetAll();
        }

        async Task<IEnumerable<ImageRecord>> IRepo<ImageRecord>.GetAllAsync()
        {
            var collection = await GetAllAsync();
            return collection;
        }

        int IRepo<ImageRecord>.Save(ImageRecord entity)
        {
            return base.Save(entity);
        }

        Task<int> IRepo<ImageRecord>.SaveAsync(ImageRecord entity)
        {
            return base.SaveAsync(entity);
        }
    }
}
