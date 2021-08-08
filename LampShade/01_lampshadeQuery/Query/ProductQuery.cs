using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using _0_Framework.Application;
using _01_lampshadeQuery.Contracts.Product;
using _01_lampshadeQuery.Contracts.ProductPicture;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EfCore;

namespace _01_lampshadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext context, InventoryContext inventoryContext,
            DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public ProductQueryModel GetBy(string slug)
        {
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.ProductId, x.DiscountRate, x.EndDate}).AsNoTracking();
            ;
            var inventory = _inventoryContext.Inventory.Select(x => new {x.ProductId, x.UnitPrice, x.InStock})
                .AsNoTracking();

            var productResult = _context.Products.Include(x => x.Category)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    Category = product.Category.Name,
                    CategorySlug = product.Category.Slug,
                    Name = product.Name,
                    Code = product.Code,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug,
                    Keywords = product.Keywords,
                    MetaDescription = product.MetaDescription,
                    Description = product.Description,
                    ProductPictures = MapProductPicture(product.ProductPictures)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);


            var inventoryProduct = inventory.FirstOrDefault(x => x.ProductId == productResult.Id);
            if (inventoryProduct != null)
            {
                if (productResult != null)
                {
                    productResult.IsInStock = inventoryProduct.InStock;
                    productResult.Price = inventoryProduct.UnitPrice.ToMoney();
                    var discount = discounts.FirstOrDefault(x => x.ProductId == productResult.Id);
                    if (discount != null)
                    {
                        productResult.EndDate = discount.EndDate.ToDiscountFormat();

                        productResult.HasDiscount = discount.DiscountRate > 0;
                        productResult.DiscountRate = discount.DiscountRate;
                        var discountPrice = Math.Round((inventoryProduct.UnitPrice * discount.DiscountRate) / 100);
                        productResult.PriceWithDiscount = (inventoryProduct.UnitPrice - discountPrice).ToMoney();
                    }
                }
            }


            return productResult;
        }

        private static List<ProductPictureQueryModel> MapProductPicture(List<ProductPicture> productPictures)
        {
            return productPictures.Where(x => !x.IsRemove).Select(x => new ProductPictureQueryModel()
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).ToList();
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.ProductId, x.DiscountRate});
            var inventory = _inventoryContext.Inventory.Select(x => new {x.ProductId, x.UnitPrice}).ToList();

            var products = _context.Products.Include(x => x.Category)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    Category = product.Category.Name,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug
                }).OrderByDescending(x => x.Id).Take(6).ToList();

            foreach (var product in products)
            {
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

            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            {
                var inventory = _inventoryContext.Inventory.Select(x =>
                    new { x.ProductId, x.UnitPrice }).ToList();
                var discounts = _discountContext.CustomerDiscounts
                    .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                    .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

                var query = _context.Products
                    .Include(x => x.Category)
                    .Select(product => new ProductQueryModel
                    {
                        Id = product.Id,
                        Category = product.Category.Name,
                        CategorySlug = product.Category.Slug,
                        Name = product.Name,
                        Picture = product.Picture,
                        PictureAlt = product.PictureAlt,
                        PictureTitle = product.PictureTitle,
                        ShortDescription = product.ShortDescription,
                        Slug = product.Slug
                    }).AsNoTracking();

                if (!string.IsNullOrWhiteSpace(value))
                    query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));

                var products = query.OrderByDescending(x => x.Id).ToList();
                ;

                foreach (var product in products)
                {
                    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productInventory != null)
                    {
                        var price = productInventory.UnitPrice;
                        product.Price = price.ToMoney();
                        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (discount == null) continue;

                        var discountRate = discount.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.EndDate = discount.EndDate.ToDiscountFormat();
                        product.HasDiscount = discountRate > 0;
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }

                return products;
            }
        }
    }
}