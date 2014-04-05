using UnityEngine;
using System.Collections;

public enum TriggerActivator {
	Player,
	PushableBlock,
	Key,
	Brazier,
	Arrow,
	TimedArrow
}

public interface ITrigger {
	TriggerActivator GetTriggerActivator();
}
