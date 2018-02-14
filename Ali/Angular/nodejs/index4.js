var http = require('http');
var fs = require('fs'); // Read - Create - Update - Delete - Rename

var serveurWeb = http.createServer(function (request, response) {
    var urlDemande = './' + request.url.split('/')[1];

    if (urlDemande == './') urlDemande = "./index.html"; // If in my url I have further information (other than the address of the server 
    //then execute the function)
    fs.readFile(urlDemande, function (err, data) { // The words err and data are not key words of javascript but in the syntax 
        //of the readFile the second parameter is a function of which the 1st parametrer is always the error and the second is the content of the file.
        // Read the file given in the url (ex index4.html) and execute the function
            if (err) {
                response.writeHead(404, { 'Content-Type': 'text/html' });
                return response.end('<html><body><h2>404 Ressource introuvable !</h2></body></html>');
            }
            response.writeHead(200, { 'Content-Type': 'text/html' });
            response.write(urlDemande);
            response.write(data);
            response.write('Version 1');
            return response.end();
        });
});
serveurWeb.listen(8181);