using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using VCubWatcher.Models;

namespace VCubWatcher.Controllers
{
    public class StationController : Controller
    {
        public IActionResult Liste()
        {
            var stations = GetBikeStationsFromApi();
            return View(stations);
        }

        public IActionResult Carte()
        {
            var stations = GetBikeStationsFromApi();
            return View(stations);
        }

        private static List<BikeStation> GetBikeStationsFromApi()
        {
            //Création un HttpClient (= outil qui va permettre d'interroger une URl via une requête HTTP)
            using (var client = new HttpClient())
            {
                //Interrogation de l'URL censée me retourner les données
                var response = client.GetAsync("http://api.alexandredubois.com/vcub-backend/vcub.php");
                //Récupération du corps de la réponse HTTP sous forme de chaîne de caractères
                var stringResult = response.Result.Content.ReadAsStringAsync();
                //Conversion de mon flux JSON (string) en une collection d'objets BikeStation
                //d'un flux de données vers des objets => Déserialisation
                //d'objets vers un flux de données => Sérialisation
                var result = JsonConvert.DeserializeObject<List<BikeStation>>(stringResult.Result);
                return result;
            }
        }
    }
}
