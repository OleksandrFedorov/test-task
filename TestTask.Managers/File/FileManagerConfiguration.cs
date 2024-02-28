namespace TestTask.Managers.File
{
    public class FileManagerConfiguration(string defaultLocation)
    {
        public string DefaultLocation { get; set; } = defaultLocation;
    }
}
