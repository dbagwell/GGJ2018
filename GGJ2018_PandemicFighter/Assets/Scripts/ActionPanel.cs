using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ActionPanelState {Research, SendCure, Disease}

public class ActionPanel : MonoBehaviour {

	public Image image;
	public Button researchButton;
	public Button sendCureButton;
	public Button mutateButton;
	public Button spreadButton;

	public Disease disease;
	public City city;
	public CityManager cityManager;

	public Material yellowLineMaterial;

	public CityInfoPanel cityInfoPanel;
	public InputManager inputManager;

	ActionPanelState state;
	public ActionPanelState State {
		get {
			return state;
		}
		set {
			state = value;

			switch (state) {
			case ActionPanelState.Research: {
					// set image
					researchButton.gameObject.SetActive(true);
					sendCureButton.gameObject.SetActive(false);
					mutateButton.gameObject.SetActive(false);
					spreadButton.gameObject.SetActive(false);
					break;
				}
			case ActionPanelState.SendCure: {
					// set image
					researchButton.gameObject.SetActive(false);
					sendCureButton.gameObject.SetActive(true);
					mutateButton.gameObject.SetActive(false);
					spreadButton.gameObject.SetActive(false);
					break;
				}
			case ActionPanelState.Disease: {
					// set image
					researchButton.gameObject.SetActive(false);
					sendCureButton.gameObject.SetActive(false);
					mutateButton.gameObject.SetActive(true);
					spreadButton.gameObject.SetActive(true);
					break;
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResearchButtonPressed() {
		disease.isCured = true;
		gameObject.SetActive(false);
		cityInfoPanel.gameObject.SetActive(false);
		inputManager.InputState = InputState.EndTurn;
	}

	public void SendCureButtonPressed() {
		gameObject.SetActive(false);
		cityInfoPanel.gameObject.SetActive(false);

		HighlightLines(city.doctorLines);

		inputManager.InputState = InputState.Transmit;
	}

	public void MutateButtonPressed() {
		cityManager.InfectCity(city.gameObject, disease.StrainID+1);
		gameObject.SetActive(false);
		cityInfoPanel.gameObject.SetActive(false);
		inputManager.InputState = InputState.EndTurn;
	}

	public void SpreadButtonPressed() {
		Debug.Log("Spread");
		gameObject.SetActive(false);
		cityInfoPanel.gameObject.SetActive(false);

		HighlightLines(city.diseaseLines);

		inputManager.InputState = InputState.Transmit;
	}

	void HighlightLines(List<GameObject> lines) {
		for (int i = 0; i<lines.Count; i++) {
			GameObject line = lines[i];
			LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
			lineRenderer.material = yellowLineMaterial;
		}
	}
}
