using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

    public enum Clickables
    {
        Nothing,
        City,
        Disease,
        Cure,
        Strain,
        Button
    }
    public const string CLICKABLE_TAG = "Clickable";
    public const string CITY_TAG = "City";
    public const string STRAIN_TAG = "Strain";
    public const string DISEASE_TAG = "Disease";
    public const string CURE_TAG = "Cure";
    public const string BUTTON_TAG = "Button";
    
    public CityManager cm;
    public TurnManager tm;
	public CityInfoPanel cityInfoPanel;
    public List<RaycastResult> hitObjects = new List<RaycastResult>();

    // Holding Variables
    public GameObject selectedObject;
    public Clickables lastSelected = Clickables.Nothing;
    public City selectedCity;
    public bool citySelected = false;
    public Disease selectedDisease;
    public bool diseaseSelected = false;

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
                    case Clickables.Button:
                        {
                            Debug.Log("Some sort of Button");
                            string tempText = selectedObject.GetComponentInChildren<Text>().text;
                            Debug.Log(tempText);
                            break;
                        }
                    case Clickables.Strain:
                        {
                            Debug.Log("Strain Button");
                            break;
                        }
                    case Clickables.City: {

						if (tm.turn == 0 && tm.currentPlayer == Player.Disease && !cm.firstCityInfected) {
							cm.InfectCity(selectedObject, 0);
							cm.firstCityInfected = true;
						} else {
							City city = selectedObject.GetComponent<City>();
							cityInfoPanel.City = city;
							cityInfoPanel.gameObject.SetActive(true);
						}
							
//                            //A City has been selected
//                            Player currentPlayer = tm.currentPlayer;
//                            City tempCity = selectedObject.GetComponent<City>();
//
//                            // If current Player is the disease
//                            if (currentPlayer == Player.Disease)
//                            {
//                                cm.InfectCity(selectedObject);
//                            }
//                            // if current Player is the Doctors
//                            else
//                            {
//                                cm.CureCity(selectedObject);
//                            }
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

        if (clickedObject != null)
            {
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
                default:
                    {
                        lastSelected = Clickables.Nothing;
                        return null;
                    }
            }
            return clickedObject;
        }

        return null;
    }

}
