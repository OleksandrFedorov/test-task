using Microsoft.AspNetCore.Http;
using TestTask.Managers.File;

namespace TestTask.Services.File
{
    public class FileManager(FileManagerConfiguration configuration)
    {
        private readonly FileManagerConfiguration _configuration = configuration;

        public FileInfo[] GetUserFiles(Guid userId)
        {
            var currentLocation = Path.Combine(_configuration.DefaultLocation, userId.ToString());
            var directory = new DirectoryInfo(currentLocation);
            return directory.GetFiles();
        }

        public static Stream GetFileStream(string filePath)
        {
            return System.IO.File.OpenRead(filePath);
        }

        public static async Task AddAsync(string filePath, IFormFile file, CancellationToken cancellationToken)
        {
            using FileStream fs = new(filePath, FileMode.Create);
            await file.CopyToAsync(fs, cancellationToken);
            fs.Close();
        }

        public string GetUserFolder(Guid userId)
        {
            var userDirectory = Path.Combine(_configuration.DefaultLocation, userId.ToString());
            CreateDirectoryIfNotExists(userDirectory);
            return userDirectory;
        }

        public FileInfo Rename(Guid userId, string oldFileName, string newFileName)
        {
            var oldFilePath = Path.Combine(_configuration.DefaultLocation, userId.ToString(), oldFileName);
            var newFilePath = Path.Combine(_configuration.DefaultLocation, userId.ToString(), newFileName);
            System.IO.File.Move(oldFilePath, newFilePath);

            return new FileInfo(newFilePath);
        }

        public static void Delete(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            var NumberOfRetries = 3;
            var DelayOnRetry = 1000;

            for (int i = 1; i <= NumberOfRetries; ++i)
            {
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch (IOException) when (i <= NumberOfRetries)
                {
                    Thread.Sleep(DelayOnRetry);
                }
            }
        }

        private void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(_configuration.DefaultLocation)) 
            {
                Directory.CreateDirectory(_configuration.DefaultLocation);
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
