using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

    public const string CITY_TAG = "City";
    
    public CityManager cm;
    public TurnManager tm;
    public List<RaycastResult> hitObjects = new List<RaycastResult>();

    // Holding Variables
    public GameObject cityObject;
    public bool citySelected = false;
    public Disease selectedDisease;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cityObject = GetCityTransformUnderMouse();

            if (cityObject != null)
            {
                //A City has been selected
                Player currentPlayer = tm.currentPlayer;
                City tempCity = cityObject.GetComponent<City>(); 

                // If current Player is the disease
                if (currentPlayer == Player.Disease)
                {
                    cm.InfectCity(cityObject);
                }
                // if current Player is the Doctors
                else
                {
                    cm.CureCity(cityObject);
                }

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

    private GameObject GetCityTransformUnderMouse()
    {
        GameObject clickedObject = GetObjectUnderMouse();

        if (clickedObject != null && clickedObject.tag == CITY_TAG)
        {
            return clickedObject;
        }

        return null;
    }

}
