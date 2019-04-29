using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	[SerializeField] string sceneToLoad;

	void Update () {
		if (Input.anyKey) {
			SceneManager.LoadScene (sceneToLoad);
		}
	}
}
