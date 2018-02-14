var http = require('http');
var date1 = require('./moduleDate'); // moduleDate est le nom du fichier
var serveurWeb = http.createServer(function (request, response) {
    response.writeHead(200, { 'Content-Type': 'text/html' });
    response.end('Salut tout le monde ! Il est ' + date1.maDate());
    // maDate est une méthode de l'objet date1 qui est une instance du module/class moduleDate
});
serveurWeb.listen(8181);