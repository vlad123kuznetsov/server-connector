using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

[Serializable]
public class UserState
{
	public List<string> openedTraits = new List<string>();
	public List<string> openedCards = new List<string>();
}

[Serializable]
public class CardState
{
	public string cardId;
	public int hp;
	public int defense;
	public List<string> appliedTraits = new List<string>();
}

[Serializable]
public class PlayerState
{
	public List<CardState> cards = new List<CardState>();
}

[Serializable]
public class GameState
{
	public bool myTurn;
	
	public PlayerState MyState;
	public PlayerState OpponentState;
}