using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public class ProductPicture : EntityBase
    {
        public long ProductId { get; private set; }
        public string Picture { get; private set; }
        public string PictureTitle { get; private set; }
        public string PictureAlt { get; private set; }
        public bool IsRemove { get; private set; }
        public Product Product { get; private set; }
        public ProductPicture(long productId, string picture, string pictureTitle, string pictureAlt)
        {
            ProductId = productId;
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            IsRemove = false;
        }

        public void Edit(long productId, string picture, string pictureTitle, string pictureAlt)
        {
            ProductId = productId;
            if(!string.IsNullOrWhiteSpace(picture)) Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;

        }

        public void Remove()
        {
            IsRemove = true;
        }
        public void Restore()
        {
            IsRemove = false;
        }
    }

}
