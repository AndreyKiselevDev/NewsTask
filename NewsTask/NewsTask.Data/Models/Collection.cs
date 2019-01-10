using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsTask.Data.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<SourceCollection> SourceCollections { get; set; }
    }
}
