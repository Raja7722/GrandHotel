using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel
{
    public interface IDataContext
    {
        IList<string> GetClients();

        IList<string> GetFactures();
    }
}
