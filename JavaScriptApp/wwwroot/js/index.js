fetch("http://localhost:63958/retailers")
    .then((response) => response.json()).then(parsed_response => console.log(parsed_response))
    .catch((e) => { console.log(e) })

async function downloadData() {
    const response = await fetch("http://localhost:63958/retailers", {});
    const response_json = await response.json();
    console.log(response_json);
}

downloadData();

async function createRetailer() {
    const response = await fetch("http://localhost:63958/retailers", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            "retailerName": "test actor for fetch"
        })
    });
    console.log(response);
}
addActor();

async function removeRetailer() {
    const response = await fetch("http://localhost:63958/retailers", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
    });
    console.log(response);
}
removeRetailer();

async function getRetailer() {
    const response = await fetch("http://localhost:63958/retailers", {});
    console.log(await response.json());
}
getRetailer();