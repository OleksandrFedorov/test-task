using TestTask.Data.Entity;
using TestTask.Data.Repository;
using TestTask.Dto.User;

namespace TestTask.Services.User
{
    public class UserService(TestTaskRepository repository)
    {
        private readonly TestTaskRepository _repository = repository;

        public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var userDb = await _repository.UserDataAccess.GetByIdAsync(id, cancellationToken);
            return userDb.ToDtoEntity();
        }

        public async Task<List<string>> GetAllUserNamesAsync(CancellationToken cancellationToken)
        {
            var usersDb = await _repository.UserDataAccess.GetAllAsync(cancellationToken);
            return usersDb.Select(u => u.Name).ToList();
        }

        public async Task<UserDto> AddAsync(UserDto file, CancellationToken cancellationToken)
        {
            var userDb = file.ToDbEntity();
            userDb = await _repository.UserDataAccess.AddAsync(userDb, cancellationToken);

            return userDb.ToDtoEntity();
        }

        public async Task<bool> ValidateLoginAsync(UserDto user, CancellationToken cancellationToken)
        {
            var userDb = user.ToDbEntity();
            return await _repository.UserDataAccess.ValidateLoginAsync(userDb, cancellationToken);
        }

        public async Task<bool> ValidateUserSessionAsync(Guid userSessionId, CancellationToken cancellationToken)
        {
            var sessionDb = await _repository.UserSessionDataAccess.GetByIdAsync(userSessionId, cancellationToken);
            if (sessionDb is null)
            {
                return false;
            }

            await _repository.UserSessionDataAccess.UpdateDateOnLoginAsync(sessionDb, cancellationToken);
            return true;
        }

        public async Task<UserSessionDto> UserLoginAsync(UserDto user, CancellationToken cancellationToken)
        {
            var userDb = await _repository.UserDataAccess.GetByNameAsync(user.Name, cancellationToken);
            var sessionDb = await _repository.UserSessionDataAccess.GetByUserIdAsync(userDb.Id, cancellationToken);

            if (sessionDb is null)
            {
                return await AddUserSessionAsync(userDb, cancellationToken);
            }
            else
            {
                await _repository.UserSessionDataAccess.UpdateDateOnLoginAsync(sessionDb, cancellationToken);
                return sessionDb.ToDtoEntity();
            }
        }

        private async Task<UserSessionDto> AddUserSessionAsync(Data.Entity.User userDb, CancellationToken cancellationToken)
        {
            UserSession sessionDb = new()
            {
                LastLogin = DateTime.Now,
                User = userDb
            };

            sessionDb = await _repository.UserSessionDataAccess.AddAsync(sessionDb, cancellationToken);
            return sessionDb.ToDtoEntity();
        }
    }
}
