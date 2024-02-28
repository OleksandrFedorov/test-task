using Microsoft.EntityFrameworkCore;
using UserEntity = TestTask.Data.Entity.User;

namespace TestTask.Data.DataAccess.User
{
    public class UserDataAccess : BaseDataAccess<UserEntity>
    {
        internal UserDataAccess(TestTaskApplcationContext applicationContext) : base(applicationContext)
        {
        }

        public async Task<UserEntity> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _appContext.Users.FirstOrDefaultAsync(u => u.Name == name, cancellationToken);
        }

        public async Task<bool> ValidateLoginAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return await _appContext.Users.AnyAsync(u => u.Name == user.Name && u.PasswordHash == user.PasswordHash, cancellationToken);
        }

    }
}
