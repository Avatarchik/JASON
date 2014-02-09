using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class ItemList:MonoBehaviour {
	 public static List<Item> items = new List<Item>();
	 
	 void Start() {
	 	Debug.Log (items.Count);
	 }
}
