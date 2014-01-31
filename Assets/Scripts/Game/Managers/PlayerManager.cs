using UnityEngine;
using System.Collections;

public class PlayerManager:Singleton<PlayerManager> {
	[SerializeField] private bool isDead;
	
	public bool Dead {
		set { isDead = value; }
		get { return isDead; }
	}
}
