using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Assignment1.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {

        string url = "https://www.googleapis.com/books/v1/volumes?q=harry+potter";


        public async Task<IActionResult> Index()
        {

            HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                var data = responseMessage.Content.ReadAsStringAsync().Result;
                Books books = Newtonsoft.Json.JsonConvert.DeserializeObject<Books>(data);
                System.Diagnostics.Debug.WriteLine(books.items);
                
                return View(books);
            }
            return View();
        }

        public async Task<IActionResult> Details(string id )
        {
            Books books;

            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                var data = responseMessage.Content.ReadAsStringAsync().Result;
                books = Newtonsoft.Json.JsonConvert.DeserializeObject<Books>(data);

                foreach (Book book in books.items)
                {
                    if (String.Equals(book.id, id))
                    {
                        return View(book);
                    }
                }
            }



           return View();
        }
    }
}