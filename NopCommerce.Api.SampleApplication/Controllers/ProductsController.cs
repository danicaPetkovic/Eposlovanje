using Newtonsoft.Json;
using NopCommerce.Api.AdapterLibrary;
using NopCommerce.Api.SampleApplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NopCommerce.Api.SampleApplication.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult GetProducts()
        {
            var accessToken = (Session["accessToken"] ?? TempData["accessToken"]).ToString();
            var serverUrl = (Session["serverUrl"] ?? TempData["serverUrl"]).ToString();

            var nopApiClient = new ApiClient(accessToken, serverUrl);

            string jsonUrl = $"/api/products?fields=id,name,images";
            object productsData = nopApiClient.Get(jsonUrl);

            var productsRootObject = JsonConvert.DeserializeObject<ProductRootObject>(productsData.ToString()).Products.AsEnumerable();

            return View("~/Views/Products/Products.cshtml", productsRootObject);
        }

        public ActionResult UpdateProduct(int productId)
        {
            var accessToken = (Session["accessToken"] ?? TempData["accessToken"]).ToString();
            var serverUrl = (Session["serverUrl"] ?? TempData["serverUrl"]).ToString();

            var nopApiClient = new ApiClient(accessToken, serverUrl);

            string jsonUrl = $"/api/products/{productId}";

            var productToUpdate = new { product = new { name = "izmenic" } };
            string productJson = JsonConvert.SerializeObject(productToUpdate);

            nopApiClient.Put(jsonUrl, productJson);

            return RedirectToAction("GetProducts");
        }

        public ActionResult CreateProduct()
        {
            var accessToken = (Session["accessToken"] ?? TempData["accessToken"]).ToString();
            var serverUrl = (Session["serverUrl"] ?? TempData["serverUrl"]).ToString();

            var nopApiClient = new ApiClient(accessToken, serverUrl);

            string jsonUrl = $"/api/products";

            string productJson = "{ \"product\": {\"visible_individually\": true,\"name\": \"Dodat dodato\",\"images\": [ {\"id\": 1,\"position\": 1, \"src\": \"http://localhost:1030/content/images/thumbs/0000020_build-your-own-computer.jpeg\", \"attachment\": null} ] } }"; 
      
            nopApiClient.Post(jsonUrl, productJson);

            return RedirectToAction("GetProducts");
        }

        public ActionResult DeleteProduct(int productId)
        {
            var accessToken = (Session["accessToken"] ?? TempData["accessToken"]).ToString();
            var serverUrl = (Session["serverUrl"] ?? TempData["serverUrl"]).ToString();

            var nopApiClient = new ApiClient(accessToken, serverUrl);

            string jsonUrl = $"/api/products/{productId}";

            nopApiClient.Delete(jsonUrl);

            return RedirectToAction("GetProducts");
        }
    }
}