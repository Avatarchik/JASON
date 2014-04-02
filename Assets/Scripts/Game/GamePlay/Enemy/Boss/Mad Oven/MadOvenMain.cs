using UnityEngine;
using System.Collections;

public class MadOvenMain:Boss {
	private MadOvenHamer hamer;
	private MadOvenSpatel spatel;

	private bool attacking;

	protected override void Start() {
		hamer = GameObject.Find("Hamer").GetComponent<MadOvenHamer>();
		spatel = GameObject.Find("Spatel").GetComponent<MadOvenSpatel>();
	}

	protected override void Update() {
		if(isDead && hamer.IsDead && spatel.IsDead)
			StartCoroutine("SwitchLevel");
	}

	public void StartAttack() {
		hamer.StartAttack();
		spatel.StartAttack();

		StartCoroutine(Attack());
	}

	void OnCollisionEnter(Collision collision) {
		if(!attacking)
			return;

		if(collider.gameObject.CompareTag("Player"))
			collider.GetComponent<Player>().Damage(data.AttackDamage, 0, false);
	}

	public override void Damage(int amount) {
		if(data.Health <= 0)
			return;

		data.Health -= amount;
		DisplayCombatText(amount.ToString(), Color.red, 0.7f);
	}

	private IEnumerator Attack() {
		while(true) {
			yield return new WaitForSeconds(data.AttackDelay / 2);

			attacking = true;

			yield return new WaitForSeconds(data.AttackDelay / 2);

			attacking = false;
		}
	}

	private IEnumerator SwitchLevel() {
		GameObject.Find("SGUI Manager").GetComponent<SGUIManager>().RemoveAll();

		yield return new WaitForSeconds(3);

		PlayerData.Instance.Reset();

		Application.LoadLevel("DoorScene");
	}
}
