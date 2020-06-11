import { Set } from '../models';

let allSets: Set[];

const setIsLoaded = () : boolean => {
    return (allSets && allSets.length > 0);
}

const httpGet = async <T>(url:string): Promise<T> => {
    const response = await fetch(url);
    const body = await response.json();

    console.log('httpGet.body')
    return body;
}

export const getCardDataOld = async (): Promise<Set[]> => {
    console.log('getCardData entry');
    if (setIsLoaded()) return allSets;
    console.log('getCardData.allSets', allSets);
    const sets: string[] = [ '2nd', 'adventures', 'alchemy', 'base', 'basecards', 'common', 'cornucopia', 'darkages', 'guilds', 'hinterlands', 'intrigue', 'promo', 'prosperity', 'seaside'];

    allSets = [];

    for (let s of sets) {
        const path = `/carddata/${s}/cards.dom`;
        console.log('Set (' + s + ').path', path);
        const setJson = await httpGet<Set>(path);
        console.log('Set (' + s + ').json', setJson);
        allSets.push(setJson);        
    }

    console.log('getCardData.allSets', allSets);    

    return allSets;
}