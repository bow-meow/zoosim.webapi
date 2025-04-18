const baseAddresss = 'http://localhost:5149/api/zoo';
const reviveEndPoint = `${baseAddresss}/revive`;
const feedEndPoint = `${baseAddresss}/feed`;
const hourEndPoint = `${baseAddresss}/hour`;
const stateEndPoint = `${baseAddresss}`;

const id: string = crypto.randomUUID();

export interface Animal {
    type: string;
    health: number;
    isAlive: boolean;
    canMove: boolean;
    aliveImg: {
        src: string;
        alt: string;
    }
    deadImg: {
        src: string;
        alt: string;
    }
}

export interface ZooState {
    [key: string]: Animal[];
    monkeys: Animal[],
    giraffes: Animal[],
    elephants: Animal[]
}

export async function getAllAnimals(): Promise<ZooState>{
    const response = await fetch(stateEndPoint,
        {
            method: 'GET',
            headers:
            {
                'Tab-ID': id
            }
        });
    if (!response.ok)
        throw new Error(`Error occurred while running ${stateEndPoint}. Status: ${response.statusText}`);
    return await response.json() as ZooState;
}
export async function cycleTimeBy1Hour(): Promise<ZooState>{
    return await internalPost(hourEndPoint);
}
export async function feedAllAnimals(): Promise<ZooState> {
    return await internalPost(feedEndPoint);
}
export async function reviveAllAnimals(): Promise<ZooState> {
    return await internalPost(reviveEndPoint);
}

async function internalPost(address: string): Promise<ZooState>{
    const response = await fetch(address, {
        method: "POST",
        headers:
        {
            'Content-Type': 'application/json',
            'Tab-ID': id
        }
    });
    if (!response.ok)
        throw new Error(`Error occurred while running ${address}. Status: ${response.statusText}`);
    return await response.json() as ZooState;
}
