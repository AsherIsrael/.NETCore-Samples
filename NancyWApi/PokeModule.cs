using Nancy;
using ApiCaller;
using System;
using System.Collections.Generic;

namespace NancyWApi
{
    public class PokeModule : NancyModule
    {
        public PokeModule()
        {
            Get("/pokemon/{PokeId}", async args => 
            {
                string response = "";
                Console.WriteLine("main route");
                string url = "http://pokeapi.co/api/v2/pokemon/";
                try
                {
                    url = url + (int)args.PokeId;
                }
                catch
                {
                    url = url + "1";
                }
                await WebRequest.SendRequest(url, new Action<Dictionary<string, object>>( JsonResponse =>
                   { 
                       response = (string)JsonResponse["name"];
                       ViewBag.Pokemon = JsonResponse;
                   }
                ));
                Console.WriteLine(response);
                return View["Show"];
            });
        }
    }
}