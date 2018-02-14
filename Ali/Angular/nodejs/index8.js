var nodemailer = require('nodemailer');

var transporter = nodemailer.createTransport({
    service: 'gmail',
    auth: {
        user: 'gtmangular@gmail.com',
        pass:'987321654'
    }
});
var mailOptions = {
    from: 'gtmangular@gmail.com',
    to: 'gtmangular@gmail.com',
    subject: 'Test avec Nodejs',
    //text: 'Et voilà ! '
    html: '<h1>Et voilà !</h1>'
};

transporter.sendMail(mailOptions, function (error, info) {
    if (error)
        console.log(error);
    else console.log('c\'est envoyé !');
});
