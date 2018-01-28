using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityManager : MonoBehaviour {

    public List<GameObject> cityDirectory;
    public TurnManager tm;
    //    public List<GameObject> diseasedCities;
    //    public List<GameObject> deadCities;
    //    public List<GameObject> cleanCities;
    public Sprite cleanCity;
    public Sprite diseasedCity;
    public Sprite deadCity;

	public Material blackLineMaterial;

	public GameObject diseasePathsParent;
	public GameObject doctorPathsParent;
	public List<Pathway> diseaseLines = new List<Pathway>();
	public List<Pathway> doctorLines = new List<Pathway>();

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
				GameObject line = CreateLineBetweenCities(city, city.connectingCities[j]);
				Pathway pathway = new Pathway();
				pathway.city1 = city;
				pathway.city2 = city.connectingCities[j];
				pathway.line = line;

				if (AddPathwayToList(pathway, doctorLines, Player.Doctor, true)) {
					AddPathwayToList(pathway, city.doctorLines, Player.Doctor, false);
					AddPathwayToList(pathway, city.connectingCities[j].doctorLines, Player.Doctor, false);
				} else {
					GameObject.Destroy(pathway.line);
				}
			}
			for (int j = 0; j<city.diseaseConnectingCities.Count; j++) {
				GameObject line = CreateLineBetweenCities(city, city.diseaseConnectingCities[j]);
				Pathway pathway = new Pathway();
				pathway.city1 = city;
				pathway.city2 = city.diseaseConnectingCities[j];
				pathway.line = line;

				if (AddPathwayToList(pathway, diseaseLines, Player.Disease, true)) {
					AddPathwayToList(pathway, city.diseaseLines, Player.Disease, false);
					AddPathwayToList(pathway, city.diseaseConnectingCities[j].diseaseLines, Player.Doctor, false);
				} else {
					GameObject.Destroy(pathway.line);
				}
			}

			doctorPathsParent.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	bool AddPathwayToList(Pathway pathway, List<Pathway> list, Player player, bool addToScene) {

		bool foundPathway = false;
		for (int i = 0; i<list.Count; i++) {
			if (pathway.city1 == list[i].city1 && pathway.city2 == list[i].city2 || pathway.city1 == list[i].city2 && pathway.city2 == list[i].city1) {
				foundPathway = true;
				break;
			}
		}

		if (!foundPathway) {
			list.Add(pathway);

			if (addToScene) {	
				switch (player) {
				case Player.Disease: {
						Debug.Log(pathway.city1.name + pathway.city2.name);	
						pathway.line.transform.parent = diseasePathsParent.transform;
						break;
					}
				case Player.Doctor: {
						pathway.line.transform.parent = doctorPathsParent.gameObject.transform;
						break;
					}
				}

			}

			return true;
		} else {
			return false;
		}
	}

    public void Reset()
    {
        int i;
        firstCityInfected = false;
        for (i = 0; i < cityDirectory.Count; i++)
        {
            cityDirectory[i].GetComponent<City>().Reset();
            cityDirectory[i].GetComponent<Image>().sprite = cleanCity;
        }
    }

    public void ResetLines(Player player) {
		switch (player) {
		case Player.Disease: {
				for (int i = 0; i<diseaseLines.Count; i++) {
					GameObject line = diseaseLines[i].line;
					LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
					lineRenderer.material = blackLineMaterial;
				}

				diseasePathsParent.SetActive(true);
				doctorPathsParent.SetActive(false);
				break;
			}
		case Player.Doctor: {
				for (int i = 0; i<doctorLines.Count; i++) {
					GameObject line = doctorLines[i].line;
					LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
					lineRenderer.material = blackLineMaterial;
				}
				diseasePathsParent.SetActive(false);
				doctorPathsParent.SetActive(true);
				break;
			}
		}


	}

	GameObject CreateLineBetweenCities(City city1, City city2) {
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

    public void CheckForWin()
    {
        if (CheckIfWorldDead() || CheckIfDoctorsWon())
        {
            tm.ReportWinner();
            Reset();
        }
    }

    public void StartDoctorTurn()
    {
        //
    }

	public bool CureCity(GameObject city, int strainId)
    {
		City cityComponent = city.GetComponent<City>();
        
		bool foundDisease = false;

		for (int i = 0; i<cityComponent.diseaseList.Count; i++) {
			Disease disease = cityComponent.diseaseList[i];
			if (disease.StrainID == strainId) {
				disease.isCured = true;
				foundDisease = true;
			}
		}

		if (!foundDisease) {
			Disease newDisease = new Disease();
			newDisease.StrainID = strainId;
			newDisease.isCured = true;
			newDisease.name = "Strain " + strainId;
			cityComponent.diseaseList.Add(newDisease);
		}
      
        //        int i;
        //        bool found = false;
        //        int strainID = 2;
        //
        //        City tempCity = city.GetComponent<City>();
        //		tempCity.CureDisease(strainID);
        //
        //        for (i = 0; i < cityDirectory.Count; i++)
        //        {
        //            if (cityDirectory[i] == city)
        //            {
        //                found = true;
        //                break;
        //            }
        //        }
        //
        //        if (found)
        //        {
        //            tempCity.DecreaseOutbreakLevel();
        //            if (tempCity.outbreakLevel == 0)
        //            {
        //                city.GetComponent<Image>().sprite = cleanCity;
        //            }
        //        }
        return !foundDisease;
    }


    public bool InfectCity(GameObject city, int strainId)
    {
        int i;
        bool found = false;
        Disease strain = new Disease();
        strain.StrainID = strainId;
		strain.name = "Strain " + strainId;

        City tempCity = city.GetComponent<City>();
        if (!tempCity.AddDisease(strain))
        {
            return found;
        }

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
        return found;
    }

    public bool isAlive(City city)
    {
        if (city.outbreakLevel > outbreakLimit)
        {
            return false;
        }
        return true;
    }
    public bool CheckIfDoctorsWon()
    {
        return tm.CheckIfExceededTurnCount();
    }

    public bool CheckIfWorldDead()
    {
        int i;

        for (i = 0; i < cityDirectory.Count; i++)
        {
            if (cityDirectory[i].GetComponent<City>().outbreakLevel <= outbreakLimit)
            {
                return false;
            }
        }
        return true;
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
