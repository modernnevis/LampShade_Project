using System;
using System.Collections.Generic;
using System.Text;
using _01_lampshadeQuery.Contracts.ProductPicture;

namespace _01_lampshadeQuery.Contracts.Product
{
    public class ProductQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string PriceWithDiscount { get; set; }
        public long DiscountRate { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Category { get; set; }
        public string Slug { get; set; }
        public bool HasDiscount { get; set; }
        public string ShortDescription { get; set; }
        public string EndDate { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public List<ProductPictureQueryModel> ProductPictures { get; set; }
        public string CategorySlug { get; set; }
        public string Code { get; set; }
        public bool IsInStock { get; set; }
        public string Description { get; set; }
    }
}