using UnityEngine;
using System.Collections;

public class Powers : MonoBehaviour {
    // Stamina variable. Indicates what powers you can use.
    public int magic = 0;

    // Locate power buttons and prepare to get their animators.
    public GameObject blckButton;
    public GameObject atckButton;
    public GameObject healButton;
    public GameObject summButton;

    private Animator blckAni;
    private Animator atckAni;
    private Animator healAni;
    private Animator summAni;

	// Initialization.
	void Start ()
    {
        // Locate animators.
        blckAni = blckButton.GetComponent<Animator>();
        atckAni = atckButton.GetComponent<Animator>();
        healAni = healButton.GetComponent<Animator>();
        summAni = summButton.GetComponent<Animator>();
	}
	
	// Main.
	void Update ()
    {
        magic = this.GetComponent<magicStatus>().count;

        // Update animation stamina integer.
        blckAni.SetInteger("Stamina", magic);
        atckAni.SetInteger("Stamina", magic);
        healAni.SetInteger("Stamina", magic);
        summAni.SetInteger("Stamina", magic);
        // Update animation pressed boolean.
        blckAni.SetBool("Pressed", Input.GetKeyDown(KeyCode.Q));
        atckAni.SetBool("Pressed", Input.GetKeyDown(KeyCode.W));
        healAni.SetBool("Pressed", Input.GetKeyDown(KeyCode.E));
        summAni.SetBool("Pressed", Input.GetKeyDown(KeyCode.R));
    }
}
