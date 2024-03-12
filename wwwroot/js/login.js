const url = '/login';

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


// function redirectToPostman() {
//     // window.location.href = 'https://www.getpostman.com/localhost:7290/login';
//     // window.location.href = 'https://grey-equinox-798450.postman.co/workspace/06112675-7103-4667-81df-f350245a08e2/request/31280363-317e0a16-d053-4de2-864b-89c014914c56?ctx=documentation';

    
// }