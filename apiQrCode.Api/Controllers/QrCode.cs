using System;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QRCoder;
using apiQrCode.Api.Models;
using apiQrCode.Api.Services;

namespace apiQrCode.Api.Controllers
{
    [Route("generate-qrcode")]
    [ApiController]
    public class QrCode : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public ActionResult Get([FromBody]ContentQrCode content)
        {
            var environmentVar = Environment.GetEnvironmentVariable("DATABASE_URL");
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            var json = JsonConvert.SerializeObject(content.Text + environmentVar);

            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(json.Substring(1, json.Length - 2), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            var bitmapBytes = ConverterBitmapBytes.BitmapToBytes(qrCodeImage);
            return File(bitmapBytes, "image/jpeg");
        }
    }
}