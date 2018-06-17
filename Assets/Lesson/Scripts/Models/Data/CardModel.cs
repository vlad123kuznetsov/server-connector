using System;
using UnityEngine;

namespace Models.Data
{
    [Serializable]
    public class CardModel
    {
        public Texture texture;
        public int defense;
        public int hp;
        public float criticalChance;
        public string id;
        public string name;

        public int maxDmg;
        public int minDmg;
        
        public CardType type;
    }
}