namespace TestTask.Data.Entity
{
    public class FileShare : BaseEntity
    {
        public Guid FileId { get; set; }
        public File File { get; set; }
        public DateTime Expired { get; set; }
    }
}
