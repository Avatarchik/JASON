using UnityEngine;
using System;
using System.Collections;

public class GUIDisplay:Singleton<GUIDisplay> {
	[SerializeField] private Texture healthBorder;
	[SerializeField] private Texture healthBar;
	
	private Player player;
	
	private bool isOnDelay;
	private bool showing;

	private int timershit;
	private int barLength;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void FixedUpdate() {
		barLength = 300 / player.PlayerData.maxHealth * player.PlayerData.Health;

		if(player.PlayerCombat.Attacking) {
			timershit -= 10;
		} else {
			timershit = 300;
		}
	}

	void OnGUI() {
		if(player.PlayerCombat.TargetEnemy != null)
			DrawEnemyHealthDisplay();

		DrawPlayerHealthDisplay();
	}

	void DrawEnemyHealthDisplay() {
		GUI.BeginGroup(new Rect(400, 0, 300, 25));
			GUI.DrawTexture(new Rect(0, 0, 300 / player.PlayerCombat.TargetEnemy.data.maxHealth * player.PlayerCombat.TargetEnemy.data.health, 25), healthBar);
			GUI.DrawTexture(new Rect(0, 0, 300, 25), healthBorder);
		GUI.EndGroup();
	}

	void DrawPlayerHealthDisplay() {
		GUI.BeginGroup(new Rect(0, 0, 300, 25));
			GUI.DrawTexture(new Rect(0, 0, barLength, 25), healthBar);
			GUI.DrawTexture(new Rect(0, 0, 300, 25), healthBorder);
		GUI.EndGroup();
	}
}