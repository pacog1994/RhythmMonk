using UnityEngine;
using System.Collections;
using SynchronizerData;

public class Generator : MonoBehaviour {
    public GameObject[] keyTypes = new GameObject[5];   // List of the five keys, in the order of the rows.
    
    private int currentStep = 0;        // Keep track of the step/beat number.
    private int currentNote = 0;        // Keep track of which note to check for next.

    private BeatType repeatCheck;       // Prevent duplicates during the beat window.
    private BeatObserver onBeat;        // Direct to the beatobserver.

    private float[] keyPositions = new float[] {    // Spawn Y locations for each button type.
        -1.07f, -1.70f, -2.34f, -2.99f, -3.59f
    };

    private int[] noteData = new int[] {            // Table of what steps to spawn a note on.
        6, 7, 8, 9, 10, 11, 12, 13
    };




    // Initialization - get the BeatObserver component.
    void Start() {
        onBeat = this.GetComponent<BeatObserver>();
    }
	
	// Main - Check if time to spawn a note, and make one if so.
	void Update() {
        if ((onBeat.beatMask != 0) && onBeat.beatMask != repeatCheck) {
            currentStep++;
            if (currentStep >= 6)
                SpawnNewNote();

            //while (noteData[currentNote] == currentStep)
            //{
            //    currentNote++;
            //}
        }
        repeatCheck = onBeat.beatMask;
    }

    // Routine to actually create the note.
    private void SpawnNewNote() {
        int newKey = Random.Range(0, 5);
        GameObject spawnedSprite = Instantiate(keyTypes[newKey], new Vector3(5.35f, keyPositions[newKey], 0), this.transform.rotation) as GameObject;
        spawnedSprite.GetComponent<Rigidbody2D>().velocity = new Vector2(-4.5575f, 0f);      // Send to the left.
    }
}
