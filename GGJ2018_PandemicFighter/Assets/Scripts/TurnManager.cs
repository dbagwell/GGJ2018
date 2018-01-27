using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Player {Disease, Doctor};

public class TurnManager : MonoBehaviour {
	
	public GameObject passTurnScreen;
	public GameObject passTurnScreenText;

	public Player currentPlayer = Player.Disease;
	public int turn = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EndTurnButtonPressed () {
		ToggleCurrentPlayer();
		passTurnScreen.SetActive(true);
		turn++;
	}

	public void StartTurnButtonPressed () {
		// send current player to city manager and tell it to update the cities and pathways for that turn
		passTurnScreen.SetActive(false);
	}

	void ToggleCurrentPlayer () {
		Image image = passTurnScreen.GetComponent<Image>();
		Text text = passTurnScreenText.GetComponent<Text>();

		switch (currentPlayer) {
		case Player.Disease: 
			image.color = Color.green;
			text.text = "Doctor's Turn";
			currentPlayer = Player.Doctor; 
			break;

		case Player.Doctor: 
			image.color = Color.red;
			text.text = "Disease's Turn";
			currentPlayer = Player.Disease; 
			break;
		}
	}
}
