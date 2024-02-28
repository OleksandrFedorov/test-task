using Microsoft.EntityFrameworkCore;
using FileEntity = TestTask.Data.Entity.File;

namespace TestTask.Data.DataAccess.File
{
    public class FileDataAccess : BaseDataAccess<FileEntity>
    {
        internal FileDataAccess(TestTaskApplcationContext applicationContext) : base(applicationContext)
        {
        }

        public new async Task<FileEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _appContext.Files
                .Include(f => f.User)
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }

        public async Task<FileEntity> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _appContext.Files.FirstOrDefaultAsync(f => f.Name == name, cancellationToken);
        }

        public new async Task<List<FileEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _appContext.Files
                .Include(f => f.User)
                .Include(f => f.Shares)
                .ToListAsync(cancellationToken);
        }

        public Task UpdatePathAndTrumbnailAsync(FileEntity file, CancellationToken cancellationToken)
        {
            _appContext.Entry(file).Property(nameof(FileEntity.Path)).IsModified = true;
            _appContext.Entry(file).Property(nameof(FileEntity.Trumbnail)).IsModified = true;
            return _appContext.SaveChangesAsync(cancellationToken);
        }

        public Task UpdateCountOnDownloadAsync(FileEntity file, CancellationToken cancellationToken)
        {
            _appContext.Entry(file).Property(nameof(FileEntity.NumberOfDownloads)).IsModified = true;
            return _appContext.SaveChangesAsync(cancellationToken);
        }
    }
}
