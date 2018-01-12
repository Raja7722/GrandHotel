using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel
{
    public class Client
    {
        public int IdClient { get; set; } // P
        public string Civilite { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public Boolean CarteFidelite { get; set; }
        public string Societe { get; set; }
    }

    public class ClientCoordonnees
    {
        public int IdClient { get; set; } // P
        public string Civilite { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Rue { get; set; }
        public string Complement { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Numero { get; set; }
        public string Adresse { get; set; } // P
    }

    public class Telephone
    {
        public string Numero { get; set; } //
        public int IdClient { get; set; } // F
        public string CodeType { get; set; }
        public bool Pro { get; set; }
    }

    public class Reservation
    {
        public Int16 NumChambre { get; set; } // PF
        public DateTime Jour { get; set; } // PF
        public int IdClient { get; set; } //F
        public byte NbPersonnes { get; set; }
        public byte HeureArrivee { get; set; }
        public Boolean Travail { get; set; }
    }

    public class Adresse
    {
        public int IdClient { get; set; } // PF
        public string Rue { get; set; }
        public string Complement { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
    }

    public class Email
    {
        public string Adresse { get; set; } // P
        public int IdClient { get; set; } // F
        public Boolean Pro { get; set; }
    }

    public class Calendrier
    {
        public DateTime Jour { get; set; } // P
    }

    public class ModePaiement
    {
        public string Code { get; set; } // P
        public string Libelle { get; set; }
    }

    public class Facture
    {
        public int IdFacture { get; set; } // P
        public int IdClient { get; set; } // F
        public DateTime DateFacture { get; set; }
        public DateTime DatePaiement { get; set; }
        public string CodeModePaiement { get; set; }
    }

    public class LigneFacture
    {
        public int IdFacture { get; set; } // PF
        public int NumLigne { get; set; } // P
        public Int16 Quantite { get; set; }
        public decimal MontantHT { get; set; }
        public decimal TauxTVA { get; set; }
        public decimal TauxReduction { get; set; }
    }

    public class Chambre
    {
        public Int16 Numero { get; set; } // P
        public Byte Etage { get; set; }
        public Boolean Bain { get; set; }
        public Boolean Douche { get; set; }
        public Boolean WC { get; set; }
        public Byte NbLits { get; set; }
        public Int16 NumTel { get; set; }
    }

    public class TarifChambre
    {
        public Int16 NumChambre { get; set; } // PF
        public string CodeTarif { get; set; } // PF       
    }

    public class Tarif
    {
        public string Code { get; set; } // P
        public DateTime DateDebut { get; set; }
        public decimal Prix { get; set; }
    }

}
