using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models.Data;
using UnityEngine;

public interface IDataLoaderService
{
	List<TraitModel> Traits { get; }
	List<CardModel> CardModel { get; }

	CardModel GetCardById(string id);
	TraitModel GetTraitByMode(string id);
}

public class DataLoaderService : IDataLoaderService
{
	private DataScriptableObject obj;

	public DataLoaderService()
	{
		obj = Resources.Load<DataScriptableObject>("data");
	}

	public List<TraitModel> Traits => obj.traits;
	
	public List<CardModel> CardModel => obj.cardModel;
	
	public CardModel GetCardById(string id)
	{
		return CardModel.Find(p => p.id == id);
	}

	public TraitModel GetTraitByMode(string id)
	{
		return Traits.Find(p => p.id == id);
	}
}


