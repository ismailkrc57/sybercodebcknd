using System;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private EfProductDal efProductDal;

        public ProductController()
        {
            efProductDal = new EfProductDal();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = efProductDal.GetAll();
            return Ok(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(string id)
        {
            var result = efProductDal.GetByIdNative(id);
            if (result != null)
                return Ok(result);
            return BadRequest(new { message = "Ürün bulunamadı." });
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            try
            {
                efProductDal.Add(product);
                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Ürün Eklenemedi" });
            }
        }
    }
}