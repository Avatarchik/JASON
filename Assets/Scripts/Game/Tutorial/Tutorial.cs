using System.Collections;

public interface Tutorial {
	void StartTutorial();

	void StartStage();

	void NextStage();

	void StopTutorial();

	IEnumerator NextStageOnFinish();
}
