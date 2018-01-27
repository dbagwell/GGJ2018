using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disease : MonoBehaviour {
    int strainID;
    public int StrainID
    {
        get { return strainID; }
        set { strainID = value; }
    }

	public string name;
	public bool isCured;

    // Use this for initialization
    void Start () {
		isCured = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
