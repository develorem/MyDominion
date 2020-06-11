export interface DataCard {
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
}