var http = require('http');
var formidable = require('formidable'); // As with "upper-case" we need to download it
var fs = require('fs');

http.createServer(function (request, response) {
    if (request.url == '/fileupload') {
        // reception du formulaire
        var form = new formidable.IncomingForm();
        form.parse(request, function (err, fields, files) {
            dossierNode = files.filetoupload.path; // The file to copy
            dossierCible = "S:/Theodoros/Ali/Angular/" + files.filetoupload.name; // The path to which we copy the file
            fs.copyFile(dossierNode, dossierCible, function (err) {
                if (err) throw err;
                response.write('Téléchargement effectué');
                response.end();
            });
        });
    }
    else {
        // demande initiale
        response.writeHead(200, { 'Content-Type': 'text/html' });
        response.write('<form action="fileupload" method="post" enctype="multipart/form-data">');
        response.write('<input type="file" name="filetoupload"/><br/>');
        response.write('<input type="submit" value="Ok"/>');
        response.write('</form>');
        return response.end();
    }
}).listen(8181);
