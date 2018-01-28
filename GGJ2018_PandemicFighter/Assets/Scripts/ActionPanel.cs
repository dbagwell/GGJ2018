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

	}

	public void SendCureButtonPressed() {

	}

	public void MutateButtonPressed() {

	}

	public void SpreadButtonPressed() {

	}
}
