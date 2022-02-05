using Billing.Models;
using Billing.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.Repository
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillRepository : IBillingInterface
    {
        private readonly billContext _context;

        public BillRepository(billContext context)
        {
            _context = context;
        }
        #region Category
        public async Task<List<Category>> GetAllCategories()
        {
            if (_context != null)
            {
                return await _context.Category.ToListAsync();
            }
            return null;
        }
        public async Task<int> AddCategory(Category category)
        {
            if (_context != null)
            {
                await _context.Category.AddAsync(category);
                await _context.SaveChangesAsync();//commit the transaction
                return category.Cid;
            }
            return 0;
        }
        public async Task UpdateCategory(Category category)
        {
            if (_context != null)
            {
                _context.Entry(category).State = EntityState.Modified;
                _context.Category.Update(category);
                await _context.SaveChangesAsync();

            }
        }
        public async Task<ActionResult<Category>> GetCategoryById(int? cid)
        {
            if (_context != null)
            {
                var cat = await _context.Category.FindAsync(cid);
                return null;
            }
            return null;
        }

        public async Task<int> DeleteCategory(int? cid)
        {
            int result = 0;
            if (_context != null)
            {
                var cat = await _context.Category.FirstOrDefaultAsync(c => c.Cid == cid);
                if (cat != null)
                {
                    //delete
                    _context.Category.Remove(cat);
                    //commit
                    result = await _context.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }
        #endregion

        #region Product
        public async Task<List<Products>> GetAllProducts()
        {
            if (_context != null)
            {
                return await _context.Products.ToListAsync();
            }
            return null;
        }
        public async Task<int> AddProduct(Products products)
        {
            if (_context != null)
            {
                await _context.Products.AddAsync(products);
                await _context.SaveChangesAsync();//commit the transaction
                return products.ProductCode;
            }
            return 0;
        }
        public async Task UpdateProduct(Products products)
        {
            if (_context != null)
            {
                _context.Entry(products).State = EntityState.Modified;
                _context.Products.Update(products);
                await _context.SaveChangesAsync();

            }
        }
        public async Task<ActionResult<Products>> GetProductById(int? productCode)
        {
            if (_context != null)
            {
                var prod = await _context.Products.FindAsync(productCode);
                return null;
            }
            return null;
        }
        public async Task<int> DeleteProduct(int? productCode)
        {
            int result = 0;
            if (_context != null)
            {
                var prod = await _context.Products.FirstOrDefaultAsync(p => p.ProductCode == productCode);
                if (prod != null)
                {
                    //delete
                    _context.Products.Remove(prod);
                    //commit
                    result = await _context.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }
        #endregion

        #region Gst
        public async Task<List<Gst>> GetAllGst()
        {
            if (_context != null)
            {
                return await _context.Gst.ToListAsync();
            }
            return null;
        }
        public async Task<int> AddGst(Gst gst)
        {
            if (_context != null)
            {
                await _context.Gst.AddAsync(gst);
                await _context.SaveChangesAsync();//commit the transaction
                return gst.GstId;
            }
            return 0;
        }
        public async Task UpdateGst(Gst gst)
        {
            if (_context != null)
            {
                _context.Entry(gst).State = EntityState.Modified;
                _context.Gst.Update(gst);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<ActionResult<Gst>> GetGstById(int? gstId)
        {
            if (_context != null)
            {
                var gst1 = await _context.Gst.FindAsync(gstId);
                return null;
            }
            return null;
        }
        public async Task<int> DeleteGst(int? gstId)
        {
            int result = 0;
            if (_context != null)
            {
                var gst1 = await _context.Gst.FirstOrDefaultAsync(g => g.GstId == gstId);
                if (gst1 != null)
                {
                    //delete
                    _context.Gst.Remove(gst1);
                    //commit
                    result = await _context.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        public async Task<List<ProductViewModel>> Getproductgstdetails()
        {
            if (_context != null)
            {
                //linq
                //join post and category
                return await (from p in _context.Products
                              join c in _context.Category
                              on p.Cid equals c.Cid
                              join g in _context.Gst
                              on c.Cid equals g.Cid
                              where c.Category1.Equals("mobilephone")
                              select new ProductViewModel
                              {
                                  Cid= c.Cid,
                                  Gstvalue = (float)g.Gstvalue,
                                  ProductCode = p.ProductCode,
                                  Qty = (short)p.Qty,
                                  RatePerUnit = (int)p.RatePerUnit,
                                  Net_Rate = (double)(g.Gstvalue * p.RatePerUnit)
                              }



                ).OrderBy(x => x.Gstvalue).ToListAsync();



            }
            return null;
        }
        #endregion

    }
}
