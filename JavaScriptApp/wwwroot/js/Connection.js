const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:63958/hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

const retailers = new Map();
let selectedRetailer = -1;

async function start() {
    try {
        await connection.start();
        console.log("Connected!");
    }
    catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

async function downloadData() {
    const response = await fetch("http://localhost:63958/retailers", {});
    const response_json = await response.json();
    for (const retailer of response_json) {
        retailers.set(retailer.id, retailer);
    }
}
function showData() {
    for (const [id, retailer] of retailers) {
        const div = document.querySelector('#retailer-data');
        div.innerHTML += `<div id='retailer-${retailer.id}'><label>${id}</label >
                                <label>${retailer.name}</label>
                                <button onclick=selectedRetailer('${retailer.id}')>Select!</button>
                                <button onclick=removeRetailer('${retailer.id}')>!</button>
        </div>`;
    }
}

function selectedRetailer(id) {
    selectedRetailer = parseInt(id);
    const input = document.querySelector('#retailer-name');
    input.value = retailer.get(selectedRetailer).name;
}

function removeRetailer(id) {
    fetch(`http://localhost:63958/retailers/${id}`, {
        method: "DELETE",
        headers: {
            'Content-Type': 'application/json'
        },
    }).then(() => { });
}

function addOrModify() {
    const retailer_name = document.querySelector('#retailer-name').value;
    if (selectedRetailer !== -1) {
        modifyRetailer(retailer_name);
    }
    else {
        addRetailer(retailer_name);
    }
}

function modifyRetailer(retailer_name) {
    fetch("http://localhost:63958/retailers", {
        method: "POST",
        headers: {
            'Content-Tpye': 'application/json'
        },
        body: JSON.stringify({
            "id": selectedRetailer,
            "name": retailer_name,
        })
    }).then(() => { });
}

function createRetailer(retailer_name) {
    fetch("http://localhost:63958/retailers", {
        method: "POST",
        headers: {
            'Content-Tpye': 'application/json'
        },
        body: JSON.stringify({
            "retailerName": retailer_name,
        })
    }).then(() => { });
}

downloadData().then(() => {
    showData();
});

connection.onclose(async () => {
    console.log("Connection over!");
    await start();
})

connection.on("onRetailerCreated", (retailer) => {
    retailer.set(retailer.id, retailer);
    const div = document.querySelector('#retailer-data');
    div.innerHTML += `<div id='retailer-${retailer.id}'><label>${retailer.id}</label>
                                <label>${retailer.id}</label>
                                <button onclick=selectRetailer('${retailer.id}')>Select!</button>
                                   <button onclick=removeRetailer('${retailer.id}')>Remove!</button>
    </div>`
});

connection.on("onRetailerRemoved", (id) => {
    console.log(id);
    retailer.remove(id);
    const div = document.querySelector(`#retailer-${id}`);
    div.innerHTML = "";
});

connection.on("onRetailerUpdated", (retailer) => {
    retailer.get(retailer.id);
    retailer.set(retailer.id, retailer);
    const div = document.querySelector(`retailer-${retailer.id}`);
    div.innerHTML = "";
    div.innerHTML += `<label>${retailer.id}</label>
                      <button onclick=selectRetailer('${retailer.id}')>Selected!</button>
                      <button onclick=removeRetailer('${retailer.id}')>Removed!</button>
                      `
});

start();