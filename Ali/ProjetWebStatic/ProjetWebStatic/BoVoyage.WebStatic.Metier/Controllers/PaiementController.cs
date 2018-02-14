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
    public class PaiementController : ApiController
    {
        // GET: api/Paiement
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Paiement/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Paiement
        public Paiement Post([FromBody]string noCB)
        {
            return new Paiement{ noCB = noCB, Check = true };
        }

        // PUT: api/Paiement/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Paiement/5
        public void Delete(int id)
        {
        }
    }
}
