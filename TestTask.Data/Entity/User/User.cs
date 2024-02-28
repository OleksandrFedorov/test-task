namespace TestTask.Data.Entity
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public UserSession UserSession { get; set; }

        public List<File> Files { get; set; } = [];
    }
}
