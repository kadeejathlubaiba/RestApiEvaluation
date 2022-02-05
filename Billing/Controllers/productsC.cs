using Billing.Models;
using Billing.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productsC : ControllerBase
    {
        private readonly IBillingInterface _ibill;

        //constructor injection
        public productsC(IBillingInterface ibill)
        {
            _ibill = ibill;
        }

        #region Get all Products
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Products>>> GetAllProducts()
        {
            return await _ibill.GetAllProducts();
        }

        #endregion

        #region add product

        [HttpPost]
        [Authorize]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddProduct([FromBody] Products products)

        {
            //validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    var productId = await _ibill.AddProduct(products);
                    if (productId > 0)
                    {
                        return Ok(productId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();

        }


        #endregion

        #region update Product
        [HttpPut]
        [Authorize]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> UpdateProduct([FromBody] Products products)

        {
            //validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _ibill.UpdateProduct(products);
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        #endregion


        #region find 
        [HttpGet("{productCode}")]
        public async Task<ActionResult<Products>> GetProductById(int? productCode)
        {
            try
            {
                var prod = await _ibill.GetProductById(productCode);
                if (prod == null)
                {
                    return NotFound();
                }
                return prod; 
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        #endregion


        [HttpDelete("{productCode}")]
        public async Task<IActionResult> DeleteProduct(int? productCode)
        {
            int result = 0;
            if (productCode == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _ibill.DeleteCategory(productCode);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok(); 
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}



