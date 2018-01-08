﻿using DejlienApp.Framework;

namespace DejlienApp.Models
{
    public class Post: IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual Profile Author { get; set; }
        public virtual Profile Receiver { get; set; }

    }
}
