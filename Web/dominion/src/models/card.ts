export interface Card {
    Name: string;
    SetName: string;
    LocalImageFileName: string;
    CardType: number;
    Cost: number;

    SpecialText: string;
    FullText: string;

    PlusBuy: number;
    PlusAction: number;
    PlusCard: number;
    PlusCoin: number;
    PlusVictory: number;

    HasSpecialEffect: boolean;

    IsVictoryCard: boolean;
    IsTreasureCard: boolean;
    IsActionCard: boolean;
    IsReactionCard: boolean;
    IsKnightCard: boolean;
    IsDurationCard: boolean;    
}

export default Card;
