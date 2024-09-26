using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Tesseract;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace ImageTextExtraction.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage()
        {
            var file = Request.Form.Files[0];
            if (file == null || file.Length == 0)
            {
                ViewBag.ErrorMessage = "Please select a valid file to upload.";
                return View("Index");
            }

            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string fileName = Path.GetFileName(file.FileName);
            string filePath = Path.Combine(uploadFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            ViewBag.ImagePath = "/uploads/" + fileName;
            ViewBag.SelectedLanguage = Request.Form["selectedLanguage"];

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ExtractText(string imageUrl, string selectedLanguage)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                ViewBag.ErrorMessage = "Please upload an image first.";
                return View("Index");
            }

            if (string.IsNullOrEmpty(selectedLanguage))
            {
                ViewBag.ErrorMessage = "Please select a language.";
                return View("Index");
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageUrl.TrimStart('/'));
            string tessDataPath = Path.Combine(Directory.GetCurrentDirectory(), "tessdata");

            try
            {
                using (Bitmap selectedImage = new Bitmap(filePath))
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    selectedImage.Save(memoryStream, ImageFormat.Png);
                    byte[] imageBytes = memoryStream.ToArray();

                    using (var ocrEngine = new TesseractEngine(tessDataPath, selectedLanguage, EngineMode.Default))
                    using (var pixImage = Pix.LoadFromMemory(imageBytes))
                    using (var page = ocrEngine.Process(pixImage))
                    {
                        string extractedText = page.GetText();
                        ViewBag.ExtractedText = extractedText;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }

            ViewBag.ImagePath = imageUrl;
            return View("Index");
        }

        [HttpPost]
        public IActionResult CopyText(string textToCopy)
        {
            if (!string.IsNullOrEmpty(textToCopy))
            {
                // Use JavaScript to copy text to clipboard
                TempData["CopiedText"] = textToCopy;
                return Content("Text copied to clipboard!", "text/plain");
            }

            return BadRequest("No text to copy.");
        }
    }
}
