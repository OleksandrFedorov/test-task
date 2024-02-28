using TestTask.Data.Entity;
using TestTask.Dto.User;
using TestTask.Helpers;
using UserEntity = TestTask.Data.Entity.User;

namespace TestTask.Services.User
{
    public static class Mapper
    {
        public static UserDto ToDtoEntity(this UserEntity userDb) => userDb is null
            ? null
            : new UserDto()
            {
                Id = userDb.Id,
                Name = userDb.Name,
                Password = string.Empty,
                Created = userDb.Created
            };

        public static UserEntity ToDbEntity(this UserDto user) => user is null
            ? null
            : new UserEntity()
            {
                Id = user.Id,
                Name = user.Name,
                Created = user.Created,
                PasswordHash = EncryptionHelper.HashPasword(user.Password)
            };

        public static UserSessionDto ToDtoEntity(this UserSession userSessionDb) => userSessionDb is null
            ? null
            : new UserSessionDto()
            {
                Id = userSessionDb.Id,
                Created = userSessionDb.Created,
                User = userSessionDb.User.ToDtoEntity(),
                LastLogin = userSessionDb.LastLogin
            };
    }
}
