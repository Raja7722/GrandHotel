var http = require('http');
var uc = require('upper-case'); // This module, contrary to the previous one is not already installed 
//so we need to install it with the command "npm install upper-case" in the terminal.

var serveurWeb = http.createServer(function (request, response) {
    response.writeHead(200, { 'Content-Type': 'text/html' });
    response.write(uc("Bonjour la compagnie !"));
    response.end();
});
serveurWeb.listen(8181);