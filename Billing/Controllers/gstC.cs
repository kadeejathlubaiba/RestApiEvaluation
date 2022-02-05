using Billing.Models;
using Billing.Repository;
using Billing.ViewModel;
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
    public class gstC : ControllerBase
    {
        private readonly IBillingInterface _ibill;
        public gstC(IBillingInterface ibill)
        {
            _ibill = ibill;
        }

        #region Get all gst
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gst>>> GetAllGst()
        {
            return await _ibill.GetAllGst();
        }

        #endregion

        #region add gst

        [HttpPost]
        [Authorize]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddGst([FromBody] Gst gst)

        {
            //validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    var gstId = await _ibill.AddGst(gst);
                    if (gstId > 0)
                    {
                        return Ok(gstId);
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

        #region update gst
        [HttpPut]
        [Authorize]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> UpdateGst([FromBody] Gst gst)

        {
            //validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _ibill.UpdateGst(gst);
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
        
        [HttpGet("{gstId}")]
        public async Task<ActionResult<Gst>> GetGstById(int? gstId)
        {
            try
            {
                var gst1 = await _ibill.GetGstById(gstId);
                if (gst1 == null)
                {
                    return NotFound();
                }
                return gst1;
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        #endregion


        #region delete
        [HttpDelete("{gstId}")]
        public async Task<IActionResult> DeleteGst(int? gstId)
        {
            int result = 0;
            if (gstId == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _ibill.DeleteGst(gstId);
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
        [HttpGet("gst")]

        public async Task<List<ProductViewModel>> Getproductgstdetails()
        {
            return await _ibill.Getproductgstdetails();



        }
    }
}




