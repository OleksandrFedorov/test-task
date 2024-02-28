using TestTask.Data.Enums;

namespace TestTask.Services.File
{
    public static class FileExtentions
    {
        public static readonly Dictionary<string, FileType> Map = new()
        {
            { "", FileType.Other },
            { "PDF", FileType.PDF },
            { "XLS", FileType.TableDocument },
            { "XLSX", FileType.TableDocument },
            { "ODS", FileType.TableDocument },
            { "DOC", FileType.TextDocument },
            { "DOCX", FileType.TextDocument },
            { "ODT", FileType.TextDocument },
            { "TXT", FileType.Text },
            { "LOG", FileType.Text },
            { "JSON", FileType.Text },
            { "XML", FileType.Text },
            { "HTM", FileType.Text },
            { "HTML", FileType.Text },
            { "BMP", FileType.Image },
            { "DIB", FileType.Image },
            { "JPG", FileType.Image },
            { "JPEG", FileType.Image },
            { "PNG", FileType.Image },
            { "JFIF", FileType.Image },
            { "TIF", FileType.Image },
            { "TIFF", FileType.Image },
            { "GIF", FileType.Image },
            { "SVG", FileType.Image },
            { "SVGZ", FileType.Image },
            { "WEBP", FileType.Image },
            { "ICO", FileType.Image },
            { "XBM", FileType.Image},
            { "PJP", FileType.Image },
            { "APNG", FileType.Image },
            { "PJPEG", FileType.Image },
            { "AVIF", FileType.Image },
            { "HEIC", FileType.Image },
        };
    }
}
