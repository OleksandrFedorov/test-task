using Microsoft.EntityFrameworkCore;
using TestTask.Data.Entity;

namespace TestTask.Data.DataAccess.User
{
    public class UserSessionDataAccess : BaseDataAccess<UserSession>
    {
        internal UserSessionDataAccess(TestTaskApplcationContext applicationContext) : base(applicationContext)
        {
        }

        public Task<UserSession> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return _appContext.UserSessions.Include(x => x.User).SingleOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        }

        public Task UpdateDateOnLoginAsync(UserSession userSession, CancellationToken cancellationToken)
        {
            userSession.LastLogin = DateTime.Now;
            _appContext.Entry(userSession).Property(nameof(UserSession.LastLogin)).IsModified = true;
            return _appContext.SaveChangesAsync(cancellationToken);
        }
    }
}
