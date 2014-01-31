using UnityEngine;
using System.Collections;

public class PlayerCombat:MonoBehaviour {
	private Player player;
	
	void Start() {
		player = GetComponent<Player>();
	}

	public void Defend() {
		player.hitName = "Defend";
	}
	
	public void Attack() {
		player.hitName = "Attack";
	}
}
