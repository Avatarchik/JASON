using UnityEngine;
using System;

public class ItemPower:MonoBehaviour {
	public PowerData data;
}

[Serializable]
public class PowerData:Item {
	public enum PowerType {
		Sprint,
		Regeneration,
		Beserk,
		Thorn
	}
	
	public PowerType powerType;
	public int duration;
}