using BoVoyage.WebStatic.Metier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BoVoyage.WebStatic.Metier.Controllers
{
    [EnableCors(origins: "http://localhost:1111", headers: "*", methods: "*")]
    public class VoyageController : ApiController
    {
        private Voyage[] Voyages = new Voyage[] {
                new Voyage { Id = 1, Nom = "Bamako", Image = "bamako.png", NbPlacesDispo=3, Continent=1, Pays=11, Region=111 },
                new Voyage { Id = 2, Nom = "Singapour", Image = "Singapour.png", NbPlacesDispo=8, Continent=2, Region=21, Pays=21 }
            };
        // GET api/values

        [Route("api/voyage/{continent}/{pays}/{region}")]
        public IEnumerable<Voyage> Get(int continent, int region, int pays)
        {
            return Voyages.Where(v =>
            (v.Continent == continent || continent == 0) &&
            (v.Region == region || region == 0) &&
            (v.Pays == pays || pays == 0));
        }

        // GET api/values/5
        public Voyage Get(int id)
        {
            return Voyages.Where(v => v.Id == id).FirstOrDefault();
        }

        // POST api/values
        public Reservation Post([FromBody]Reservation reservation)
        {
            if (reservation.Personnes.Length < 2) return null;
            foreach(Personne p in reservation.Personnes)
            {
                if (string.IsNullOrEmpty(p.Nom)) return null;
                if (string.IsNullOrEmpty(p.Prenom)) return null;
                if (p.DateNaissance == null || p.DateNaissance >= DateTime.Now) return null;
            }
            if (!reservation.InfosPaiements.Check) return null;
            if (!reservation.Personnes[0].EstClient) return null;
            reservation.Etat = EtatEnum.Ok;
            return reservation;
        }

    }
}
