using BlazorApp6.Models;
using Hanssens.Net;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace BlazorApp6.Data
{
    public class UserService
    {
        

       
        public async Task<List<Usuario>> GetUsuarios()
        {

          

            HttpClient client = new HttpClient();

            

            List<Usuario> usuario = new List<Usuario>();
            Token t = new Token();
            var storage = new LocalStorage();
            t.AcessToken = storage.Get<string>("Token");
            storage.Persist();

            client.DefaultRequestHeaders.Accept.Add(

           
                 new MediaTypeWithQualityHeaderValue("application/json")
                    );;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", t.AcessToken
               );

        HttpResponseMessage respToken = client.GetAsync("https://localhost:44381/api/usuario ").Result;

        string conteudo = respToken.Content.ReadAsStringAsync().Result;
            if (respToken.IsSuccessStatusCode)
            {
               
                Usuario[] u = JsonConvert.DeserializeObject<Usuario[]>(conteudo);
                 return u.ToList();
            }
            else if (respToken.IsSuccessStatusCode != true)
            {
                return null;

            }
            return null;

        }
    }
}
