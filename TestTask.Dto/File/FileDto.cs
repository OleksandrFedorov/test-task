using TestTask.Dto.Enums;
using TestTask.Dto.User;

namespace TestTask.Dto.File
{
    public class FileDto : BaseDto
    {
        public string Name { get; set; }
        public FileType Type { get; set; }
        public string MIMEType { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public long NumberOfDownloads { get; set; }
        public string Thumbnail { get; set; }
        public UserDto User { get; set; }
        public List<FileShareDto> Shares { get; set; }
    }
}
