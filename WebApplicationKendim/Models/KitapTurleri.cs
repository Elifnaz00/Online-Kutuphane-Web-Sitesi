using System.ComponentModel.DataAnnotations;

namespace WebApplicationKendim.Models
{
    public class KitapTurleri
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage ="Kitap Türü Adı Boş Bırakılamaz!")]
        [MaxLength(30)]
        public string Ad { get; set; }
    }
}
