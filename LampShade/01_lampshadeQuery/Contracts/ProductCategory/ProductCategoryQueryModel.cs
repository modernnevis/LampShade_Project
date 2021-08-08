﻿using System.Collections.Generic;
using _01_lampshadeQuery.Contracts.Product;

namespace _01_lampshadeQuery.Contracts.ProductCategory
{
    public class ProductCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Slug { get; set; }
        public string Keywords { get;  set; }
        public string MetaDescription { get;  set; }
        public string Description { get;  set; }


        public List<ProductQueryModel> Products { get; set; }
    }
}