using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XmlHttpRequest
{
    public partial class page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // POST
            if (Request.HttpMethod == "POST")
            {
                var cat = Request.Form["cat"];
                var souscat = Request.Form["souscategorie"];
                switch (cat)
                {
                    case "ville":
                        Response.Write("<ul><li>Paris</li><li>Lyon</li></ul>");
                        break;
                    case "pays":
                        Response.Write("<ul><li>France</li><li>Portugal</li></ul>"); break;
                    default:
                        Response.Write("<ul><li>X</li><li>Y</li></ul>"); break;
                }
                Response.Write("<h6>" + souscat + "</h6>");
            }
            // GET
            else
            {
                string cat = Request.QueryString["categorie"]; // Querystring in this case is http://localhost:62604/page.aspx 
                // which is given in the GET request on the client side
                string souscat = Request.QueryString["souscategorie"];
                switch (cat)
                {
                    case "auto":
                        Response.Write("<ul><li>Porsche</li><li>Jaguar</li></ul>"); break;
                    case "animal":
                        Response.Write("<ul><li>Poule</li><li>Cheval</li></ul>"); break;
                    default:
                        Response.Write("<ul><li>A</li><li>B</li></ul>"); break;
                }
                Response.Write("<h6>" + souscat + "</h6>");
            }
        }
    }
}