using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationKendim.Models;
using WebApplicationKendim.Utility;

namespace WebApplicationKendim.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KiralamaController : Controller
    {
        private IKitapRepository _kitapRepository;
        private IKiralamaRepository _kiralamaRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kiralamaRepository = kiralamaRepository; 
        } 
        
        
        
        [HttpGet]
        public IActionResult Index() 
        {
            List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(includeProps: "Kitap").ToList();
            return View(objKiralamaList);
        }

        [HttpGet]
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    // Text = k.Kitap.KitapAdi,
                    Text = k.KitapAdi,
                    Value = k.Id.ToString(),

                }); 
            ViewBag.KitapList = KitapList;  

            if( id == null || id ==0)
            {
                return View();
            }
            else
            {
                Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);
                if (kiralamaVt == null)
                {
                    return NotFound();
                }
                return View(kiralamaVt);

            }

        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama kiralama)
        {
            

            if (ModelState.IsValid)
            {

                if (kiralama.Id == 0)
                {
                    _kiralamaRepository.Ekle(kiralama);
                    TempData["basarili"] = "Yeni Kitap Türü Başarıyla Oluşturuldu.";
                }
                else
                {
                    _kiralamaRepository.Guncelle(kiralama);
                    TempData["basarili"] = " Kitap Güncelleme Başarılı! ";
                }

                _kiralamaRepository.Kaydet();

                return RedirectToAction("Index", "Kiralama");
            }
            return View();

        }

        [HttpGet]
        public IActionResult Sil(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
               .Select(k => new SelectListItem
               {
                   // Text = k.Kitap.KitapAdi,
                   Text = k.KitapAdi,
                   Value = k.Id.ToString(),

               });
            ViewBag.KitapList = KitapList;


            if (id == null || id ==0) 
            { return NotFound(); }

            Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);
            if (kiralamaVt == null)
            {
                return NotFound();
            }
            return View(kiralamaVt);

        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);
            if (ModelState.IsValid)
            {
                _kiralamaRepository.Sil(kiralama);
                _kiralamaRepository.Kaydet();
                TempData["basarili"] = "Kayıt Silme İşlemi Başarıyla Güncellendi";
                return RedirectToAction("Index", "Kiralama");
            }
            return View();
        }

        
    }
}
