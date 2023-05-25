const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:63958/hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

const retailers = new Map();
let selectedRetailer = -1;
let selectedWood = -1;
let selectedFurniture = -1;

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
    const response2 = await fetch("http://localhost:63958/woods", {});
    const response3 = await fetch("http://localhost:63958/furnitures", {});
    const response_json = await response.json();
    const response_json2 = await response2.json();
    const response_json3 = await response3.json();
    for (const retailer of response_json) {
        retailers.set(retailer.id, retailer);
    }
    for (const wood of response_json2) {
        woods.set(wood.id, wood);
    }
    for (const furniture of response_json3) {
        furnituress.set(furniture.id, furniture);
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
    for (const [id, wood] of woods) {
        const div = document.querySelector('#wood-data');
        div.innerHTML += `<div id='wood-${wood.id}'><label>${id}</label >
                                <label>${wood.name}</label>
                                <button onclick=selectedWood('${wood.id}')>Select!</button>
                                <button onclick=removeWood('${wood.id}')>!</button>
        </div>`;
    }
    for (const [id, furniture] of retailers) {
        const div = document.querySelector('#furniture-data');
        div.innerHTML += `<div id='furniture-${furniture.id}'><label>${id}</label>
                                <label>${furniture.name}</label>
                                <button onclick=selectedFurniture('${furniture.id}')>Select!</button>
                                <button onclick=removeFurniture('${furniture.id}')>!</button>
        </div>`;
    }
}

function selectedRetailer(id) {
    selectedRetailer = parseInt(id);
    const input = document.querySelector('#retailer-name');
    input.value = retailer.get(selectedRetailer).name;
}
function selectedWood(id) {
    selectedWood = parseInt(id);
    const input = document.querySelector('#wood-name');
    input.value = wood.get(selectedWood).name;
} function selectedFurniture(id) {
    selectedFurniture = parseInt(id);
    const input = document.querySelector('#furniture-name');
    input.value = furniture.get(selectedFurniture).name;
}

function removeRetailer(id) {
    fetch(`http://localhost:63958/retailers/${id}`, {
        method: "DELETE",
        headers: {
            'Content-Type': 'application/json'
        },
    }).then(() => { });
}
function removeWood(id) {
    fetch(`http://localhost:63958/woods/${id}`, {
        method: "DELETE",
        headers: {
            'Content-Type': 'application/json'
        },
    }).then(() => { });
}
function removeFurniture(id) {
    fetch(`http://localhost:63958/furnitures/${id}`, {
        method: "DELETE",
        headers: {
            'Content-Type': 'application/json'
        },
    }).then(() => { });
}

function addOrModify() {
    const retailer_name = document.querySelector('#retailer-name').value;
    const wood_name = document.querySelector('#wood-name').value;
    const furniture_name = document.querySelector('#furniture-name').value;
    if (selectedRetailer !== -1) {
        modifyRetailer(retailer_name);
    }
    else if (retailer_name !== null) {
        addRetailer(retailer_name);
    }
    if (selectedFurniture !== -1) {
        modifyFurniture(furniture_name);
    }
    else if (furniture_name !== null) {
        addFurniture(furniture_name);
    }
    if (selectedWood !== -1) {
        modifyWood(wood_name);
    }
    else if (wood_name !== null) {
        addWood(wood_name);
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
function modifyFurniture(furniture_name) {
    fetch("http://localhost:63958/furnitures", {
        method: "POST",
        headers: {
            'Content-Tpye': 'application/json'
        },
        body: JSON.stringify({
            "id": selectedFurniture,
            "name": furniture_name,
        })
    }).then(() => { });
}
function modifyWood(wood_name) {
    fetch("http://localhost:63958/woods", {
        method: "POST",
        headers: {
            'Content-Tpye': 'application/json'
        },
        body: JSON.stringify({
            "id": selectedWood,
            "name": wood_name,
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
function createFurniture(furniture_name) {
    fetch("http://localhost:63958/furnitures", {
        method: "POST",
        headers: {
            'Content-Tpye': 'application/json'
        },
        body: JSON.stringify({
            "furnitureName": furniture_name,
        })
    }).then(() => { });
}
function createWood(wood_name) {
    fetch("http://localhost:63958/woods", {
        method: "POST",
        headers: {
            'Content-Tpye': 'application/json'
        },
        body: JSON.stringify({
            "woodName": wood_name,
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
                      <button onclick=removeRetailer('${retailer.id}')>Removed!</button>`;
});

connection.on("onWoodCreated", (wood) => {
    wood.set(wood.id, wood);
    const div = document.querySelector('#wood-data');
    div.innerHTML += `<div id='wood-${wood.id}'><label>${wood.id}</label>
                                <label>${wood.id}</label>
                                <button onclick=selectWood('${wood.id}')>Select!</button>
                                   <button onclick=removeWood('${wood.id}')>Remove!</button>
    </div>`
});

connection.on("onWoodRemoved", (id) => {
    console.log(id);
    wood.remove(id);
    const div = document.querySelector(`#wood-${id}`);
    div.innerHTML = "";
});

connection.on("onWoodUpdated", (wood) => {
    wood.get(wood.id);
    wood.set(wood.id, wood);
    const div = document.querySelector(`wood-${wood.id}`);
    div.innerHTML = "";
    div.innerHTML += `<label>${wood.id}</label>
                      <button onclick=selectWood('${wood.id}')>Selected!</button>
                      <button onclick=removeWood('${wood.id}')>Removed!</button>`;
});

connection.on("onFurnitureCreated", (furniture) => {
    furniture.set(furniture.id, furniture);
    const div = document.querySelector('#furniture-data');
    div.innerHTML += `<div id='furniture-${furniture.id}'><label>${furniture.id}</label>
                                <label>${furniture.id}</label>
                                <button onclick=selectFurniture('${furniture.id}')>Select!</button>
                                   <button onclick=removeFurniture('${furniture.id}')>Remove!</button>
    </div>`
});

connection.on("onFurnitureRemoved", (id) => {
    console.log(id);
    furniture.remove(id);
    const div = document.querySelector(`#furniture-${id}`);
    div.innerHTML = "";
});

connection.on("onFurnitureUpdated", (furniture) => {
    furniture.get(furniture.id);
    furniture.set(furniture.id, furniture);
    const div = document.querySelector(`furniture-${furniture.id}`);
    div.innerHTML = "";
    div.innerHTML += `<label>${furniture.id}</label>
                      <button onclick=selectFurniture('${furniture.id}')>Selected!</button>
                      <button onclick=removeFurniture('${furniture.id}')>Removed!</button>`;
});

start();