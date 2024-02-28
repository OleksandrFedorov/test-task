namespace TestTask.Dto.User
{
    public class UserSessionDto : BaseDto
    {
        public DateTime LastLogin { get; set; }
        public UserDto User { get; set; }
    }
}
