﻿using System.Collections.Generic;


namespace DataLogicLayer.Models
{
    public class PostIndexViewModel
    {
        public int Id { get; set; }
        public ICollection<Post> Posts { get; set; }
        public Post Post { get; set; } = new Post();
    }
}