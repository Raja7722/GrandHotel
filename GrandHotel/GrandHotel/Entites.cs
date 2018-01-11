using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel
{
    class Entites
    {
        public class Client
        {
            public int IdClient; // P
            public string Civilite;
            public string Nom;
            public string Prenom;
            public Boolean CarteFidelite;
            public string Societe;
        }

        public class Telephone
        {
            public string Numero; // P j'ai faim!!!!
            public int IdClient; // F
            public string CodeType;
            public Boolean Pro;
        }

        public class Reservation
        {
            public Int16 NumChambre; // PF
            public DateTime Jour; // PF
            public int IdClient; //F
            public byte NbPersonnes;
            public byte HeureArrivee;
            public Boolean Travail;
        }

        public class Adresse
        {
            public int IdClient; // PF
            public string Rue;
            public string complement;
            public string CodePostal;
            public string Ville;
        }

        public class Email
        {
            public string Adresse; // P
            public int IdClient; // F
            public Boolean Pro;
        }

        public class Calendrier
        {
            public DateTime Jour; // P
        }

        public class ModePaiement
        {
            public string Code; // P
            public string Libelle;
        }

        public class Facture
        {
            public int IdFacture; // P
            public int IdClient; // F
            public DateTime DateFacture;
            public DateTime DatePaiement;
            public string CodeModePaiement;
        }

        public class LigneFacture
        {
            public int IdFacture; // PF
            public int NumLigne; // P
            public Int16 Quantite;
            public decimal MontantHT;
            public decimal TauxTVA;
            public decimal TauxReduction;
        }

        public class Chambre
        {
            public Int16 Numero; // P
            public Byte Etage;
            public Boolean Bain;
            public Boolean Douche;
            public Boolean WC;
            public Byte NbLits;
            public Int16 NumTel;
        }

        public class TarifChambre
        {
            public Int16 NumChambre; // PF
            public string CodeTarif; // PF       
        }

        public class Tarif
        {
            public string Code; // P
            public DateTime DateDebut;
            public decimal Prix;
        }
    }
}
