using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel
{
    public interface IDataContext
    {
        // Afficher la liste des clients
        IList<string> GetClients();

        // Afficher la liste des factures
        IList<string> GetFactures();

    }
}
