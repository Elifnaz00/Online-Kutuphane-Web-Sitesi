namespace WebApplicationKendim.Models
{
    public interface IKitapRepository : IRepository<Kitap>
    {
        void Guncelle(Kitap kitap);

        void Kaydet();
    }
}