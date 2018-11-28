using UnityEngine;

public static class GameDebug {
	public static void Log <T> (T something) {
		Debug.Log(something.ToString());
	}
}
