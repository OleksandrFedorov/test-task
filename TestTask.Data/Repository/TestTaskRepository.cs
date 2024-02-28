using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TestTask.Data.DataAccess.File;
using TestTask.Data.DataAccess.User;

namespace TestTask.Data.Repository
{
    public class TestTaskRepository : IDisposable
    {
        private readonly TestTaskApplcationContext _appContext;

        public void AddDataAccesses()
        {
            FileDataAccess = new FileDataAccess(_appContext);
            FileShareDataAccess = new FileShareDataAccess(_appContext);
            UserDataAccess = new UserDataAccess(_appContext);
            UserSessionDataAccess = new UserSessionDataAccess(_appContext);
        }

        public FileDataAccess FileDataAccess { get; set; }
        public FileShareDataAccess FileShareDataAccess { get; set; }
        public UserDataAccess UserDataAccess { get; set; }
        public UserSessionDataAccess UserSessionDataAccess { get; set; }


        public TestTaskRepository(DbContextOptions<TestTaskApplcationContext> context)
        {
            _appContext = new TestTaskApplcationContext(context);
            AddDataAccesses();
        }

        public TestTaskRepository(string connectionString)
        {
            _appContext = new TestTaskApplcationContext(connectionString);
            AddDataAccesses();
        }

        public Task MigrateAsync(CancellationToken cancellationToken)
        {
            _appContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(120));
            return _appContext.Database.MigrateAsync(cancellationToken);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return _appContext.Database.BeginTransactionAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _appContext.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
