﻿using System.ComponentModel.DataAnnotations;namespace Myblog.Areas.Admin.Models.Categories;public class EditCategoryViewModel
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

}