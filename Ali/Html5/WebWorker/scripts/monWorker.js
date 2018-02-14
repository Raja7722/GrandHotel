var duree=0, pas=0, enPause = false, compteur = 0;
onmessage = function (eventData) { // eventData is just a name of the parameter
    var command = eventData.data.command;
    switch (command) {
        case 'init':
            duree = eventData.data.valeurs.duree;
            pas = eventData.data.valeurs.pas;
            break;
        case 'pause':
            enPause = !enPause;
            compte();
            break;
    }
}

function compte() {
    compteur += pas;
    if (!enPause) setTimeout('compte()', duree); // Here I tell him to reexecute the function compte after a 
    //certain duration if the counter is not on pause.This way it will keep counting.
    postMessage(compteur);
}
compte();