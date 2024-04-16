using Microsoft.EntityFrameworkCore;
using weblog.CoreLayer.DTOs.Category;
using weblog.CoreLayer.Mapper;
using weblog.CoreLayer.Utilities;
using weblog.DataLayer.Context;
using weblog.DataLayer.Entities;

namespace weblog.CoreLayer.Services.Categories;

public class categoryServie : ICategoryService
{

    private readonly BlogContext _context;

    public categoryServie(BlogContext context)
    {
        _context = context;
    }

    public OperationResult CreateCategory(CreateCategoryDto command)
    {
        if (IsSlugExist(command.Slug))
            return OperationResult.Error("slug is exist");
        var category = new DataLayer.Entities.Categories()
        {
            Title = command.Title,
            IsDelete = false,
            ParentId = command.ParentId,
            Slug = command.Slug.ToSlug(),
            MetaDescription = command.MetaDescription,
            MetaTag = command.MetaTag
        };
        _context.Categories.Add(category);
        _context.SaveChanges();
        return OperationResult.Success();
    }

    public OperationResult EditCategory(EditCategoryDto command)
    {

        var category = _context.Categories.FirstOrDefault(c => c.Id == command.Id);
        if (category == null) return OperationResult.Error();
        if (command.Slug.ToSlug() != category.Slug)
            if (IsSlugExist(command.Slug))
                return OperationResult.Error("slug is exist");

        category.Title = command.Title;
        category.Slug = command.Slug.ToSlug();
        category.MetaDescription = command.MetaDescription;
        category.MetaTag = command.MetaTag;
        _context.SaveChanges();
        return OperationResult.Success();
    }

  
    public List<CategoryDto> GetAllCategory()
    {
        return _context.Categories
            .Select(category => CategoryMapper.Map(category))
            .ToList();
    }

    public List<CategoryDto> GetChildCategories(int parentId)
    {
        return [.. _context.Categories.Where(r => r.ParentId == parentId).Select(category => CategoryMapper.Map(category))];
    }

    public CategoryDto GetCategoryBy(int Id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == Id);
        if (category == null)
            return null;
        return CategoryMapper.Map(category);
    }

    public CategoryDto GetCategoryBy(string slug)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Slug == slug);
        if (category == null)
            return null;
        return CategoryMapper.Map(category);
    }

    public bool IsSlugExist(string slug)
    {
        return _context.Categories.Any(s => s.Slug == slug.ToSlug());
    }
}