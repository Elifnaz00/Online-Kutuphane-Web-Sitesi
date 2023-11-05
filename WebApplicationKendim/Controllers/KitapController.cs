using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationKendim.Models;
using WebApplicationKendim.Utility;

namespace WebApplicationKendim.Controllers
{

    public class KitapController : Controller
    {
        private IKitapRepository _kitapRepository;
        private IKitapTurleriRepository _kitapTurleriRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KitapController(IKitapRepository kitapRepository, IKitapTurleriRepository kitapTurleriRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kitapTurleriRepository = kitapTurleriRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin , Ogrenci")]
        public IActionResult Index()
        {
            List<Kitap> objKtapTuruList = _kitapRepository.GetAll(includeProps:"Kitapturleri").ToList();
            return View(objKtapTuruList);


        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapTuruList = _kitapTurleriRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Ad,
                    Value = k.Id.ToString()

                });

            ViewBag.KitapTuruList= KitapTuruList;

            if (id == null || id == 0) 
            {
                return View();
            }
            else
            {

                Kitap? kitapTuruVt = _kitapRepository.Get(u => u.Id == id);
                if (kitapTuruVt == null)
                {
                    return NotFound();
                }
                return View(kitapTuruVt);

            }
            
                     
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(Kitap kitap, IFormFile? file )
        {
            var errors= ModelState.Values.SelectMany(x => x.Errors);
            
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");

                if( file != null ) 
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;

                }
                
                
                if(kitap.Id== 0)
                {
                    _kitapRepository.Ekle(kitap);
                    TempData["basarili"] = "Yeni Kitap Türü Başarıyla Oluşturuldu.";
                }
                else
                {
                    _kitapRepository.Guncelle(kitap);
                    TempData["basarili"] = " Kitap Güncelleme Başarılı! ";
                }
               
                _kitapRepository.Kaydet();
                
                return RedirectToAction("Index", "Kitap");
            }
            return View();


        }
        
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Kitap? kitapTuruVt = _kitapRepository.Get(u => u.Id == id);
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);

        }

        [HttpPost, ActionName("Sil")]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult SilPOST(int? id)

        {
            Kitap? kitap = _kitapRepository.Get(u => u.Id == id);
            if (ModelState.IsValid)
            {
                _kitapRepository.Sil(kitap);
                _kitapRepository.Kaydet();
                TempData["basarili"] = "Kayıt Silme İşlemi Başarıyla Güncellendi";
                return RedirectToAction("Index", "Kitap");
            }
            return View();


        }

    }
}

