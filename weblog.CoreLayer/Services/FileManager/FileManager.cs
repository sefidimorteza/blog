﻿using Microsoft.AspNetCore.Http;
    public void DeleteFile(string fileName, string path)
    {
        var filepath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);
    }

    public string saveFile(IFormFile file, string savePath)
        if (file == null)