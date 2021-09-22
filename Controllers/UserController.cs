using CallingUserApiInMVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace CallingUserApiInMVCApp.Controllers
{
    public class UserController : Controller
    {
        string basUrl = "https://localhost:44302/";
        public async Task<ActionResult> Index()
        {
            List<UserModel> user = new List<UserModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(basUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/v1/User/GetAllUser");

                if (Res.IsSuccessStatusCode)
                {
                    var userResponse = Res.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<List<UserModel>>(userResponse);
                }
            }
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult Create(UserModel user )
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44302/api/v1/User/CreateUser");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                /*HttpResponseMessage Res = await client.PostAsync(api/v1/User/CreateUser);*/

                var posttalk =  client.PostAsJsonAsync<UserModel>("CreateUser", user);

                var result = posttalk.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Server Error, Please Contact Admin");

            return View(user);
        }

/*        public async Task<ActionResult> Edit(int id)
        {
            List<UserModel> user = new List<UserModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44302");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("/api/v1/User/UpdateUser");


                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<List<UserModel>>(readTask);
                }
            }

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id , UserModel user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44302/api/v1/User/CreateUser");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                *//*HttpResponseMessage Res = await client.PostAsync(api/v1/User/CreateUser);*//*

                var posttalk = client.PostAsJsonAsync<UserModel>("CreateUser", user);

                var result = posttalk.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }*/
/*
            ModelState.AddModelError(string.Empty, "Server Error, Please Contact Admin");

            return View(user);
        }*/

        [HttpDelete]
        public ActionResult Delete([FromRoute]int id)
        {
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44302/api/v1/User");
                httpclient.DefaultRequestHeaders.Clear();
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var deleteTask = httpclient.DeleteAsync($"deleteUser/{id}");
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
        }
    }
}
