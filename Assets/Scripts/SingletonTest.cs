using UnityEngine;
using System.Collections;

public class SingletonTest : Singleton<SingletonTest> {
	public string I = "I am invincible";
}
