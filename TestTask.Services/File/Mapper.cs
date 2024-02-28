using Microsoft.EntityFrameworkCore.Query;
using TestTask.Dto.Enums;
using TestTask.Dto.File;
using TestTask.Services.User;
using FileEntity = TestTask.Data.Entity.File;
using FileShareEntity = TestTask.Data.Entity.FileShare;
using FileTypeEntity = TestTask.Data.Enums.FileType;

namespace TestTask.Services.File
{
    public static class Mapper
    {
        public static FileDto ToDtoEntity(this FileEntity fileDb) => fileDb == null
            ? null
            : new FileDto()
            {
                Id = fileDb.Id,
                Name = fileDb.Name,
                Type = fileDb.Type.ToDtoEntity(),
                MIMEType = fileDb.MIMEType,
                Path = fileDb.Path,
                Size = fileDb.Size,
                NumberOfDownloads = fileDb.NumberOfDownloads,
                Thumbnail = fileDb.Trumbnail,
                User = fileDb.User?.ToDtoEntity(),
                Created = fileDb.Created,
                Shares = fileDb?.Shares.Select(x => x.ToDtoEntity()).ToList() ?? new List<FileShareDto>()
            };

        public static FileEntity ToDbEntity(this FileDto file) => file == null
            ? null
            : new FileEntity()
            {
                Id = file.Id,
                Name = file.Name,
                Type = file.Type.ToDbEntity(),
                MIMEType = file.MIMEType,
                Path = file.Path,
                Size = file.Size,
                NumberOfDownloads = file.NumberOfDownloads,
                Trumbnail = file.Thumbnail,
                Created = file.Created
            };

        public static FileShareDto ToDtoEntity(this FileShareEntity shareDb, bool includeFile = false) => shareDb == null
            ? null
            : new FileShareDto()
            {
                Id = shareDb.Id,
                Created = shareDb.Created,
                Expired = shareDb.Expired,
                File = includeFile ? shareDb?.File.ToDtoEntity() : null,
            };

        public static FileShareEntity ToDbEntity(this FileShareDto share) => share == null
            ? null
            : new FileShareEntity()
            {
                Id = share.Id,
                Created = share.Created,
                Expired = share.Expired,
                File = share.File.ToDbEntity()
            };

        public static FileTypeEntity ToDbEntity(this FileType fileTypeDb) => (FileTypeEntity)fileTypeDb;

        public static FileType ToDtoEntity(this FileTypeEntity fileType) => (FileType)fileType;
    }
}
