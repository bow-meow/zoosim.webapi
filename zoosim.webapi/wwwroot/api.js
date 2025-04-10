const baseAddresss = 'http://localhost:5149/api/zoo';
const reviveEndPoint = `${baseAddresss}/revive`;
const feedEndPoint = `${baseAddresss}/feed`;
const hourEndPoint = `${baseAddresss}/hour`;
const stateEndPoint = `${baseAddresss}`;

const id = crypto.randomUUID();

export async function getAllAnimals(){
    try {
        const response = await fetch(stateEndPoint,
            {
                method: 'GET',
                headers:
                {
                    'X-Tab-ID': id
                }
            });
        if(!response.ok) throw new Error("failed to get state");
        return await response.json();
    } catch(error){
        console.log('error:', error);
    }
}
export async function cycleTimeBy1Hour(){
    return await internalPost(hourEndPoint);
}
export async function feedAllAnimals() {
    return await internalPost(feedEndPoint);
}
export async function reviveAllAnimals() {
    return await internalPost(reviveEndPoint);
}

async function internalPost(address){
    try {
        const response = await fetch(address, {
            method: "POST",
            headers:
            {
                'Content-Type': 'application/json',
                'X-Tab-ID': id
            }
        });
        if (!response.ok) throw new Error(`Failed call ${address}`);
        return await response.json();
    } catch(error) {
        console.log('error:', error);
    }
}
