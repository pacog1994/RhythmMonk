using UnityEngine;
using System.Collections;

public class CheckForPress : MonoBehaviour {
    public float bullsEye = -3.765f;          // Location where the center of the hit area is.
    public float hitRange = 0.365f;           // Range of the space where you can hit the note.
    public float killPos = -4.5f;             // Position to erase at.

    public ParticleSystem[] noteTypes = new ParticleSystem[3];     // Particle systems - okay, good, perfect.

    public KeyCode keyToPress;


    private GameObject statusObject;           // Object to get magic bar from.
    private ParticleSystem gotRight;           // Variable for the particle system to generate.


    void Start()
    {
        statusObject = GameObject.Find("GM");
    }

    // Check for input in the designated area, and destroy if it goes offscreen.
    void Update()
    {
        float range = Mathf.Abs(this.transform.position.x - bullsEye);
        int noteType;
        
        if ((range <= hitRange) && Input.GetKeyDown(keyToPress)) {
            if (range <= hitRange / 4) {        // Perfect note.
                noteType = 2;
            } else if (range <= hitRange / 2) { // Good note.
                noteType = 1;
            } else {                            // Okay note.
                noteType = 0;
            }
            ParticleSystem gotRight = Instantiate(noteTypes[noteType], this.transform.position, this.transform.rotation) as ParticleSystem;
            statusObject.GetComponent<Combo>().combo += 1;
            statusObject.GetComponent<magicStatus>().increaseMagic(noteType + 1);
            Destroy(this.gameObject);
        }

        else if (this.transform.position.x < killPos) {
            Destroy(this.gameObject);
            statusObject.GetComponent<Combo>().combo = 0;
        }
    }
}
