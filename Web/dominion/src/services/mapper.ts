import { DataSet, Set, DataCard, Card } from "../models";

export const mapSet = (set:DataSet): Set => {
    const newSet = Object.assign({}, set) as Set;
    newSet.Cards = [];
    for (let c of set.Cards) {
        newSet.Cards.push(mapCard(c));
    }
    return newSet;
}

export const mapCard = (card:DataCard): Card => {
    const newCard = Object.assign({}, card) as Card;
    newCard.IsVictoryCard = isVictoryType(card.CardType);
    newCard.IsActionCard = isActionType(card.CardType);
    newCard.IsTreasureCard = isTreasureType(card.CardType);
    newCard.IsReactionCard = isReactionType(card.CardType);
    newCard.IsDurationCard = isDurationType(card.CardType);
    newCard.IsKnightCard = isKnightType(card.CardType);
    return newCard;
}

const isVictoryType = (cardType:number): boolean => {
    const validTypes:number[] = [2, 5, 12, 17, 21, 23, 26];
    return validTypes.indexOf(cardType) > 0;
}

const isTreasureType = (cardType:number): boolean => {
    const validTypes:number[] = [1, 3, 13, 24, 27, 29];
    return validTypes.indexOf(cardType) > 0;
}

const isActionType = (cardType:number): boolean => {
    const validTypes:number[] = [0, 3, 4, 6, 8, 9, 10, 11, 12, 14, 15, 16, 17, 18, 19, 20, 21, 22, 25, 26, 27, 28];
    return validTypes.indexOf(cardType) > 0;
}

const isReactionType = (cardType:number): boolean => {
    const validTypes:number[] = [7, 8, 19];
    return validTypes.indexOf(cardType) > 0;
}

const isDurationType = (cardType:number): boolean => {
    const validTypes:number[] = [14, 18, 19, 20, 21];
    return validTypes.indexOf(cardType) > 0;
}

const isKnightType = (cardType:number): boolean => {
    const validTypes:number[] = [10, 12];
    return validTypes.indexOf(cardType) > 0;
}