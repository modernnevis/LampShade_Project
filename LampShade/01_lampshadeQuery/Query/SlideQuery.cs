using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _01_lampshadeQuery.Contracts.Slide;
using ShopManagement.Infrastructure.EfCore;

namespace _01_lampshadeQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        public readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetList()
        {
            return _context.Slides.Where(x => x.IsRemove == false).Select(x => new SlideQueryModel()
            {
                Picture = x.Picture,
                BtnText = x.BtnText,
                Heading = x.Heading,
                Link = x.Link,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text,
                Title = x.Title
            }).ToList();
        }
    }
}
