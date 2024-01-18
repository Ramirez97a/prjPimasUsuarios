﻿$(document).ready(function () {

    $(document).on('click', '#btnsuccesLogin', function () {
        Login();
    });


});


function Login() {
    var email = $("#email").val();
    var password = $("#password").val();
    var url = "Login/LoginUser";
    let formData = {
        usermail: email,
        userPassword: password
    };

    fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(formData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error en la solicitud. Código: " + response.status);
            }
            return response.json();
        })
        .then(data => {
            console.log(data);
            const userId = data.userId;
            if (data.StatusCode == 200) {
                //window.location.href = "/group/Index?userId=" + userId;
                window.location.href = "/group/Index";
                console.log(userId);
            }
            else {
                alert("usuario no autorizado ");
            }
           
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}

