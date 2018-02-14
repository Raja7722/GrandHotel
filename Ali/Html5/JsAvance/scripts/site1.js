// Classe en javascript avec un constructor

function Personne(nom, prenom) { // In javascript classes are defined as functions. It also serves as constructor so it needs parameters.
    // I could write var Personne = function(nom, prenom) { ... } // It is exactly the same
    this.nom = nom;
    this.prenom = prenom;
    this.taille = 0; // I don't need to define the type of parameters in javascript
    this.NomComplet = function () { return this.nom + " " + this.prenom };
}

function go() {
    var p1 = new Personne("Philippe", "Gérard");
    p1.taille = 195;
    var p2 = new Personne("Jugnot", "Gérard");
    var p3 = new Personne("Hernandez", "Gérard");

    // prototype (extension de la classe)
    Personne.prototype = { // I think that it "prototype" allows us to add properties to an existing class/function
        adresse: "",
        toString: function () { return this.nom + " " + this.prenom }
    };

    var p4 = new Personne("Oury", "Gérard");

    // Encapsulation
    var Maison = function (id, nom) {
        this.id = id;
        this.nom = nom;
        var code; // var means local aka it is only known in this function (similar to private in c#)
        this.getCode = function () { return code; }
        this.setCode = function (val) { code = val; }
    }
    var m1 = new Maison();
    m1.setCode("12345");

    // Heritage
    var Etudiant = function (id, nom, prenom, cursus) {
        this.id = id;
        this.nom = nom;
        this.prenom = prenom;
        this.cursus = cursus;
    }
    Etudiant.prototype = new Personne(); 
    Etudiant.prototype.constructor = Etudiant; // These two line (this and the previous one) are necessary to declare heritage

    var jim = new Etudiant(1, "Carrey", "Jim", "Poe.Net");

    // Méthode d'extension
    var Point = function (x, y) { this.x = x; this.y = y; }
    Point.prototype.MoveBy = function (deltaX, deltaY) { this.x += deltaX; this.y += deltaY; }
    // Instead of making an extension of the class we can just add a method
    var point1 = new Point(100, 200);
    point1.MoveBy(10, 20);

    function SetColor(color, transparence) {
        this.color = color;
        this.transparence = transparence;
    }

    point1.SetColor("red");
    SetColor.apply(point1, ["red", 0.5]); // aka I can add the method SetColor to the  object of the class Point which normally 
    // does not have that method with the command "apply".I have to give the parameters of this method in the form of a table.
    // If there were no parameters I should declare it like this SetColor.apply(point1, [])


    // Affichage
    document.getElementById("texte1").innerHTML =
        "<ul><strong>Personne</strong>" +
        "<li>" + p1.NomComplet() + "</li>" +
        "<li>" + p2.NomComplet() + "</li>" +
        "<li>" + p3.NomComplet() + "</li>" +
        "<li>" + p4.toString() + "</li>" + // Personne.prototype is defined after the definition of p1, p2 and p3 so I can only use the method toString for p4
        "</ul>" +
        "<ul><strong>Maison</strong><li>" + m1.getCode() + "</li></ul>" +
        "<ul><strong>Maison</strong><li>" + jim.toString() + " : " + jim.cursus +  "</li></ul>";
}

