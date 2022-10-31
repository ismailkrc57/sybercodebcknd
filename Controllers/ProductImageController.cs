using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Utilities.Helpers;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly EfProductImageDal _productImageDal;
        private readonly EfProductDal _productDal;

        public ProductImageController()
        {
            _productImageDal = new EfProductImageDal();
            _productDal = new EfProductDal();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productImageDal.GetAll();
            return Ok(result);
        }

        [HttpGet("getbyproductid")]
        public IActionResult GetByProductId(int productId)
        {
            var result = _productImageDal.Get(p => p.ProductId == productId);
            return Ok(result);
        }


        [HttpPost("add")]
        public IActionResult Add(int productId, [FromForm(Name = "image")] IFormFile image)
        {
            Account account = new Account(
                "dpuqjnkvs",
                "989851641288737",
                "iXmpqJaphChRE7M6nUI2yANUh-E");

            Cloudinary cloudinary = new Cloudinary(account);

            var productImage = new ProductImage();
            var imageResult = FileHelper.Upload(image);

            if (imageResult.Success)
            {
                ImageUploadParams imageUploadParams = new ImageUploadParams()
                {
                    File = new FileDescription("wwwroot" + imageResult.Message)
                };
                ImageUploadResult result = cloudinary.Upload(imageUploadParams);
                productImage.ImagePath = result.Url.ToString();
                productImage.ProductId = productId;
                _productImageDal.Add(productImage);
                Product product = _productDal.Get(p => p.Id == productId);
                product.ImageUrl = productImage.ImagePath;
                _productDal.Update(product);
            }

            return Ok(productImage);
        }
        //
        // [HttpPost("delete")]
        // public IActionResult Delete(CarImage carImage)
        // {
        //     var result = _carImageService.Delete(carImage);
        //     if (result.Success)
        //     {
        //         return Ok(result);
        //     }
        //
        //     return BadRequest(result.Message);
        // }
    }
}