const url = '/Admin/Login';

document.getElementById('btn-google-connect').addEventListener('click', function() {
    // Redirect to the Google authentication endpoint or trigger the authentication flow
    window.location.href = 'https://localhost:7290//google-authentication-endpoint';
});


function Login() {
    localStorage.clear();
    var myHeaders = new Headers();
    const name = document.getElementById('name').value.trim();
    const password = document.getElementById('password').value.trim();

    myHeaders.append("Content-Type", "application/json");
    var raw = JSON.stringify({
        Username: name,
        Password: password
    })
    var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
    };

    fetch(url, requestOptions)
        .then((response) => response.text())
        .then((result) => {
            if (result.includes("401")) {
                name.value = "";
                password.value = "";
                alert("not exist!!")
            } else {
                token = result;
                localStorage.setItem("token", token)
                location.href = "task.html";

            }
        }).catch((error) => alert("error", error));
}