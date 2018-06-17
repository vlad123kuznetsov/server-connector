using System.Collections.Generic;
using Models.Data;
using UnityEngine;

public class DataScriptableObject : ScriptableObject
{
    public List<TraitModel> traits = new List<TraitModel>();
    public List<CardModel> cardModel = new List<CardModel>();

}