const uri = '/todo';
let tasks = [];
const token = localStorage.getItem("token");

var myHeaders = new Headers();
myHeaders.append("Authorization", "Bearer " + token);
myHeaders.append("Content-Type", "application/json");

showUser();
getItems(token);

function showUser() {
    const showUser = document.getElementById('showUser');
        var requestOptions = {
            method: 'GET',
            headers: myHeaders,
            redirect: 'follow'
        };
        fetch('/Admin/Get', requestOptions)
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(error => {
                console.error(error);
                showUser.style = 'display:none';
            });
    }


function getItems(token) {
    // fetch(uri , {
    //     method: 'GET',
    //     headers: {
    //         'Accept': 'application/json',
    //         'Content-Type': 'application/json',
    //         'token': "Bearer "+ token
    //     }
    // })
    // var myHeaders = new Headers();
    // myHeaders.append("Authorization", "Bearer " + token);
    // myHeaders.append("Content-Type", "application/json");
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
    // var myHeaders = new Headers();
    // myHeaders.append("Authorization", "Bearer " + token);
    // myHeaders.append("Content-Type", "application/json");
    const item = {
        IsDo: false,
        name: addNameTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: myHeaders,
        redirect: 'follow',
        // headers: {
        //     'Accept': 'application/json',
        //     'Content-Type': 'application/json',
        //     'token': "Bearer " + token
        // },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems(token);
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}
function deleteItem(id) {
    // var myHeaders = new Headers();
    // myHeaders.append("Authorization", "Bearer " + token);
    // myHeaders.append("Content-Type", "application/json");
    fetch(`${uri}/${id}`, {
        method: 'DELETE',
        headers: myHeaders,
        redirect: 'follow'
    })
        .then(() => getItems(token))
        .catch(error => console.error('Unable to delete item.', error));
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
    // var myHeaders = new Headers();
    // myHeaders.append("Authorization", "Bearer " + token);
    // myHeaders.append("Content-Type", "application/json");
    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: myHeaders,
        // headers: {
        //     'Accept': 'application/json',
        //     'Content-Type': 'application/json',
        //     'token': "Bearer " + token
        // },
        body: JSON.stringify(item)
    })
        .then(() => getItems(token))
        .catch(error => console.error('Unable to update item.', error));

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

