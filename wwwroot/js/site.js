const uri = '/todo';
let tasks = [];
const token = localStorage.getItem("token");
var myHeaders = new Headers();
myHeaders.append("Authorization", "Bearer " + token);
myHeaders.append("Content-Type", "application/json");

showLink();
getItems();

function showLink() {
    const showLink = document.getElementById('showLink');
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };
    fetch('/user/GetAll', requestOptions)
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => {
            console.error(error);
            showLink.style = 'display:none';
        });
}


function getItems() {
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };
    fetch(uri, requestOptions)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => {
            console.error('Unable to get items.', error);
            alert("The token has expired")
            location.href = "index.html";
        });
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const item = {
        IsDo: false,
        name: addNameTextbox.value.trim()
    };

    fetch(uri, {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => {
            console.error('Unable to add item.', error);
            alert("The token has expired")
            location.href = "index.html";
        });
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
            method: 'DELETE',
            headers: myHeaders,
            redirect: 'follow'
        })
        .then(() => getItems())
        .catch(error => {
            console.error('Unable to delete item.', error);
            alert("The token has expired")
            location.href = "index.html";
        });
}

function displayEditForm(id) {
    const item = tasks.find(item => item.id === id);
    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-IsDo').checked = item.IsDo;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        IsDo: document.getElementById('edit-IsDo').checked,
        name: document.getElementById('edit-name').value.trim()
    };

    fetch(`${uri}/${itemId}`, {
            method: 'PUT',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(item)
        })
        .then(() => getItems())
        .catch(error => {
            console.error('Unable to update item.', error);
            alert("The token has expired")
            location.href = "index.html";
        });

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'pizza' : 'Task kinds';
}

function _displayItems(data) {
    const tBody = document.getElementById('tasks');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {

        let IsDoCheckbox = document.createElement('input');
        IsDoCheckbox.type = 'checkbox';
        IsDoCheckbox.disabled = true;
        IsDoCheckbox.checked = item.isDo;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(IsDoCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    tasks = data;
}

// Allow users to update their own details

const urlUser = '/user';
const editUsername = document.getElementById('editUsername');
const editUserPassword = document.getElementById('editUserpassword');

getUser();

let userid;
let isAdmin;

function getUser() {
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };
    fetch(urlUser, requestOptions)
        .then(response => response.json())
        .then(data => showCurrentUser(data))
        .catch(error => {
            console.error('Unable to get user!!!.', error);
        });
}

function showCurrentUser(data) {
    editUsername.value = data.username;
    editUserPassword.value = data.password;
    userid = data.userId;
    isAdmin = data.isAdmin;
}

function updateUser() {
    const newUser = {
        username: editUsername.value.trim(),
        password: editUserPassword.value.trim(),
        userId: userid,
        isAdmin: isAdmin
    };
    fetch(urlUser, {
            method: 'PUT',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newUser)
        })
        .then(() => getUser())
        .catch(error => {
            console.error('Unable to edit user', error);
        });
    return false;
}