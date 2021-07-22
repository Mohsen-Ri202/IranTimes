using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Data.Models;

namespace Data
{
    class Page
    {
        [Key]
        public int id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Text { get; set; }
        public int Visit { get; set; }
        public bool ShowInSlider { get; set; }
        public string ImageName { get; set; }
        public DateTime CreateDate { get; set; }
        public List<PageGroup>  PageGroups { get; set; }
    }
}
