using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyage.WebStatic.Metier.Models
{
    public class Voyage
    {
        public int Id;
        public string Nom;
        public string Image;
        public int NbPlacesDispo=0;
        internal int Continent;
        internal int Pays;
        internal int Region;
    }
}