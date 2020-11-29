using System;
using System.ComponentModel.DataAnnotations;

namespace CardApplication.DataAccess.Models
{
    public class CardDbModel
    {
        [Required]
        public long CardNumber { get; set; }

        [Required]
        public string CardGuid { get; set; } = new Guid().ToString();
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        public DateTime Expiry { get; set; }
    }
}
