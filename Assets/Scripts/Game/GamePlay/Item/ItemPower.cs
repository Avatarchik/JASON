using UnityEngine;

public class ItemPower:Item {
	public enum PowerType {
		Sprint,
		Regeneration,
		Beserk,
		Thorn
	}

	public PowerType powerType;
	public int time;
	
	public ItemPower() { }

	public ItemPower(string itemName, PowerType powerType, int time):base(Item.ItemType.Power, itemName) {
		this.powerType = powerType;
		this.time = time;
	}
}