﻿///////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ch24ShoppingCartMVC.Models;
using Ch24ShoppingCartMVC.Models.DataAccess;

namespace Ch24ShoppingCartMVC.Models {
   public class OrderModel {
      private List<ProductViewModel> products;

      //Implement GetAllProductsFromDataStore
      public List<Product> GetAllProductsFromDataStore() {
         using (HalloweenEntities1 data = new HalloweenEntities1()) {
                //get all the products from the Collection Products order by name using HalloweenEntities
                return data.Products.OrderBy(p => p.Name).ToList();
               // return data.Products.OrderBy(p => p.Name).ToList();
         }//end using
      }//close GetAllProductsFromDataStore()


      //Implement the method ConvertToViewModel
      private ProductViewModel ConvertToViewModel(Product product) {
         ProductViewModel model = new ProductViewModel();
         model.ProductID = product.ProductID;
         model.Name = product.Name;
         //implement other required properties
         model.ShortDescription = product.ShortDescription;
         model.LongDescription = product.LongDescription;
         model.ImageFile = product.ImageFile;
         model.UnitPrice = product.UnitPrice;
         return model;
      }//close ConvertToViewModel(...)


      //Implement the method GetProductList
      public List<ProductViewModel> GetProductsList() {
         if (this.products == null)
            //Call the method GetAllProducts
            this.products = GetAllProducts();
         //Return the products
         return this.products;
      }//close GetProductsList()


      public List<ProductViewModel> GetAllProducts() {
         List<ProductViewModel> productmodels = new List<ProductViewModel>();
         // Call the GetAllProductsFromDataStore()
         List<Product> products = GetAllProductsFromDataStore();
         foreach (Product p in products) {
            //Call the method ConvertToViewModel to convert p and pass the method ConvertToViewModel 
            //to the method add of the productmodels
            productmodels.Add(ConvertToViewModel(p));
         }//end foreach
         return productmodels;
      }//close GetAllProducts()


      public Product GetProductByIdFromDataStore(string id) {
         using (HalloweenEntities1 data = new HalloweenEntities1()) {
            //Get a product from Products of data where ProductID is matched with id parameter
            return data.Products.Where(p => p.ProductID == id).FirstOrDefault();
            //return data.Products.FirstOrDefault();
         }//end using
      }//close GetProductByIdFromDataStore(...)


      public OrderViewModel GetOrderInfo(string id) {
         OrderViewModel order = new OrderViewModel();
         //Call the method GetSelectedProduct and assign the return value to SelectedProduct property
         order.SelectedProduct = GetSelectedProduct(id);
         return order;
      }//close GetOrderInfo(...)

      ///////////////////////////////////////////////////////////////////////////////////////////////////////////
      public ProductViewModel GetSelectedProduct(string id) {
         if (this.products == null)
            //call the method ConvertToViewModel and pass the method GetProductByIdFromDataStore(id)
            return ConvertToViewModel(GetProductByIdFromDataStore(id));
         else
            //Get the product from the products where ProductID is matched with id (Using 
            //Lambda expression)
            return products.Where(p => p.ProductID == id).FirstOrDefault();
      }//close GetSelectedProduct(...)    

   }//close class OrderModel
}//close namespace Ch24ShoppingCartMVC.Models