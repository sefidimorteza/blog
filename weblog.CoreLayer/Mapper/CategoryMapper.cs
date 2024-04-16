﻿using weblog.CoreLayer.DTOs.Category;


public class CategoryMapper
{
    public static CategoryDto Map(Categories category)
    {
        return new CategoryDto()
        {
            MetaDescription = category.MetaDescription,
            MetaTag = category.MetaTag,
            Slug = category.Slug,
            ParentId = category.ParentId,
            Id = category.Id,
            Title = category.Title
        };
    }
}