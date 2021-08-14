﻿using NewShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IranTimes.Models
{
    public class CreateViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0}را وارد نمایید")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0}را وارد نمایید")]
        [MaxLength(150)]
        public string ShortDescription { get; set; }
        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0}را وارد نمایید")]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        [Display(Name = "بازدید")]
        public int Visit { get; set; }
        [Required]
        [Display(Name = "نمایش در اسلاید")]
        public bool ShowInSlider { get; set; }
        [Display(Name = "عکس")]
        public string ImageName { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime CreateDate { get; set; }
        public int PageGroupId { get; set; }
        public IEnumerable<PageGroup> PageGroups { get; set; }
    }
}
