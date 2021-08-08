using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using _0_Framework.Application;
using _01_lampshadeQuery.Contracts.Product;
using _01_lampshadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EfCore;

namespace _01_lampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext,
            DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel()
            {
                Picture = x.Picture,
                Name = x.Name,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.ProductId, x.DiscountRate});
            var inventory = _inventoryContext.Inventory.Select(x => new {x.ProductId, x.UnitPrice}).ToList();

            var categories = _context.ProductCategories.Include(x => x.Products).ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProduct(x.Products)
                }).ToList();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    //product.Price = inventory.FirstOrDefault(x => x.ProductId == product.Id)?.UnitPrice.ToMoney();
                    var price = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (price != null)
                    {
                        product.Price = price.UnitPrice.ToMoney();
                        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (discount != null)
                        {
                            product.HasDiscount = discount.DiscountRate > 0;
                            product.DiscountRate = discount.DiscountRate;
                            var discountPrice = Math.Round((price.UnitPrice * discount.DiscountRate) / 100);
                            product.PriceWithDiscount = (price.UnitPrice - discountPrice).ToMoney();
                        }
                    }
                }
            }

            return categories;
        }

        public ProductCategoryQueryModel GetProductCategoryWithProducts(string slug)
        {
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.ProductId, x.DiscountRate,x.EndDate});
            var inventory = _inventoryContext.Inventory.Select(x => new {x.ProductId, x.UnitPrice}).ToList();

            var category = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Description = x.Description,
                    Slug = x.Slug,
                    Products = MapProduct(x.Products)
                }).FirstOrDefault(x => x.Slug == slug);


            if (category != null)
            {
                foreach (var product in category.Products)
                {
                    var price = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (price != null)
                    {
                        product.Price = price.UnitPrice.ToMoney();
                        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (discount != null)
                        {
                            product.EndDate = discount.EndDate.ToDiscountFormat();
                            product.HasDiscount = discount.DiscountRate > 0;
                            product.DiscountRate = discount.DiscountRate;
                            var discountPrice = Math.Round((price.UnitPrice * discount.DiscountRate) / 100);
                            product.PriceWithDiscount = (price.UnitPrice - discountPrice).ToMoney();
                        }
                    }
                }
            }

            return category;
        }

        private static List<ProductQueryModel> MapProduct(List<Product> products)
        {
            return products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Category = product.Category.Name,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
                ShortDescription = product.ShortDescription
            }).ToList();

            //var result = new List<ProductQueryModel>();
            //foreach (var product in products)
            //{
            //    var item = new ProductQueryModel
            //    {
            //        Id = product.Id,
            //        Category = product.Category.Name,
            //        Name = product.Name,
            //        Picture = product.Picture,
            //        PictureAlt = product.PictureAlt,
            //        PictureTitle = product.PictureTitle,
            //        Slug = product.Slug
            //    };
            //    result.Add(item);
            //}

            //return result;
        }
    }
}