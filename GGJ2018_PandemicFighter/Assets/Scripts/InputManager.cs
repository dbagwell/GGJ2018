using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum InputState {
	InfectFirstCity,
	SelectCity,
	Transmit,
	EndTurn
}

public class InputManager : MonoBehaviour {

    public enum Clickables
    {
        Nothing,
        City,
        Strain,
        Button,
		Panel
    }
    public const string CITY_TAG = "City";
    public const string STRAIN_TAG = "Strain";
	public const string BUTTON_TAG = "Button";
	public const string PANEL_TAG = "Panel";

	public AudioSource infectAudioSource;
	public AudioSource cureAudioSource;
    
    public CityManager cm;
    public TurnManager tm;
	public CityInfoPanel cityInfoPanel;
	public ActionPanel actionPanel;
	public Button endTurnButton;
    public List<RaycastResult> hitObjects = new List<RaycastResult>();

    // Holding Variables
    public GameObject selectedObject;
    public Clickables lastSelected = Clickables.Nothing;
    public City selectedCity;
    public bool citySelected = false;
    public Disease selectedDisease;
    public bool diseaseSelected = false;

	InputState inputState = InputState.InfectFirstCity;
	public InputState InputState {
		get {
			return inputState;
		}
		set {
			inputState = value;
			endTurnButton.interactable = inputState == InputState.EndTurn;
		}
	}

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedObject = GetTransformUnderMouse();

            if (selectedObject != null)
            {
                switch (lastSelected)
                {
                    case Clickables.Button: {
                            Debug.Log("Some sort of Button");
                            string tempText = selectedObject.GetComponentInChildren<Text>().text;
                            Debug.Log(tempText);
                            break;
                        }
				case Clickables.Strain: {
						Debug.Log("Disease");

						StrainItem strainItem = selectedObject.GetComponent<StrainItem>();
						selectedDisease = strainItem.Disease;

						actionPanel.disease = selectedDisease;
						actionPanel.city = selectedCity;
						actionPanel.cityManager = cm;

						switch (tm.currentPlayer) {
						case Player.Disease: {
								// Bring up options to mutate or spread
								actionPanel.State = ActionPanelState.Disease;
								actionPanel.gameObject.SetActive(true);
								break;
							}

						case Player.Doctor: {
								if (selectedDisease.isCured) {
									//Setup UI for sending cure
									actionPanel.State = ActionPanelState.SendCure;
									actionPanel.gameObject.SetActive(true);
								} else {
									// Bring up button asking to cure
									actionPanel.State = ActionPanelState.Research;
									actionPanel.gameObject.SetActive(true);
								}

								break;
							}
						}
                        
						break;
                    }
                    case Clickables.City: {
						switch (inputState) {
						case InputState.InfectFirstCity: {
							cm.InfectCity(selectedObject, 0);
							cm.firstCityInfected = true;
							InputState = InputState.SelectCity;
							infectAudioSource.Play();
							break;
						} 
						case InputState.Transmit: {
							City nextCity = selectedObject.GetComponent<City>();

							switch (tm.currentPlayer) {
							case Player.Disease: {
								for (int i = 0; i<selectedCity.diseaseConnectingCities.Count; i++) {
									if (selectedCity.diseaseConnectingCities[i] == nextCity) {
                                        if (cm.InfectCity(nextCity.gameObject, selectedDisease.StrainID))
                                        {
                                            cm.ResetLines(tm.currentPlayer);
											selectedCity.diseaseTransmitAnimation.SetActive(false);
                                            InputState = InputState.EndTurn;
											infectAudioSource.Play();
                                        }
									}
								}
								break;
							}
							case Player.Doctor: {
								for (int i = 0; i<selectedCity.connectingCities.Count; i++) {
									if (selectedCity.connectingCities[i] == nextCity) {
                                    if (cm.isAlive(nextCity))
                                    {
                                        if (cm.CureCity(nextCity.gameObject, selectedDisease.StrainID))
                                        {
                                            cm.ResetLines(tm.currentPlayer);
                                            selectedCity.cureTransmitAnimation.SetActive(false);
                                            InputState = InputState.EndTurn;
											cureAudioSource.Play();
                                        }
                                    }
									}
								}
								break;
							}
							}
							break;
						} 
						case InputState.SelectCity: {
                            selectedCity = selectedObject.GetComponent<City>();
                            if (cm.isAlive(selectedCity))
                            {
                                
                                cityInfoPanel.City = selectedCity;
                                cityInfoPanel.gameObject.SetActive(true);
                            }
							break;
						}
						}
						break;
                    }
                }
            }
            else
            {
                citySelected = false;
                diseaseSelected = false;
            }
        }
    }
    private GameObject GetObjectUnderMouse()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0)
        {
            return null;
        }

        return hitObjects[0].gameObject;
    }

    private GameObject GetTransformUnderMouse()
    {
        GameObject clickedObject = GetObjectUnderMouse();

		while (clickedObject != null && clickedObject.transform.parent != null && clickedObject.tag == "Untagged") {
			clickedObject = clickedObject.transform.parent.gameObject;
		}

		if (clickedObject != null) {

            switch(clickedObject.tag)
            {
                case CITY_TAG:
                    {
                        citySelected = true;
                        lastSelected = Clickables.City;
                        break;
                    }
                case STRAIN_TAG:
				{
                        lastSelected = Clickables.Strain;
                        diseaseSelected = true;
                        break;
                    }
                case BUTTON_TAG:
                    {
                        lastSelected = Clickables.Button;
                        break;
                    }
			case PANEL_TAG:
				{
					lastSelected = Clickables.Panel;
					break;
				}

                default:
				{
					lastSelected = Clickables.Nothing;
					if (actionPanel.gameObject.activeSelf) {
						actionPanel.gameObject.SetActive(false);
					} else {
						cityInfoPanel.gameObject.SetActive(false);

						if (inputState == InputState.Transmit) {
							InputState = InputState.SelectCity;
							cm.ResetLines(tm.currentPlayer);
							selectedCity.diseaseTransmitAnimation.SetActive(false);
							selectedCity.cureTransmitAnimation.SetActive(false);
						}

					}
                        return null;
                    }
            }
            return clickedObject;
        }

        return null;
    }

}
