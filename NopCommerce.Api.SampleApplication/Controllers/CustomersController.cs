﻿using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using NopCommerce.Api.AdapterLibrary;
using NopCommerce.Api.SampleApplication.DTOs;

namespace NopCommerce.Api.SampleApplication.Controllers
{
    public class CustomersController : Controller
    {
        public ActionResult GetCustomers()
        {
            // TODO: Here you should get the data from your database instead of the current Session.
            // Note: This should not be done in the action! This is only for illustration purposes.
            var accessToken = (Session["accessToken"] ?? TempData["accessToken"]).ToString();
            var serverUrl = (Session["serverUrl"] ?? TempData["serverUrl"]).ToString();

            var nopApiClient = new ApiClient(accessToken, serverUrl);

            string jsonUrl = $"/api/customers?fields=id,first_name,last_name";
            object customersData = nopApiClient.Get(jsonUrl);

            var customersRootObject = JsonConvert.DeserializeObject<CustomersRootObject>(customersData.ToString());

            var customers = customersRootObject.Customers.Where(
                customer => !string.IsNullOrEmpty(customer.FirstName) || !string.IsNullOrEmpty(customer.LastName));

            return View("~/Views/Customers.cshtml", customers);
        }

        public ActionResult UpdateCustomer(int customerId)
        {
            var accessToken = (Session["accessToken"] ?? TempData["accessToken"]).ToString();
            var serverUrl = (Session["serverUrl"] ?? TempData["serverUrl"]).ToString();

            var nopApiClient = new ApiClient(accessToken, serverUrl);

            string jsonUrl = $"/api/customers/{customerId}";

            // we use anonymous type as we want to update only the last_name of the customer
            // also the customer shoud be the cutomer property of a holder object as explained in the documentation
            // https://github.com/SevenSpikes/api-plugin-for-nopcommerce/blob/nopcommerce-3.80/Customers.md#update-details-for-a-customer
            // i.e: { customer : { last_name: "" } }
            var customerToUpdate = new { customer = new { last_name = "izmenic", first_name = "izmenic" } };
            string customerJson = JsonConvert.SerializeObject(customerToUpdate);

            nopApiClient.Put(jsonUrl, customerJson);

            return RedirectToAction("GetCustomers");
        }

        public ActionResult CreateCustomer()
        {
            var accessToken = (Session["accessToken"] ?? TempData["accessToken"]).ToString();
            var serverUrl = (Session["serverUrl"] ?? TempData["serverUrl"]).ToString();

            var nopApiClient = new ApiClient(accessToken, serverUrl);

            string jsonUrl = $"/api/customers";

            // we use anonymous type as we want to update only the last_name of the customer
            // also the customer shoud be the cutomer property of a holder object as explained in the documentation
            // https://github.com/SevenSpikes/api-plugin-for-nopcommerce/blob/nopcommerce-3.80/Customers.md#update-details-for-a-customer
            // i.e: { customer : { last_name: "" } }
            //var customerToCreate = new { customer = new { first_name = "adic", last_name = "adda" } };
            IDictionary<int, string> dict = new Dictionary<int, string>();

            dict.Add(new KeyValuePair<int, string>(1, "3"));

            //var customerToCreate = new { customer = new { first_name = "postman-name", last_name = "postman-surname", email = "postman@gmail.com", role_ids = dict } };

            //string customerJson = JsonConvert.SerializeObject(customerToCreate);

            string customerJson = "{ \"customer\" : { \"first_name\" : \"new-name\", \"last_name\" : \"new-surname\", \"email\" : \"newuser@gmail.com\", \"role_ids\" : [3] }}";

            nopApiClient.Post(jsonUrl, customerJson);

            return RedirectToAction("GetCustomers");
        }

        public ActionResult DeleteCustomer(int customerId)
        {
            var accessToken = (Session["accessToken"] ?? TempData["accessToken"]).ToString();
            var serverUrl = (Session["serverUrl"] ?? TempData["serverUrl"]).ToString();

            var nopApiClient = new ApiClient(accessToken, serverUrl);

            string jsonUrl = $"/api/customers/{customerId}";

            nopApiClient.Delete(jsonUrl);

            return RedirectToAction("GetCustomers");
        }

        public ActionResult GetOrders(int customerId)
        {
            var accessToken = (Session["accessToken"] ?? TempData["accessToken"]).ToString();
            var serverUrl = (Session["serverUrl"] ?? TempData["serverUrl"]).ToString();

            var nopApiClient = new ApiClient(accessToken, serverUrl);

            string jsonUrl = $"/api/orders/customer/{customerId}";

            object customersData = nopApiClient.Get(jsonUrl);

            var customersRootObject = JsonConvert.DeserializeObject<OrderRootObject>(customersData.ToString()).Orders.AsEnumerable();

            return View("~/Views/CustomerOrders.cshtml", customersRootObject);
        }
    }
}