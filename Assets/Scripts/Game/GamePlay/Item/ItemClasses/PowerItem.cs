using UnityEngine;
using System.Collections;

[System.Serializable]
public class PowerItem : ItemBaseClass {
	public enum PowerType{
		Sprint,
		Regeneration,
		Beserk,
		Thorn
	};
}
