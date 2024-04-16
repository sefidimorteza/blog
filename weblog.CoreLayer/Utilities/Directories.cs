﻿namespace weblog.CoreLayer.Utilities;public class Directories{    public const string PostImage = "wwwroot/images/posts";    public const string PostContentImage = "wwwroot/images/posts/content";    public static string GetPostImage(string imageName) =>
        $"{PostImage.Replace("wwwroot", "")}/{imageName}";
    public static string GetPostContentImage(string imageName) =>
        $"{PostContentImage.Replace("wwwroot", "")}/{imageName}";}