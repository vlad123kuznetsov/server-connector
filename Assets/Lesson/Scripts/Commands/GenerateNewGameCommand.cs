using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Models.Data;
using Models.State;
using strange.extensions.command.impl;
using UnityEngine;

public class GenerateNewGameCommand : Command {
	
	[Inject] public GameState State { get; set; }
	[Inject] public IDataLoaderService DataLoader { get; set; }
	[Inject] public Serializer Serializer { get; set; }
	
	public override void Execute()
	{
		State.MyState = GenerateRandomState();
		State.OpponentState = GenerateRandomState();
		State.myTurn = true;
	}

	private PlayerState GenerateRandomState()
	{
		var cards = DataLoader.CardModel.OrderBy(p => UnityEngine.Random.Range(0f, 1f)).Take(3);
		var cardStates = cards.Select(p => CreateFromModel(p)).ToList();
		
		var newState = new PlayerState();
		newState.cards = cardStates;
		
		return newState;
	}

	private CardState CreateFromModel(CardModel model)
	{
		var state = new CardState
		{
			hp = model.hp,
			cardId = model.id,
			defense = model.defense,
			appliedTraits = new List<string>()
		};

		return state;
	}
}