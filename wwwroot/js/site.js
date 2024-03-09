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
        .catch(error => {
            console.error('Unable to get items.', error);
            alert("The token has expired")
            location.href = "index.html";
        });
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
        .catch(error => {
            console.error('Unable to add item.', error);
            alert("The token has expired")
            location.href = "index.html";
        });
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

function displayEditUser(id) {
    const item = tasks.find(item => item.id === id);
    document.getElementById('edit-userName').value = item.name;
    document.getElementById('edit-userPassword').value = item.name;
    document.getElementById('edit-userId').value = item.id;
    document.getElementById('editUser').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        //ניסיתי להוריד אבל זה עשה שגיאה... בעיקרון צריך למחוק את זה
        id: parseInt(itemId, 10),
        IsDo: document.getElementById('edit-IsDo').checked,
        // IsDo: false,
        name: document.getElementById('edit-name').value.trim()
    };
    // var myHeaders = new Headers();
    // myHeaders.append("Authorization", "Bearer " + token);
    // myHeaders.append("Content-Type", "application/json");
    fetch(`${uri}/${itemId}`, {
            method: 'PUT',
            headers: myHeaders,
            redirect: 'follow',
            // headers: {
            //     'Accept': 'application/json',
            //     'Content-Type': 'application/json',
            //     'token': "Bearer " + token
            // },
            body: JSON.stringify(item)
        })
        .then(() => getItems(token))
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

        // let editUser = document.createElement('editUser');
        // let editButtonEditUser = button.cloneNode(false);
        // editButtonEditUser.innerText = 'EditUser';
        // editButtonEditUser.setAttribute('onclick', `displayEditUser(${item.id})`);
        // editUser.appendChild(editButtonEditUser);

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


// function editUser() {
//     const button = document.createElement('button');
//     let editUser = document.createElement('editUser');
//     let editButton_EditUser = button.cloneNode(false);
//     editButton_EditUser.innerText = 'editUser';
//     alert(editButton_EditUser.innerText)

//     editButton_EditUser.setAttribute('onclick', `displayEditUser(${221})`);

//     editUser.appendChild(editButton_EditUser);
// }

// editUser()

/******************************************************************************************************* */


// const uriUser = '/Admin/Get';
// const url = '/Admin';
// let users = [];


// getUserItems(token);


// function getUserItems(token) {
//     var requestOptions = {
//         method: 'GET',
//         headers: myHeaders,
//         redirect: 'follow'
//     };
//     fetch(uriUser, requestOptions)
//         .then(response => response.json())
//         .then(data => _displayUserItems(data))
//         .catch(error => {
//             console.error('Unable to get items.', error);

//         });
// }



// // function _displayCount2(itemCount) {
// //     const name = (itemCount === 1) ? 'Todo' : 'Todo kinds';
// // }

// function _displayUserItems(data) {
//     const tBody = document.getElementById('users');
//     tBody.innerHTML = '';

//     // _displayCount2(data.length);

//     const button = document.createElement('button');

//     // // data.forEach(item => {
//     // let IsDoCheckbox = document.createElement('input');
//     // IsDoCheckbox.type = 'checkbox';
//     // IsDoCheckbox.disabled = true;
//     // IsDoCheckbox.checked = item.isDo;
//     // let tr = tBody.insertRow();


//     // // if (item.userId = 222) {
//     // let editButton_EditUser = button.cloneNode(false);
//     // editButton_EditUser.innerText = 'editUser';
//     // // editButton_EditUser.setAttribute('onclick', `displayEditUser(${item.username})`);
//     // editButton_EditUser.setAttribute('onclick', `displayEditUser(${222})`);
//     // // tr.appendChild(editButton_EditUser);
//     // let td1 = tr.insertCell(0);
//     // td1.appendChild(editButton_EditUser);
//     // alert("gfy")

//     // // }
//     // // });

//     // users = data;
//     data.forEach(item => {
//         let IsDoCheckbox = document.createElement('input');
//         IsDoCheckbox.type = 'checkbox';
//         IsDoCheckbox.disabled = true;
//         IsDoCheckbox.checked = item.isAdmin;


//         let deleteButton = button.cloneNode(false);
//         deleteButton.innerText = 'Delete';
//         deleteButton.setAttribute('onclick', `deleteItem(${item.userId})`);

//         let tr = tBody.insertRow();

//         let td1 = tr.insertCell(0);
//         td1.appendChild(IsDoCheckbox);

//         // let td2 = tr.insertCell(1);
//         // let textNode = document.createTextNode(item.username);
//         // td2.appendChild(textNode);

//         let td3 = tr.insertCell(2);
//         td3.appendChild(deleteButton);
//     });
// }



// ****************************************************************

const editUsername=document.getElementById('editUsername');
const editUserpassword=document.getElementById('editUserpassword');
const saveUser=document.getElementById('saveUser');


const urlUser = '/user';
let currentUser = [];
function getUser() {
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };
    fetch(urlUser, requestOptions)
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => {
            console.error('Unable to get items.', error);
        });
}
