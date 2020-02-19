using BlazorApp6.Models;
using Hanssens.Net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BlazorApp6.Data
{
    public class LoginService
    {
       public  Tuple<Token,Usuario,bool> login(Usuario model)
        {
            HttpClient client = new HttpClient();
            
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
            HttpResponseMessage respToken = client.PostAsync("https://localhost:44381/api/account/login ", new StringContent(
                JsonConvert.SerializeObject(new
                {
                    username =" "+model.Username + " "  ,
                    password = " " + model.Password + " "

                }),Encoding.UTF8,"application/json")).Result;

            string conteudo =   respToken.Content.ReadAsStringAsync().Result;

            if (respToken.IsSuccessStatusCode)
            {
                Usuario usuario = new Usuario();
                Token token = new Token();
                var tupla = JsonConvert.DeserializeObject<Tuple<Token,Usuario>>(conteudo);

                token = tupla.Item1;
                usuario = tupla.Item2;
                

                return new Tuple<Token,Usuario,bool>(token,usuario, true);
            }
            return new Tuple<Token, Usuario, bool>(null,null, false);
            
        }
    }
}
