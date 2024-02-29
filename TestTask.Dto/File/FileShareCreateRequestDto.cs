namespace TestTask.Dto.File
{
    public class FileShareCreateRequestDto
    {
        public Guid FileId { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
}
