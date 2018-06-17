using System;
using System.Collections.Generic;
using System.Linq;
using Models.Data;
using Models.State;
using strange.extensions.command.impl;
using UnityEngine;

namespace Commands
{
    public class MakeTurnCommand : Command
    {
        [Inject]
        public GameState State { get; set; }

        [Inject]
        public IDataLoaderService DataLoader { get; set; }

        [Inject]
        public Serializer Serializer { get; set; }

        public override void Execute()
        {
            PlayerState myState;
            PlayerState opponentState;

            if (State.myTurn)
            {
                myState = State.MyState;
                opponentState = State.OpponentState;
            }
            else
            {
                myState = State.OpponentState;
                opponentState = State.MyState;
            }

            var rndCard = myState.cards[UnityEngine.Random.Range(0, myState.cards.Count)];
            var cardModel = DataLoader.GetCardById(rndCard.cardId);

            foreach (var opponentCards in opponentState.cards)
            {
                var dmg = UnityEngine.Random.Range(cardModel.minDmg, cardModel.maxDmg + 1);

                if (cardModel.type == CardType.Legend)
                {
                    if (cardModel.criticalChance < UnityEngine.Random.Range(0f, 100f))
                    {
                        if (opponentCards.defense != 0)
                        {
                            opponentCards.defense = 0;
                        }
                        else if (opponentCards.hp != 0)
                        {
                            opponentCards.hp = 0;
                        }
                    }
                    else
                    {
                        if (opponentCards.defense != 0)
                        {
                            opponentCards.defense = Mathf.Max(0, opponentCards.defense - dmg);
                        }
                        else
                        {
                            opponentCards.hp = Mathf.Max(0, opponentCards.hp - dmg);
                        }
                    }
                }
            }
        }
    }

    public class GameFinishedCommand : Command
    {
        [Inject]
        public UserState User { get; set; }

        [Inject]
        public IDataLoaderService DataLoader { get; set; }

        public override void Execute()
        {
            var rndTrait = DataLoader.Traits.Random(1);
        }
    }
    
    
    public static class ListExtension
    {
        public static IEnumerable<T> Random<T>(this IEnumerable<T> collection, int count)
        {
            return collection.OrderBy(p => UnityEngine.Random.Range(0f, 1f)).Take(count);
        }
    }
}