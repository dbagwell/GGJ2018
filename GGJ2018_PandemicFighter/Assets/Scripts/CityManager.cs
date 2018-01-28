using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityManager : MonoBehaviour {

    public List<GameObject> cityDirectory;
//    public List<GameObject> diseasedCities;
//    public List<GameObject> deadCities;
//    public List<GameObject> cleanCities;
    public Sprite cleanCity;
    public Sprite diseasedCity;
    public Sprite deadCity;

	public Material blackLineMaterial;

	public GameObject diseasePathsParent;
	public GameObject doctorPathsParent;
	public List<GameObject> diseaseLines = new List<GameObject>();
	public List<GameObject> doctorLines = new List<GameObject>();

    public int outbreakLimit = 5;
	public bool firstCityInfected = false;

	// Use this for initialization
	void Start ()
    {
        //cityDirectory = new List<GameObject>();
//        cleanCities = new List<GameObject>();
//        diseasedCities = new List<GameObject>();
//        deadCities = new List<GameObject>();

        for(int i = 0;i<cityDirectory.Count;i++)
        {
			cityDirectory[i].GetComponent<Image>().sprite = cleanCity;

			City city = cityDirectory[i].GetComponent<City>();

			for (int j = 0; j<city.connectingCities.Count; j++) {
				GameObject line = CreateLineBetweenCities(city, city.connectingCities[j], Player.Doctor);
				city.doctorLines.Add(line);
				city.connectingCities[j].doctorLines.Add(line);
			}
			for (int j = 0; j<city.diseaseConnectingCities.Count; j++) {
				GameObject line = CreateLineBetweenCities(city, city.diseaseConnectingCities[j], Player.Disease);
				city.diseaseLines.Add(line);
				city.diseaseConnectingCities[j].diseaseLines.Add(line);
			}

			doctorPathsParent.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	public void ResetLines(Player player) {
		switch (player) {
		case Player.Disease: {
				for (int i = 0; i<diseaseLines.Count; i++) {
					GameObject line = diseaseLines[i];
					LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
					lineRenderer.material = blackLineMaterial;
				}

				diseasePathsParent.SetActive(true);
				doctorPathsParent.SetActive(false);
				break;
			}
		case Player.Doctor: {
				for (int i = 0; i<doctorLines.Count; i++) {
					GameObject line = doctorLines[i];
					LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
					lineRenderer.material = blackLineMaterial;
				}
				diseasePathsParent.SetActive(false);
				doctorPathsParent.SetActive(true);
				break;
			}
		}


	}

	GameObject CreateLineBetweenCities(City city1, City city2, Player player) {
		GameObject line = new GameObject();
		LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
		lineRenderer.startWidth = 10;
		lineRenderer.endWidth = 10;
		lineRenderer.material = blackLineMaterial;
		lineRenderer.startColor = Color.black;
		lineRenderer.endColor = Color.black;

		Vector3 firstPosition = city1.gameObject.transform.position;
		firstPosition.z = 150;
		Vector3 secondPosition = city2.gameObject.transform.position;
		secondPosition.z = 150;

		lineRenderer.SetPositions(new [] {firstPosition, secondPosition});


		switch (player) {
		case Player.Disease: {
				diseaseLines.Add(line);
				line.transform.parent = diseasePathsParent.transform;
				break;
			}
		case Player.Doctor: {
				doctorLines.Add(line);
				line.transform.parent = doctorPathsParent.gameObject.transform;
				break;
			}
		}

		return line;
	}

    public void StartDiseaseTurn()
    {
        int i;
        City tempCity;
        // Increase outbreak levels
        for (i = 0; i < cityDirectory.Count; i++)
        {
            tempCity = cityDirectory[i].GetComponent<City>();
            if (!tempCity.DoIHaveAllCures())
            {
                IncreaseOutbreak(cityDirectory[i]);
            }
            else
            {
                DecreaseOutbreak(cityDirectory[i]);
            }
        }
    }

    public void StartDoctorTurn()
    {
        //
    }

    public void CureCity(GameObject city/*, Disease strain */)
    {
        int i;
        bool found = false;
        int strainID = 2;

        City tempCity = city.GetComponent<City>();
		tempCity.CureDisease(strainID);

        for (i = 0; i < cityDirectory.Count; i++)
        {
            if (cityDirectory[i] == city)
            {
                found = true;
                break;
            }
        }

        if (found)
        {
            tempCity.DecreaseOutbreakLevel();
            if (tempCity.outbreakLevel == 0)
            {
                city.GetComponent<Image>().sprite = cleanCity;
            }
        }
    }


    public void InfectCity(GameObject city, int strainId)
    {
        int i;
        bool found = false;
        Disease strain = new Disease();
        strain.StrainID = strainId;
		strain.name = "Strain " + strainId;

        City tempCity = city.GetComponent<City>();
        tempCity.AddDisease(strain);

        for (i = 0; i < cityDirectory.Count; i++)
        {
            if (cityDirectory[i] == city)
            {
                found = true;
                break;
            }
        }

        if (found)
        {
            city.GetComponent<Image>().sprite = diseasedCity;
            //cleanCities.RemoveAt(i);
            //diseasedCities.Add(city);
        }
    }

    public void IncreaseOutbreak(GameObject city)
    {
        int i;
        bool found = false;

        City tempCity = city.GetComponent<City>();
        tempCity.IncreaseOutbreakLevel();

        if (tempCity.GetOutbreakLevel() > outbreakLimit)
        {
            //for (i = 0; i < cityDirectory.Count; i++)
            //{
            //    if (cityDirectory[i] == city)
            //    {
            //        found = true;
            //        break;
            //    }
            //}

            //if (found)
            {
                city.GetComponent<Image>().sprite = deadCity;
                //diseasedCities.RemoveAt(i);
                //deadCities.Add(city);
            }
        }
    }

    public void DecreaseOutbreak(GameObject city)
    {
        int i;
        bool found = false;

        City tempCity = city.GetComponent<City>();
        tempCity.DecreaseOutbreakLevel();

        if (tempCity.GetOutbreakLevel() == 0)
        {
            //for (i = 0; i < cityDirectory.Count; i++)
            //{
            //    if (cityDirectory[i] == city)
            //    {
            //        found = true;
            //        break;
            //    }
            //}

            //if (found)
            //{
                city.GetComponent<Image>().sprite = cleanCity;
                //diseasedCities.RemoveAt(i);
                //deadCities.Add(city);
            //}
        }
    }

}
