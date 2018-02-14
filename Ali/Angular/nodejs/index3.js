var http = require('http');
var date1 = require('./moduleDate'); // moduleDate est le nom du fichier
var url = require('url');

var serveurWeb = http.createServer(function (request, response) {
    response.writeHead(200, { 'Content-Type':'text/html;charset=utf-8'});

    //maDate est une méthode de l'objet date1 qui est une instance du module/class moduleDate
    response.write('url = ' + request.url);

    var dataUrl = url.parse(request.url, true).query; // If I give names and values for variables 
    //in the url (ex http://localhost:8181/?animal=zebra&mineral=quartz&vegetal=pervenche), by using this I can do the following
    response.write('<br/>Animal = ' + dataUrl.animal);
    response.write('<br/>Minéral = ' + dataUrl.mineral);
    response.write('<br/>Végétal = ' + dataUrl.vegetal);

    response.end('<br/>Salut tout le monde ! Il est ' + date1.maDate());

});
serveurWeb.listen(8181);