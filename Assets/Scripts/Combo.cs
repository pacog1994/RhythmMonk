using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Combo : MonoBehaviour {

	//private Color temp = new Color32 (204, 52, 127, 127);
	private float t = .1f;
	public float duration = 1;
	public int combo;
	private int amp;
	public Text combo_txt;
	public Text ampli_txt;


	// Update is called once per frame----------------------------------------------------------------------------------------
	void Update () {
		if (combo < 5) {
			ampli_txt.text = "";
		} else if (combo >= 5 && combo < 10) {
			ampli_txt.text = "x2";
		} else if (combo >= 10 && combo < 20) {
			ampli_txt.text = "x3";
		} else {
			ampli_txt.text = "x5";
			ampli_txt.color = new Color32 (255, 255, 255, 255);
		}
		combo_txt.text = "Combo " + combo.ToString ();
	}
}
