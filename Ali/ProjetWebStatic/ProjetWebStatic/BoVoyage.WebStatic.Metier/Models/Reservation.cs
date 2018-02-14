using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyage.WebStatic.Metier.Models
{
    public class Reservation
    {
        public int IdVoyage;
        public Paiement InfosPaiements;
        public Personne[] Personnes;
        public EtatEnum Etat;
    }
    public enum EtatEnum { Ok, TimeOut, PlusDePlace}
}