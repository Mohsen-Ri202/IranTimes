using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IranTimes.Models
{
    public class IndexViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }   
        public string Email { get; set; }
        public IEnumerable<string> RoleName { get; set; }
    }
}
