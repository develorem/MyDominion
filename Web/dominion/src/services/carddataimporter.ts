import Second from '../carddata/2nd/cards.json';
import Adventures from '../carddata/adventures/cards.json';
import Alchemy from '../carddata/alchemy/cards.json';
import Base from '../carddata/base/cards.json';
import BaseCards from '../carddata/basecards/cards.json';
import Common from '../carddata/common/cards.json';
import Cornucopia from '../carddata/cornucopia/cards.json';
import DarkAges from '../carddata/darkages/cards.json';
import Guilds from '../carddata/guilds/cards.json';
import Hinterlands from '../carddata/hinterlands/cards.json';
import Intrigue from '../carddata/intrigue/cards.json';
import Promo from '../carddata/promo/cards.json';
import Prosperity from '../carddata/prosperity/cards.json';
import Seaside from '../carddata/seaside/cards.json';
import { DataSet, Set } from '../models';
import { mapSet } from './mapper';

let allSets: Set[];

const setIsLoaded = () : boolean => {
    return (allSets && allSets.length > 0);
}

export const getCardData = (): Set[] => {
    if (setIsLoaded()) return allSets;
    
    const dataSets:DataSet[] = [];
    dataSets.push(Second as DataSet);
    dataSets.push(Adventures as DataSet);
    dataSets.push(Alchemy as DataSet);
    dataSets.push(Base as DataSet);
    dataSets.push(BaseCards as DataSet);
    dataSets.push(Common as DataSet);
    dataSets.push(Cornucopia as DataSet);
    dataSets.push(DarkAges as DataSet);
    dataSets.push(Guilds as DataSet);
    dataSets.push(Hinterlands as DataSet);
    dataSets.push(Intrigue as DataSet);
    dataSets.push(Promo as DataSet);
    dataSets.push(Prosperity as DataSet);
    dataSets.push(Seaside as DataSet);

    allSets = [];

    for (let ds of dataSets) {
        const newSet = mapSet(ds);
        allSets.push(newSet);
    }

    return allSets;
}
