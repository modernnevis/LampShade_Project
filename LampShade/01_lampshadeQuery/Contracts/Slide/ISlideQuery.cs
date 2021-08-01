using System;
using System.Collections.Generic;
using System.Text;

namespace _01_lampshadeQuery.Contracts.Slide
{
    public interface ISlideQuery
    {
        List<SlideQueryModel> GetList();
    }
}
