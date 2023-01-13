using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PDFtoImage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiForPdfToPng.Controllers
{
    [Route("api/[controller]/")]

    [ApiController]
    public class PdfToPngController : ControllerBase
    {
        // GET: api/<PdfToPngController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public byte[] Post(IFormCollection formData)
        {
            try
            {
                byte[] pdfByteArray = null;
                var files = formData.Files;
                MemoryStream memoryStream = new MemoryStream();
                files[0].CopyTo(memoryStream);
                MemoryStream s = new MemoryStream();
                Conversion.SavePng(s, memoryStream, null, 0, 300, null, null, true, true);
                return s.ToArray();
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}


//if (file != null)
//{
//    using var memoryStream = new MemoryStream();
//    file!.CopyToAsync(memoryStream);
//    pdfByteArray = memoryStream.ToArray();
//}

//foreach (var img in imagesList)
//{
//    using (Image image = Image.FromStream(new MemoryStream(img)))
//    {
//        image.Save("output.png", ImageFormat.Png);
//    }
//}


// var imagesList = Freeware.Pdf2Png.ConvertAllPages(pdfByteArray);