using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;

namespace apiForPdfToPng.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetConvertedPdf")]
        public List<byte[]> GetConvertedPdf(byte[] pdfByteArray)
        {
            //var pdfByteArray = File.ReadAllBytes("GT0506.pdf");
            var imagesList = Freeware.Pdf2Png.ConvertAllPages(pdfByteArray);
            // To save total pdf as images
            //foreach(var img in imagesList)
            //{
            //    using (Image image = Image.FromStream(new MemoryStream(img)))
            //    {
            //        image.Save("output.png", ImageFormat.Png);
            //    }
            //}
            return imagesList;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post(IFormFile file)
        {
            byte[] pdfByteArray = null;
            try
            {
                if (file != null)
                {
                    using var memoryStream = new MemoryStream();
                    file!.CopyToAsync(memoryStream);
                    pdfByteArray = memoryStream.ToArray();
                }
                var imagesList = Freeware.Pdf2Png.ConvertAllPages(pdfByteArray);
                //foreach (var img in imagesList)
                //{
                //    using (Image image = Image.FromStream(new MemoryStream(img)))
                //    {
                //        image.Save("output.png", ImageFormat.Png);
                //    }
                //}
                return File(imagesList[0], "image/jpng");

            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        //[HttpGet(Name = "ConvertToPng")]
        //public List<byte[]> ConvertToPng(byte[] pdfByteArray)
        //{
        //    var imagesList = Freeware.Pdf2Png.ConvertAllPages(pdfByteArray);
        //    // To save total pdf as images
        //    //foreach(var img in imagesList)
        //    //{
        //    //    using (Image image = Image.FromStream(new MemoryStream(img)))
        //    //    {
        //    //        image.Save("output.png", ImageFormat.Png);
        //    //    }
        //    //}
        //    return imagesList;
        //}
    }
}