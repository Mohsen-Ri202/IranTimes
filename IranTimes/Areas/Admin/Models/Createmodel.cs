using NewShop.Models;
using System.Collections.Generic;


namespace NewShop
{
    public class Createmodel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Text { get; set; }
        public bool ShowInSlider { get; set; }
        public string ImageName { get; set; }
        public IEnumerable<PageGroup> PageGroup { get; set; }

    }
}
