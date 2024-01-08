$(document).ready(function () {

    $(document).on('click', '#btnsuccesLogin', function () {
        Login();
    });


});


function Login() {
    var email = $("#email").val();
    var password = $("#password").val();
    var url = "https://localhost:44366/api/User/login"; 
    let formData = {
        Email: email,
        Password: password
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

            window.location.href = "/group/Index?userId=" + userId;
            console.log(userId);
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}

