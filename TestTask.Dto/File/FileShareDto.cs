namespace TestTask.Dto.File
{
    public class FileShareDto : BaseDto
    {
        public FileDto File { get; set; }
        public DateTime Expired { get; set; }
    }
}
