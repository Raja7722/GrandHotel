// Gestion d'un évènement du framework nodejs
var fs = require('fs');
var rs = fs.createReadStream('./demo.txt');
rs.on('open', function (){ // open is a key word for the event rs.
    console.log('Le fichier demo.txt a été ouvert');
});


// Gestion d'un évènement personnalisé qu'on a crée nous même
var events = require('events'); // Module
var eventEmmetteur = new events.EventEmitter(); // Objet de gestion des évènements

// Gestion de l'évènement
var coucouEventHandler = function () {
    console.log('Quelq\'un a fait coucou !!!! ');
};

// Association ebtre évènement et le handler
eventEmmetteur.on('coucou', coucouEventHandler); // Here coucou is not a key word. It is just the name we give to the event we created

// L'évènement se produit
eventEmmetteur.emit('coucou');