using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
            byte[] pdfByteArray = null;
            try
            {
                var files = formData.Files;
                foreach (var file in files) 
                {
                    if (file.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        file!.CopyToAsync(memoryStream);
                        pdfByteArray = memoryStream.ToArray();
                    }
                }
                        var imagesList = Freeware.Pdf2Png.ConvertAllPages(pdfByteArray);
                
                return imagesList[0];

            }
            catch (Exception)
            {
                throw;
            }
            //return Ok();
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