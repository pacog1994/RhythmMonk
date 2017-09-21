using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour {

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Space))
		{
			startGame();
		}
	}

    public void startGame ()
    {
        Application.LoadLevel(1);
    }

}
