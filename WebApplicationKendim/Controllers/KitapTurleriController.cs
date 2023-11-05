using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationKendim.Models;
using WebApplicationKendim.Utility;

namespace WebApplicationKendim.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KitapTurleriController : Controller
    {
       
        private IKitapTurleriRepository _kitapTurleriRepository;

        public KitapTurleriController(IKitapTurleriRepository context)
        {
            _kitapTurleriRepository = context;
        }
        public IActionResult Index()
        {
            List<KitapTurleri> objKtapTuruList= _kitapTurleriRepository.GetAll().ToList();
            return View(objKtapTuruList);

               
        }

        public IActionResult Ekle() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(KitapTurleri kitapTurleri)
        {
            if( ModelState.IsValid ) 
            {
                _kitapTurleriRepository.Ekle(kitapTurleri);
                _kitapTurleriRepository.Kaydet();
                TempData["basarili"] = "Yeni Kitap Türü Başarıyla Oluşturuldu";
                return RedirectToAction("Index", "KitapTurleri");
            }
            return View();


        }

        public IActionResult Guncelle(int? id)
        { 
            if( id == null || id== 0) 
            {
                return NotFound();
            }
            
            KitapTurleri? kitapTuruVt= _kitapTurleriRepository.Get(u=>u.Id==id);
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);

        }

        [HttpPost]
        public IActionResult Guncelle(KitapTurleri kitapTurleri)
        {
            if (ModelState.IsValid)
            {
                _kitapTurleriRepository.Guncelle(kitapTurleri);
                _kitapTurleriRepository.Kaydet();
                TempData["basarili"] = "Yeni Kitap Türü Başarıyla Güncellendi";
                return RedirectToAction("Index", "KitapTurleri");
            }
            return View();


        }

        public IActionResult Sil (int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            KitapTurleri? kitapTuruVt = _kitapTurleriRepository.Get(u => u.Id == id);
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);

        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)

        {
            KitapTurleri? kitapTuru = _kitapTurleriRepository.Get(u => u.Id == id);
            if (ModelState.IsValid)
            {
                _kitapTurleriRepository.Sil(kitapTuru);
                _kitapTurleriRepository.Kaydet();
                TempData["basarili"] = "Kayıt Silme İşlemi Başarıyla Güncellendi";
                return RedirectToAction("Index", "KitapTurleri");
            }
            return View();


        }

    }
}
