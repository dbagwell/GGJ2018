using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityInfoPanel : MonoBehaviour {

	City city;
	public City City {
		get {
			return city;
		}
		set {
			city = value;
			cityNameText.text = city.name;
			PopulateStrainItems();
		}
	}

	public StrainItem strainItemPrefab;
	public City cityPrefab;

	public Text cityNameText;
	public GameObject strainItemList;

	public List<StrainItem> strainItems;

	// Use this for initialization
	void Start () {
		strainItems = new List<StrainItem>();


		// Test code
//		City city = Instantiate(cityPrefab, transform.position, transform.rotation);
//		Disease disease = new Disease();
//		disease.name = "Strain A";
//		city.AddDisease(disease);
//		city.name = "Regina";
//		City = city;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void PopulateStrainItems () {
		for (int i = 0; i<strainItemList.transform.childCount; i++) {
			GameObject.Destroy(strainItemList.transform.GetChild(i).gameObject);
		}

		for (int i = 0; i<city.diseaseList.Count; i++) {
			StrainItem newStrainItem = Instantiate(strainItemPrefab, transform.position, transform.rotation);
			newStrainItem.transform.parent = strainItemList.transform;
			newStrainItem.Disease = city.diseaseList[i];
			strainItems.Add(newStrainItem);
		}
	}
}
