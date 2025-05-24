using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models
{
    public class AddBook
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Edit { get; set; }

        public string Type { get; set; } //gen

        public string Image { get; set; } //imagine
    }
}