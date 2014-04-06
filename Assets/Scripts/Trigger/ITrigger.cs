using UnityEngine;
using System.Collections;

public enum TriggerActivator {
	Player,
	PushableBlock,
	Brazier,
	Arrow,
	TimedArrow
}

public interface ITrigger {
	/** <returns>The activator of this trigger</returns> */
	TriggerActivator GetTriggerActivator();
}
