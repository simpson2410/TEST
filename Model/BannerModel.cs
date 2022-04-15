﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BannerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public IFormFile FileImage { get; set; }
        public string Url { get; set; }
        public int Type { get; set; }
        public string LinkWeb { get; set; }
        public int Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public bool IsEnglish { get; set; }
    }
}
