using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeoManager.Entities
{
    public class VaiTro
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 1)]
        public int Id { get; set; }
        public string Ten { get; set; }
    }
}