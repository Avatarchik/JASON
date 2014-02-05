using UnityEngine;
using System.Collections;

public class ItemList:MonoBehaviour {
	private static ItemList instance = null;

	public static ItemList Instance {
		get {
			if(instance == null) {
				instance = FindObjectOfType(typeof(ItemList)) as ItemList;
			}
			
			if(instance == null) {
				GameObject go = new GameObject("ItemList");
				instance = go.AddComponent(typeof(ItemList)) as ItemList;
			}
			
			return instance;
		}
	}
	
	void OnApplicationQuit() {
		instance = null;
	}

	[System.Serializable]
	public class List {
		public WeaponItem[] weaponList = new WeaponItem[0];
		public ShieldItem[] shieldList = new ShieldItem[0];

		public HelmetItem[] helmetList = new HelmetItem[0];
		public ArmorItem[] armorList = new ArmorItem[0];
		public LeggingsItem[] leggingsList = new LeggingsItem[0];
		public BootsItem[] bootsList = new BootsItem[0];

		public PowerItem[] powerList = new PowerItem[0];
		public SpecialItem[] specialList = new SpecialItem[0];

	}

	public List itemlist = new List();
}
