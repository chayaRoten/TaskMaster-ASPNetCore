const uri = '/Admin/Get';
const url = '/Admin';
let users = [];
const token = localStorage.getItem("token");


var myHeaders = new Headers();
myHeaders.append("Authorization", "Bearer " + token);
myHeaders.append("Content-Type", "application/json");
getItems(token);

function getItems(token) {
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };
    fetch(uri, requestOptions)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addPasswordTextbox = document.getElementById('add-password');
    const item = {
        isAdmin: false,
        Username: addNameTextbox.value.trim(),
        Password: addPasswordTextbox.value.trim()
    };

    fetch(url, {
        method: 'POST',
        headers: myHeaders,
        redirect: 'follow',
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems(token);
            addNameTextbox.value = '';
            addPasswordTextbox.value = '';
        })
        .catch(error => { 
            if(error.status===401)
                location.href="../"
            console.error('Unable to add item.', error);
            
    });
}

function deleteItem(id) {
    fetch(`/Admin/${id}`, {
        method: 'DELETE',
        headers: myHeaders,
        redirect: 'follow'
    })
        .then(() => getItems(token))
        .catch(error => {
            console.error('Unable to delete item.', error);
            // if(error.status===401)
            //     location.href="../";
    });
}


function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'Todo' : 'Todo kinds';
}

function _displayItems(data) {
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let IsDoCheckbox = document.createElement('input');
        IsDoCheckbox.type = 'checkbox';
        IsDoCheckbox.disabled = true;
        IsDoCheckbox.checked = item.isAdmin;


        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.userId})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(IsDoCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.username);
        td2.appendChild(textNode);


        let td3 = tr.insertCell(2);
        td3.appendChild(deleteButton);
    });

    users = data;

}