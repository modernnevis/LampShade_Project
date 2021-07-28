using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class SlideRepository : RepositoryBase<long,Slide> , ISlideRepository
    {

        private readonly ShopContext _context;

        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(x => new EditSlide
            {
                Id = x.Id,
                BtnText = x.BtnText,
                Heading = x.Heading,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text,
                Title = x.Title
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(x => new SlideViewModel
            {
                Id = x.Id,
                Heading = x.Heading,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                IsRemove = x.IsRemove,
                Title = x.Title
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
