using Microsoft.AspNetCore.Http;namespace weblog.CoreLayer.Services.IFileManager;public class FileManager : IFileManager{
    public void DeleteFile(string fileName, string path)
    {
        var filepath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);        if (File.Exists(filepath))        {            File.Delete(filepath);        }
    }

    public string saveFile(IFormFile file, string savePath)    {
        if (file == null)            throw new Exception("File Is Null");        var fileName = $"{Guid.NewGuid()}{file.FileName}";        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/", "\\"));        if (!Directory.Exists(folderPath))        {            Directory.CreateDirectory(folderPath);        }        var fullPath = Path.Combine(folderPath, fileName);        using var stream = new FileStream(fullPath, FileMode.Create);        file.CopyTo(stream);        return fileName;    }}