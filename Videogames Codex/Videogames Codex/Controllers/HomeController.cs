using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Videogames_Codex;

namespace Videogames_Codex.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Models.Videogames> videogames = Models.Videogames.SelectAll(); //llamamos al metodo listar
            return View(videogames);//lo enseñamos en la vista
        }

        public ActionResult Show(int id)
        {
            Models.Videogames videogames = Models.Videogames.GetVideoGame(id);//devolvemos el videojuego con esa id dada

            if(videogames != null)//si el videojuego no es nulo...
            {
                return View(videogames);//lo enseñamos por la vista
            } else//sino...
            {
                return View("~/Views/Home/Index");//redireccionamos a la pagina de inicio
            }

        }

        public ActionResult Create()
        {
            Models.Videogames videogames = new Models.Videogames();
            return View("~/Views/Home/VideogamesForm.cshtml", videogames);
        }

        public ActionResult Edit(int id =0)
        {
            Models.Videogames videogames = Models.Videogames.GetVideoGame(id);
            if (videogames == null)
            {
                return View("~/Views/Home/Create");
            } else
            {
                return View("~/Views/Home/VideogamesForm.cshtml",videogames);
            }

        }

        public ActionResult Save(Models.Videogames videogames)
        {
            videogames.Save();
            return Redirect("~/Home/Show/"+videogames.id);
        }

        public ActionResult Remove(int id = 0)
        {
            Models.Videogames videogames = Models.Videogames.GetVideoGame(id);
            if (videogames != null)
            {
                videogames.Remove();
            }
            
                return Redirect("~/Home/Index");
            
        }

        public ActionResult Ranking()
        {
            List<Models.Videogames> videogames = Models.Videogames.ListRanking();
            return View("~/Views/Home/Ranking.cshtml", videogames);
        }

        public ActionResult SearchGame(string name)
        {
            Videogames_Codex.Models.Videogames videogames = Models.Videogames.GetVideogameName(name);
            return View("~/Views/Home/Show.cshtml", videogames);
            }
        }
    }
