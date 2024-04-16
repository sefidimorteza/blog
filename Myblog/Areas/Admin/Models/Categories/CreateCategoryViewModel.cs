using System.ComponentModel.DataAnnotations;
using weblog.CoreLayer.DTOs.Category;

namespace Myblog.Areas.Admin.Models.Categories;

public class CreateCategoryViewModel
{

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    public string Title { get; set; }

    [Display(Name = "Slug")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    public string Slug { get; set; }

    [Display(Name = "MetaTag (با خط - جدا کنید)")]
    public string MetaTag { get; set; }

    [DataType(DataType.MultilineText)]
    public string MetaDescription { get; set; }

    public int? ParentId { get; set; }

    public CreateCategoryDto MapCategoryDto()
    {
        return new CreateCategoryDto()
        {
            MetaDescription = MetaDescription,
            MetaTag = MetaTag,
            Slug = Slug,
            ParentId = ParentId,
            Title = Title
        };
    }



}