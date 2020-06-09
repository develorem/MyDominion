using System.Collections.Generic;
using Dev.Dominion.Scraper.Models;

namespace Dev.Dominion.Scraper.Parsing
{
    public class CardTypeMapper
    {
        public Dictionary<string,CardType> Map { get; private set; }

        public CardTypeMapper()
        {
            Map = new Dictionary<string, CardType>();
            Map.Add("Action", CardType.Action);
            Map.Add("Action - Reaction", CardType.ActionReaction);
            Map.Add("Treasure - Reaction", CardType.TreasureAction);
            Map.Add("Action - Ruins", CardType.ActionRuins);
            Map.Add("Victory - Shelter", CardType.VictoryShelter);
            Map.Add("Action - Shelter", CardType.ActionShelter);
            Map.Add("Reaction - Shelter", CardType.ReactionShelter);
            Map.Add("Action - Looter", CardType.ActionLooter);
            Map.Add("Action - Attack - Knight", CardType.ActionAttackKnight);
            Map.Add("Action - Attack - Looter", CardType.ActionAttackLooter);
            Map.Add("Action - Attack - Knight - Victory", CardType.ActionAttackKnightVictory);
            Map.Add("Treasure - Reserve", CardType.TreasureReserve);
            Map.Add("Action - Traveller", CardType.ActionTraveller);
            Map.Add("Action - Reserve", CardType.ActionReserve);
            Map.Add("Action - Duration", CardType.ActionDuration);
            Map.Add("Action - Duration - Reaction", CardType.ActionDurationReaction);
            Map.Add("Action - Reserve - Victory", CardType.ActionReserveVictory);
            Map.Add("Action - Attack - Duration", CardType.ActionAttackDuration);
            Map.Add("Treasure - Attack", CardType.TreasureAttack);
            Map.Add("Action - Attack - Traveller", CardType.ActionAttackTraveller);
            Map.Add("Curse", CardType.Curse);
            Map.Add("Action - Attack", CardType.ActionAttack);
            Map.Add("Victory", CardType.Victory);
            Map.Add("Action - Victory", CardType.ActionVictory);
            Map.Add("Treasure", CardType.Treasure);
            Map.Add("Action - Prize", CardType.ActionPrize);
            Map.Add("Treasure - Prize", CardType.TreasurePrize);
        }

        public CardType Find(string cardType)
        {
            if (Map.ContainsKey(cardType) == false) return CardType.Unknown;
            return Map[cardType];
        }
    }
}