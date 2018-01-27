using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {
    public List<City> connectingCities;
    public List<City> diseaseConnectingCities;
	public List<Disease> diseaseList;
    public List<Cure> cureList;
    public int outbreakLevel;
	public string name;
    
	// Use this for initialization
	void Start () {
        //connectingCities = new List<City>();
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
                if (diseaseList[i].StrainID == cureList[j].StrainID)
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
            if (cureList[i].StrainID == strainId)
            {
                result = true;
                break;
            }
        }
        
        return result;
    }
}
