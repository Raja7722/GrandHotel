using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyage.WebStatic.Metier.Models
{
    public class Personne
    {
        public int Id;
        public bool EstClient;
        public string Nom;
        public string Prenom;
        public DateTime DateNaissance;
    }
}