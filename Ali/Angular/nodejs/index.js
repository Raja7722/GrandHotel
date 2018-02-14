var http = require('http');
var serveurWeb = http.createServer(function (request, response) {
    response.writeHead(200, { 'Content-Type': 'text/html' }); // 200 means "all ok"
    response.end('Salut tout le monde ! Il est ');
}); // All this creates an object named serveurWeb
serveurWeb.listen(8181);