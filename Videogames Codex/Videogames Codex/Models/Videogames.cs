namespace Videogames_Codex.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Videogames")]
    public partial class Videogames
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string publisher { get; set; }

        public int year { get; set; }

      int genre { get; set; }

        public Genre Genre
        {
            get { return (Genre)genre; }
            set { genre = (int)value; }
        }

        int platform { get; set; }

        public Platform Platform
        {
            get { return (Platform)platform; }
            set { platform = (int)value; }
        }

        public int score { get; set; }

        public bool online { get; set; }

         int pegi { get; set; }

        public PEGI Pegi
        {
            get { return (PEGI)pegi; }
            set { pegi = (int)value; }
        }

        public static List<Videogames> SelectAll()//metodo para listar la tabla de videojuegos
        {
            List<Videogames> videogames = new List<Videogames>();

            try
            {
                VideogamesContext context = new VideogamesContext();
                videogames = context.Videogames.OrderBy(x => x.name).ToList();//Ordenamos por el nombre del juego y listamos la tabla
            }
            catch (Exception e)
            {

                throw;
            }
            return videogames;
        }

        public static Videogames GetVideoGame(int id)
        {
            Videogames videogames = null;

            try
            {
                VideogamesContext context = new VideogamesContext();

                videogames = context.Videogames.Where(x => x.id == id).SingleOrDefault();//devolvemos el videojuego que tenga la id dada
            }
            catch (Exception e)
            {

                throw;
            }
            return videogames;
        }

        public void Save()
        {
            bool crear = this.id == 0;//sera verdadero cuando la id sea 0
            try
            {
                VideogamesContext context = new VideogamesContext(); //creamos el contexto
                if (crear)//si se cumple crear(Su id es 0, por lo tanto es nuevo)
                {
                    context.Entry(this).State = System.Data.Entity.EntityState.Added;//añadimos el producto
                }
                else//sino...
                {
                    context.Entry(this).State = System.Data.Entity.EntityState.Modified;//modificamos el producto
                }

                context.SaveChanges(); //lo guardamos
            }
            catch (Exception e)
            {

                throw;
            }
        }         

        public void Remove()
        {
            try
            {
                VideogamesContext context = new VideogamesContext();
                
                context.Entry(this).State = System.Data.Entity.EntityState.Deleted;//borramos el producto
                context.SaveChanges();//guardamos los cambios

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static List<Videogames> ListRanking()
        {
            List<Videogames> videogames = new List<Videogames>();

            try
            {
                VideogamesContext context = new VideogamesContext();
                videogames = context.Videogames.OrderByDescending(x => x.score).ToList();
            }
            catch (Exception e)
            {

                throw;
            }
            return videogames;
        }

        public static Videogames GetVideogameName(string name)
        {
            Models.Videogames videogames = null;
            Models.VideogamesContext context = new Models.VideogamesContext();

            if (!string.IsNullOrEmpty(name))
            {
                videogames = context.Videogames.Where(x => x.name.Contains(name)).SingleOrDefault();
            }

            return videogames;
        }
    }
}
