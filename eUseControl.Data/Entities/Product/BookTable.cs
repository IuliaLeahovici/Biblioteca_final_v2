using eUseControl.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Data.Entities.Product
{
    public class BookTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Author")]
        public string Author { get; set; }

        [Display(Name = "Edit")]
        public string Edit { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Bought")]
        public int Bought { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }
    }
}
