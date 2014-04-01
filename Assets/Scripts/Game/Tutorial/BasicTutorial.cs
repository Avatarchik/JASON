using UnityEngine;
using System.Collections;
using SGUI;

public class BasicTutorial:Singleton<BasicTutorial>, ITutorial {
	public enum TutorialStage {
		None,
		Movement,
		Traps,
		BlockPickup,
		BlockPath,
		BlockDrop,
		PlayerTrigger,
		BlockTrigger,
		Key,
		KeyDoor
	};

	[SerializeField] private SGUITexture tooltip;

	[SerializeField] private SGUISlowWriteLabel[] labels;

	private TutorialStage stage = TutorialStage.None;

	private bool started;

	void Start() {
		tooltip.Create();

		foreach(SGUISlowWriteLabel label in labels)
			label.Create();

		StartTutorial();
	}

	public void StartTutorial() {
		stage = TutorialStage.Movement;

		started = true;

		StartStage();
	}

	public void StartStage() {
		tooltip.Activated = true;
		labels[(int)stage - 1].Activated = true;

		StartCoroutine(labels[(int)stage - 1].Write());
	}

	public void NextStage() {
		tooltip.Activated = false;
		labels[(int)stage - 1].Activated = false;

		stage++;
	}

	public void StopTutorial() {
		tooltip.Activated = false;
		labels[(int)stage - 1].Activated = false;

		started = false;
	}

	public SGUISlowWriteLabel[] Labels {
		get { return labels; }
	}

	public TutorialStage Stage {
		get { return stage; }
	}

	public bool Started {
		get { return started; }
	}
}
