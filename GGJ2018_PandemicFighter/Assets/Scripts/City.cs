using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {
    public List<City> connectingCities;
    public List<City> diseaseConnectingCities;
	public List<Disease> diseaseList;
    public int outbreakLevel;
	public string name;


	public List<GameObject> diseaseLines = new List<GameObject>();
	public List<GameObject> doctorlLines = new List<GameObject>();
    
	// Use this for initialization
	void Start () {
        //connectingCities = new List<City>();
        diseaseList = new List<Disease>();
        outbreakLevel = 0;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool DoIHaveAllCures()
    {
        for (int i = 0; i < diseaseList.Count; i++)
        {
			if (!diseaseList[i].isCured) {
				return false;
			}
        }

        return true;
    }

    public void IncreaseOutbreakLevel()
    {
        if (diseaseList.Count > 0)
        {
            outbreakLevel++;
        }
    }

    public void DecreaseOutbreakLevel()
    {
        if (outbreakLevel > 0)
        {
            outbreakLevel--;
        }
    }

    public int GetOutbreakLevel()
    {
        return outbreakLevel;
    }
    
    public void AddCity(City newCity)
    {
        connectingCities.Add(newCity);

    }

    public void AddDisease(Disease newStrain)
    {
        diseaseList.Add(newStrain);
    }

    public void CureDisease(int strainId)
    {
		int i;

		for (i=0;i<diseaseList.Count;i++)
		{
			if (diseaseList[i].StrainID == strainId)
			{
				diseaseList[i].isCured = true;
			}
		}
    }

    public bool CheckForCure(int strainId)
    {
        int i;

        for (i=0;i<diseaseList.Count;i++)
        {
			if (diseaseList[i].StrainID == strainId && diseaseList[i].isCured)
            {
				return true;
            }
        }
        
        return false;
    }
}
