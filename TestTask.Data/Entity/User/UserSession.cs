namespace TestTask.Data.Entity
{
    public class UserSession : BaseEntity
    {
        public DateTime LastLogin{ get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
