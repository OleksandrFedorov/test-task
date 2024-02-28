using TestTask.Data.Enums;

namespace TestTask.Data.Entity
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public FileType Type { get; set; }
        public string MIMEType { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public long NumberOfDownloads { get; set; }
        public string Trumbnail { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public List<FileShare> Shares { get; set; } = [];
    }
}
