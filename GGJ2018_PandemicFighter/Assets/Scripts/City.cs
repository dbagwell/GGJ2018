using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {
    public List<City> connectingCities;
    List<Disease> diseaseList;
    List<Cure> cureList;
    public int outbreakLevel;
	// Use this for initialization
	void Start () {
        connectingCities = new List<City>();
        diseaseList = new List<Disease>();
        cureList = new List<Cure>();
        outbreakLevel = 0;		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool DoIHaveAllCures()
    {
        int i;
        int j;
        bool result = false;
        for (i = 0; i < diseaseList.Count; i++)
        {
            result = false;
            for (j = 0; j < cureList.Count; j++)
            {
                if (diseaseList[i].strainID == cureList[j].strainID)
                {
                    result = true;
                    break;
                }
            }
            if (result == false)
            {
                break;
            }
        }

        return result;
    }
    public void IncreaseOutbreakLevel()
    {
        outbreakLevel++;
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

    public void AddCure(Cure newCure)
    {
        cureList.Add(newCure);
    }

    public bool CheckForCure(int strainId)
    {
        bool result = false;
        int i;

        for (i=0;i<cureList.Count;i++)
        {
            if (cureList[i].strainID == strainId)
            {
                result = true;
                break;
            }
        }
        
        return result;
    }
}
