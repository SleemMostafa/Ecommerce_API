using Ecommerce_API.DTO;
using Ecommerce_API.Models;
using Ecommerce_API.Ropsitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]// use token and token check her if is valid or not valid
    public class ProductController : ControllerBase
    {
      
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var query = productRepository.GetAll();
            
            return Ok(query);
        }
        [HttpGet("GetProductByCategoryId/{id:int}")]
        public IActionResult GetProductByCategoryId(int categoryId)
        {
            var query = productRepository.GetProductsByCategoryId(categoryId);
            if (query != null)
            {
                return Ok(query);
            }
            return BadRequest();
        }
        [HttpGet("{id:int}", Name = "getOneRoute")]
        public IActionResult GetById(int productId)
        {

            var query = productRepository.GetById(productId);
            if (query != null)
            {
                return Ok(query);
            }
            return BadRequest();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Insert(ProductTdo newProduct)
        {

            Product product = new Product();
            product.Name = newProduct.Name.ToUpper();
            product.Price = newProduct.Price;
            product.PurchaseDate = newProduct.PurchaseDate;
            product.Photo = newProduct.Photo;
            product.Quantity = newProduct.Quantity;
            product.Description = newProduct.Description;
            product.CateogryID = newProduct.CateogryID;
            if (ModelState.IsValid)
            {
                try
                {
                    var query = productRepository.Insert(product);
                    string url = Url.Link("getOneRoute", new { id = newProduct.ID });
                    return Created(url, newProduct);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPatch("{id:int}")]
        public IActionResult Edit(int productId,ProductTdo newProduct)
        {
            Product product = new Product();
            if (ModelState.IsValid)
            { 
                    product.Name = newProduct.Name.ToUpper();
                    product.Price = newProduct.Price;
                    product.Photo = newProduct.Photo;
                    product.PurchaseDate = newProduct.PurchaseDate;
                    product.Quantity = newProduct.Quantity;
                    product.Description = newProduct.Description;
                    product.CateogryID = newProduct.CateogryID;
                
                try
                {
                    productRepository.Update(productId, product);
                    return StatusCode(StatusCodes.Status204NoContent, "Data Saved"); 
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var query = productRepository.Delete(id);
            if(query == 1)
            {
                return Accepted();
            }
            return NoContent(); 
        }
    }
}
