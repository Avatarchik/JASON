using UnityEngine;
using System;

[Serializable]
public class ItemPower:Item {
	public static ItemPower power = new ItemPower();

	public enum PowerType {
		Sprint,
		Regeneration,
		Beserk,
		Thorn
	}

	[SerializeField] private PowerType powerType;
	[SerializeField] private int time;
	
	public ItemPower() { }

	public ItemPower(string itemName, PowerType powerType, int time):base(Item.ItemType.Power, itemName) {
		this.powerType = powerType;
		this.time = time;
	}

	public PowerType TypePower {
		set { powerType = value; }
		get { return powerType; }
	}

	public int Time {
		set { time = value; }
		get { return time; }
	}
}