function go() {
    var p1 = new Object();
    p1.nom = "Philippe";
    p1.prenom = "Gérard";
    p1.NomComplet = function () { return this.nom + " " + this.prenom };

    var p2 = {};
    p2.nom = "Jugnot";
    p2.prenom = "Gérard";
    p2.NomComplet = function () { return this.nom + " " + this.prenom };


    // JSon
    var p3 = {
        nom: "Depardieu",
        prenom: "Gérard",
        NomComplet: function () { return this.nom + " " + this.prenom }
    };

    var ps = [p1, p2, p3];
    document.getElementById("texte1").innerHTML =
        "<ul>" +
        "<li>" + ps[0].NomComplet() + "</li>" +
        "<li>" + ps[1].NomComplet() + "</li>" +
        "<li>" + ps[2].NomComplet() + "</li>" +
        "</ul>";
}