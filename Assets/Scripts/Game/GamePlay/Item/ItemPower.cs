using UnityEngine;

public class ItemPower:Item {
	public enum PowerType {
		Sprint,
		Regeneration,
		Beserk,
		Thorn
	}

	public PowerType powerType;
	public int buffTime;

	public ItemPower(ItemPower.PowerType powerType, int buffTime) {
		this.powerType = powerType;
		this.buffTime = buffTime;
	}
}