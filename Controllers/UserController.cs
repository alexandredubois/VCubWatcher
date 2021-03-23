using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace VCubWatcher.Controllers
{
    public class UserController : Controller
    {
        private const string _cookieKey = "stations";

        public IActionResult AddFavorite(int stationId)
        {
            List<int> favorites;

            //1) Récupérer la valeur actuelle du cookie qui nous intéresse
            if (Request.Cookies.ContainsKey(_cookieKey))
            {
                favorites = JsonConvert.DeserializeObject<List<int>>(Request.Cookies[_cookieKey]);
            }
            else
            {
                favorites = new List<int>();
            }

            //Si la station n'existe pas dans la liste, je l'ajoute
            if (!favorites.Contains(stationId))
            {
                favorites.Add(stationId);
            }

            //Je supprime le cookie avec sa valeur actuelle
            Response.Cookies.Delete(_cookieKey);

            //Je crée un cookie de même nom avec la nouvelle valeur
            Response.Cookies.Append(_cookieKey, JsonConvert.SerializeObject(favorites));

            //On redirige l'utilisateur vers une action d'un autre contrôleur
            return RedirectToAction("Favoris", "Station");
        }

        public IActionResult RemoveFavorite(int stationId)
        {
            List<int> favorites;
            //1) Récupérer la valeur actuelle du cookie qui nous intéresse
            if (Request.Cookies.ContainsKey(_cookieKey))
            {
                favorites = JsonConvert.DeserializeObject<List<int>>(Request.Cookies[_cookieKey]);
                if (favorites.Contains(stationId))
                {
                    favorites.Remove(stationId);
                    Response.Cookies.Append(_cookieKey, JsonConvert.SerializeObject(favorites));
                }
                Response.Cookies.Delete(_cookieKey);
                Response.Cookies.Append(_cookieKey, JsonConvert.SerializeObject(favorites));
            }
            return RedirectToAction("Favoris", "Station");
        }
    }
}
