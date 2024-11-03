using NanoidDotNet;

namespace MessegnerBackend.Models
{
    public class FileSaver
    {
        public static async Task<FileInfo> SaveFileAsync(IFormFile file)
        {
            string id = Nanoid.Generate();
            
            string extension = Path.GetExtension(file.FileName);

            string path = $"./static/{id}{extension}";

            using Stream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return new FileInfo { Name = $"{id}{extension}", ContentType = file.ContentType };



        }
    }
}
