﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using _0_Framework.Domain;

namespace ShopManagement.Domain.SlideAgg
{
    public class Slide : EntityBase
    {
        public string Picture { get;private set; }
        public string PictureAlt { get;private set; }
        public string PictureTitle { get;private set; }
        public string Heading { get;private set; }
        public string Title { get;private set; }
        public string Text { get;private set; }
        public string BtnText { get;private set; }
        public bool IsRemove { get;private set; }

        public Slide(string picture, string pictureAlt, string pictureTitle, string heading, string title, string text, string btnText)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            IsRemove = false;
        }

        public void Edit(string picture, string pictureAlt, string pictureTitle, string heading, string title, string text, string btnText)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            IsRemove = false;
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
