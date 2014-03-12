using UnityEngine;
using System;
using System.Collections;

public class GUIDisplay:GUIBehaviour {
	[SerializeField] private Texture healthBorder;
	[SerializeField] private Texture healthBar;
	[SerializeField] private Texture damageBar;
	[SerializeField] private Texture defendingBar;
	[SerializeField] private Texture defendingIcon;
	private Texture currentBar;
	private Player player;
	
	private bool isOnDelay;
	private bool showing;

	private int timershit;
	private int barLength;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void FixedUpdate() {
		if(player.PlayerCombat.Defending){
			currentBar = defendingBar;
		}else if(player.hit){
			currentBar = damageBar;
		}else{
			currentBar = healthBar; 
		}
		barLength = 900 / player.PlayerData.maxHealth * player.PlayerData.Health;

		if(player.PlayerCombat.Attacking) {
			timershit -= 10;
		} else {
			timershit = 300;
		}
	}

	protected override void OnGUI() {
		base.OnGUI();

		GUI.depth = 1;
		base.OnGUI();
		if(player.PlayerCombat.TargetEnemy != null){
			DrawEnemyHealthDisplay();
		}

		DrawPlayerHealthDisplay();
		GUI.depth = 0;
	}

	void DrawEnemyHealthDisplay() {
		GUI.BeginGroup(new Rect(1020, 0, 900, 100));
			GUI.DrawTexture(new Rect(900, 0, -900, 100), healthBorder);
			GUI.DrawTexture(new Rect(900, 0, -900 / player.PlayerCombat.TargetEnemy.data.maxHealth * player.PlayerCombat.TargetEnemy.data.health, 100), healthBar);
		GUI.EndGroup();
	}

	void DrawPlayerHealthDisplay() {
		GUI.BeginGroup(new Rect(0, 0, 900, 100));
			GUI.DrawTexture(new Rect(0, 0, 900, 100), healthBorder);
			GUI.DrawTexture(new Rect(0, 0, barLength, 100), currentBar);
		if(player.PlayerCombat.Defending){
			GUI.DrawTexture(new Rect(barLength - 100, 0, 100, 100), defendingIcon);
		}
		GUI.EndGroup();
	}
}