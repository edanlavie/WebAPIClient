// See https://aka.ms/new-console-template for more information
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient // Note: actual namespace depends on the project name.
{
    public class Books
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }

    internal class Program
    {
       

        public class InitializeClient
        {
            static readonly HttpClient _httpClient = new HttpClient();

        }


        static async Task Main(string[] args)
        {
            await ProcessRepositories();
            
        }



        public static async Task ProcessRepositories()
        {
            var client = new HttpClient();

            while (true)
            {
                try
                {
                    Console.WriteLine("Search Project Gutenberg:");

                    var searchResult = Console.ReadLine();

                    if (string.IsNullOrEmpty(searchResult))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://gutendex.com/books?search=" + searchResult + "%20");
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var webResult = JsonConvert.DeserializeObject<Books>(resultRead);

                    Console.WriteLine("_____________");
                    Console.WriteLine(resultRead);
                    // line doesn't work with site
                    //Console.WriteLine("Books: " + webResult.Title);
                    Console.WriteLine("_____________");
                    Console.WriteLine("These are all your search results. Apologies, for our formatting");
                    Console.WriteLine("_____________");
                }
                catch (Exception)
                {
                    Console.WriteLine("No valid results. Please try again");
                }
            }   
        }
    }
}