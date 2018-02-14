// Gestion d'un �v�nement du framework nodejs
var fs = require('fs');
var rs = fs.createReadStream('./demo.txt');
rs.on('open', function (){ // open is a key word for the event rs.
    console.log('Le fichier demo.txt a �t� ouvert');
});


// Gestion d'un �v�nement personnalis� qu'on a cr�e nous m�me
var events = require('events'); // Module
var eventEmmetteur = new events.EventEmitter(); // Objet de gestion des �v�nements

// Gestion de l'�v�nement
var coucouEventHandler = function () {
    console.log('Quelq\'un a fait coucou !!!! ');
};

// Association ebtre �v�nement et le handler
eventEmmetteur.on('coucou', coucouEventHandler); // Here coucou is not a key word. It is just the name we give to the event we created

// L'�v�nement se produit
eventEmmetteur.emit('coucou');