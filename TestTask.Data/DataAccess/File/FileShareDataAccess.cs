using Microsoft.EntityFrameworkCore;
using FileShareEntity = TestTask.Data.Entity.FileShare;

namespace TestTask.Data.DataAccess.File
{
    public class FileShareDataAccess : BaseDataAccess<FileShareEntity>
    {
        internal FileShareDataAccess(TestTaskApplcationContext applicationContext) : base(applicationContext)
        {
        }

        public async Task<FileShareEntity> GetFileByShareIdAsync(Guid fileId, CancellationToken cancellationToken)
        {
            return await _appContext.FileShares.Include(x => x.File).FirstOrDefaultAsync(x => x.Id == fileId, cancellationToken);
        }
    }
}
