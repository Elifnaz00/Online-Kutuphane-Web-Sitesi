using WebApplicationKendim.Utility;

namespace WebApplicationKendim.Models
{
    public class KitapTurleriRepository : Repository<KitapTurleri>, IKitapTurleriRepository
    {
        private UygulamaDbContext _uygulamaDbContext;
        public KitapTurleriRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext= uygulamaDbContext;
        }

        public void Guncelle(KitapTurleri kitapTurleri)
        {
            _uygulamaDbContext.Update(kitapTurleri);

        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
