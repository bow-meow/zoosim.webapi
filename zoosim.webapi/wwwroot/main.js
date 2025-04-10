import * as api from './api.js';

let date = new Date();
let hoursPassed = 0;
const MAX_FATIGUE_TIME = 20
let fatigueTime = 0;

localStorage.setItem("tabId", crypto.randomUUID());

document.getElementById('clock').textContent = new Date().toLocaleTimeString();

const state = await api.getAllAnimals();
addAnimals(state);

setInterval(async function updateTimeBy1Hour() {
    hoursPassed += 1;

    document.getElementById('hoursPassed').textContent = hoursPassed;

    let timestamp = date.setHours(date.getHours() + 1);
    date = new Date(timestamp);
    document.getElementById('clock').textContent = date.toLocaleTimeString();

    const state = await api.cycleTimeBy1Hour();
    addAnimals(state);
}, 20000);
setInterval(function () {
    fatigueTime += 1;

    if (fatigueTime > MAX_FATIGUE_TIME) {
        fatigueTime = 1;
    }
    document.getElementById('fatigueTimer').textContent = fatigueTime;

    let timestamp = date.setSeconds(date.getSeconds() + 1);
    date = new Date(timestamp);
    document.getElementById('clock').textContent = date.toLocaleTimeString();
}, 1000);

document.getElementById("feedButton").addEventListener('click', async function(_){
    const state = await api.feedAllAnimals();
    addAnimals(state);
});
document.getElementById("resetButton").addEventListener('click', async function (_) {
    hoursPassed = 0;
    date = new Date();
    document.getElementById('hoursPassed').textContent = hoursPassed;
    document.getElementById('clock').textContent = date.toLocaleTimeString();

    const state = await api.reviveAllAnimals();
    addAnimals(state);
});

function addAnimals(state) {
    const animalConfig = {
        monkeys: {
            gridId: "monkey-grid",
            aliveAsset: "mankey.png",
            deadAsset: "mankey_dead.png",
        },
        giraffes: {
            gridId: "giraff-grid",
            aliveAsset: "girafarig.png",
            deadAsset: "girafarig_dead.png",
        },
        elephants: {
            gridId: "elephant-grid",
            aliveAsset: "phanpy.png",
            deadAsset: "phanpy_dead.png",
        }
    };

    for (const [key, { gridId, aliveAsset, deadAsset }] of Object.entries(animalConfig)) {
        const grid = document.getElementById(gridId);
        const animals = state[key];
        grid.innerHTML = "";

        const isCompact = animals.length > 11;

        animals.forEach(animal => {
            const asset = animal.isAlive ? aliveAsset : deadAsset;
            const card = createAnimalCard(asset, animal);
            if (isCompact) {
                card.style.flex = "0 0 50px";
            }
            grid.appendChild(card);
        });
    }
}

function createAnimalCard(asset, animal){
    let div = document.createElement("div");
    let img = document.createElement("img");
    let stats = document.createElement("div");
    let progress = document.createElement("div");
    
    stats.textContent = animal.canMove ? "can move" : "cant move";
    stats.style = "text-align:center;background-color:white;width:100%;border-top:1px black solid;border-bottom:1px black solid;";

    div.classList.add("animal-card");
    
    img.src = `assets/${asset}`;
    img.alt = "animal";
    
    progress.classList.add("bar-fill");
    progress.style.width = `${animal.health}%`;
    progress.textContent = `${animal.health}%`;

    div.appendChild(img);
    div.appendChild(stats);
    div.appendChild(progress);
    return div;
}