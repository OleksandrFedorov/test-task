using System.Drawing;
using TestTask.Data.Repository;
using TestTask.Dto.File;
using TestTask.Helpers;
using PdfLibCore;

namespace TestTask.Services.File
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Other platforms is not supported yet")]
    public class FileService(TestTaskRepository repository, FileManager fileManager)
    {
        private readonly TestTaskRepository _repository = repository;
        private readonly FileManager _fileManager = fileManager;

        #region File share

        public async Task<FileShareDto> ShareAsync(FileShareCreateRequestDto dto, CancellationToken cancellationToken)
        {
            var fileDb = await _repository.FileDataAccess.GetByIdAsync(dto.FileId, cancellationToken);
            if (fileDb is null)
            {
                return null;
            }

            var expired = DateTime.Now.AddDays(dto.Days).AddHours(dto.Hours).AddMinutes(dto.Minutes);
            var shareDb = new Data.Entity.FileShare()
            {
                File = fileDb,
                Expired = expired
            };

            shareDb = await _repository.FileShareDataAccess.AddAsync(shareDb, cancellationToken);
            return shareDb.ToDtoEntity();
        }

        public async Task<FileShareDto> GetFileByShareIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var shareDb = await _repository.FileShareDataAccess.GetFileByShareIdAsync(id, cancellationToken);
            return shareDb.ToDtoEntity(includeFile: true);
        }

        #endregion

        #region File

        public async Task<FileDto> AddAsync(Guid userId, Microsoft.AspNetCore.Http.IFormFile file, CancellationToken cancellationToken)
        {
            var fileExtention = file.FileName.Split('.').Last();
            var fileName = file.FileName[0..^(fileExtention.Length + 1)];
            var fileType = GetFileTypeByExtention(fileExtention);
            var fileDb = new Data.Entity.File()
            {
                Name = fileName,
                Type = fileType,
                MIMEType = file.ContentType,
                Size = file.Length,
                UserId = userId,
                Path = string.Empty,
                Trumbnail = string.Empty
            };

            try
            {
                fileDb = await _repository.FileDataAccess.AddAsync(fileDb, cancellationToken);
                var filePath = Path.Combine(_fileManager.GetUserFolder(userId), fileDb.Id.ToString() + $".{fileExtention}");
                await FileManager.AddAsync(filePath, file, cancellationToken);
                fileDb.Path = filePath;
                fileDb.Trumbnail = GetFileTrumbnail(filePath, fileType);
                await _repository.FileDataAccess.UpdateCountOnDownloadAsync(fileDb, cancellationToken);

                return fileDb.ToDtoEntity();
            }
            catch (Exception)
            {
                await _repository.FileDataAccess.DeleteAsync(fileDb, cancellationToken);
                FileManager.Delete(fileDb.Path);
                throw;
            }
        }

        public async Task<FileDto> GetFileAsync(Guid id, CancellationToken cancellationToken)
        {
            var fileDb = await _repository.FileDataAccess.GetByIdAsync(id, cancellationToken);
            return fileDb.ToDtoEntity();
        }

        public Stream GetFileStream(string filePath)
        {
            return FileManager.GetFileStream(filePath);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var fileDb = await _repository.FileDataAccess.GetByIdAsync(id, cancellationToken);
            if (fileDb is null)
            {
                return;
            }

            FileManager.Delete(fileDb.Path);
            await _repository.FileDataAccess.DeleteAsync(fileDb, cancellationToken);
        }

        public async Task DeleteShareAsync(Guid id, CancellationToken cancellationToken)
        {
            var fileShareDb = await _repository.FileShareDataAccess.GetByIdAsync(id, cancellationToken);
            if (fileShareDb is null)
            {
                return;
            }

            await _repository.FileShareDataAccess.DeleteAsync(fileShareDb, cancellationToken);
        }

        public async Task UpdateCountOnDownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            var fileDb = await _repository.FileDataAccess.GetByIdAsync(id, cancellationToken);
            ++fileDb.NumberOfDownloads;
            await _repository.FileDataAccess.UpdateCountOnDownloadAsync(fileDb, cancellationToken);
        }

        public async Task<List<FileDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var filesDb = await _repository.FileDataAccess.GetAllAsync(cancellationToken);
            return filesDb.Select(f => f.ToDtoEntity()).ToList();
        }

        private static string GetFileTrumbnail(string filePath, Data.Enums.FileType fileType)
        {
            var thumbnail = string.Empty;
            switch (fileType)
            {
                case Data.Enums.FileType.Other:
                    return thumbnail;
                case Data.Enums.FileType.PDF:
                    return GetTrumbnailAsBase64FromPdf(filePath);
                case Data.Enums.FileType.TableDocument:
                    break;
                case Data.Enums.FileType.TextDocument:
                    break;
                case Data.Enums.FileType.Text:
                    return GetTrumbnailAsBase64FromTxt(filePath);
                case Data.Enums.FileType.Image:
                    var bitmap = new Bitmap(filePath);
                    return bitmap.GetThumbnailAsBase64();
                default:
                    break;
            }

            return thumbnail;
        }

        private static string GetTrumbnailAsBase64FromTxt(string filePath)
        {
            var maxLength = 120;
            var thumbnail = System.IO.File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(thumbnail)) return thumbnail;
            return thumbnail.Length <= maxLength ? thumbnail : thumbnail[..maxLength] + "...";
        }

        private static string GetTrumbnailAsBase64FromPdf(string filePath)
        {
            using var pdfPage = new PdfDocument(System.IO.File.Open(filePath, FileMode.Open)).Pages.FirstOrDefault();
            if (pdfPage is null)
            {
                return null;
            }

            using var pdfBitmap = new PdfiumBitmap(210, 297, false);
            pdfPage.Render(pdfBitmap, (0, 0, 210, 297));

            using var stream = pdfBitmap.AsBmpStream(25D, 25D);
            var bitmap = new Bitmap(stream);
            return bitmap.ToBase64String();
        }

        private static Data.Enums.FileType GetFileTypeByExtention(string extention)
        {
            if (FileExtentions.Map.TryGetValue(extention.ToUpper(), out var fileExtentions))
            {
                return fileExtentions;
            }

            return Data.Enums.FileType.Other;
        }

        #endregion
    }
}
