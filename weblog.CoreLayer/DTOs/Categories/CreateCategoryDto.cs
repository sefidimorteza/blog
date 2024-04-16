using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weblog.CoreLayer.DTOs.Category
{


    public class CreateCategoryDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }

    }
}
