namespace WebApplicationKendim.Models
{
    public interface IKitapTurleriRepository : IRepository<KitapTurleri>
    {
        void Guncelle(KitapTurleri kitapTurleri);

        void Kaydet();
    }
}