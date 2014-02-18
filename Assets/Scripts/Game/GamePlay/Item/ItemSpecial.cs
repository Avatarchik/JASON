using UnityEngine;
using System;

public class ItemSpecial:MonoBehaviour {
	public SpecialData data;
}

[Serializable]
public class SpecialData:Item {
	public int id;
}