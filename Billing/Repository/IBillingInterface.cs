using Billing.Models;
using Billing.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.Repository
{
    public interface IBillingInterface
    {
        #region Category Interface
        //category
        Task<List<Category>> GetAllCategories(); 

        //add category
        Task<int> AddCategory(Category category);

        //update category 
        Task UpdateCategory(Category category);

        //find 
        Task<ActionResult<Category>> GetCategoryById(int? id);

        //delete
        Task<int> DeleteCategory(int? id);
        #endregion

        #region Product interface
        //products
        Task<List<Products>> GetAllProducts();

        //add product
        Task<int> AddProduct(Products products);

        //update product
        Task UpdateProduct(Products products);

        //find 
        Task<ActionResult<Products>> GetProductById(int? id);

        //delete
        Task<int> DeleteProduct(int? id);
        #endregion

        #region GST interface
        //products
        Task<List<Gst>> GetAllGst();

        //add product
        Task<int> AddGst(Gst gst);

        //update product
        Task UpdateGst(Gst gst);

        //find 
        Task<ActionResult<Gst>> GetGstById(int? id);

        //delete
        Task<int> DeleteGst(int? id);
        #endregion

        Task<List<ProductViewModel>> Getproductgstdetails();
    }
}
