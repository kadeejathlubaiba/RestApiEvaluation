using Billing.Models;
using Billing.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace Billing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoryC : ControllerBase
    {
        private readonly IBillingInterface _ibill;

        //constructor injection
        public categoryC(IBillingInterface ibill)
        {
            _ibill = ibill;
        }

        #region Get all categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            return await _ibill.GetAllCategories();
        }

        #endregion

        #region add category


        [HttpPost]
        [Authorize]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddCategory([FromBody] Category category)

        {
            //validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    var categoryId = await _ibill.AddCategory(category);
                    if (categoryId > 0)
                    {
                        return Ok(categoryId);
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

        #region update category

        [HttpPut]
        [Authorize]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> UpdateCategory([FromBody] Category category)

        {
            //validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _ibill.UpdateCategory(category);
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


        #region find by id
        //https://localhost:44306/api/categoryC/3
        [HttpGet("{cid}")]
        public async Task<ActionResult<Category>> GetCategoryById(int? cid)
        {
            try
            {
                var cat = await _ibill.GetCategoryById(cid);
                if (cat == null)
                {
                    return NotFound();
                }
                return cat; 
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        #endregion


        #region delete
        [HttpDelete("{cid}")]
        public async Task<IActionResult> DeleteCategory(int? cid)
        {
            int result = 0;
            if (cid == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _ibill.DeleteCategory(cid);
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
        #endregion
    }
}



