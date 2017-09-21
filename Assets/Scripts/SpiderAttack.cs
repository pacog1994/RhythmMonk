using UnityEngine;
using System.Collections;
using SynchronizerData;

// Script to handle the spider's attack.

public class SpiderAttack : MonoBehaviour {

    public int minBeat;              // Min number of beats between attacks.
    public int maxBeat;              // Max number of beats between attacks.
    public float beatWait = 50;      // Initial wait time for the first attack.

    public GameObject stringSprite;  // Object to use for the attack.

    private BeatObserver onBeat;
    private BeatType repeatCheck;
    private Color32 spiderColor;

    void Start () {
        onBeat = this.GetComponent<BeatObserver>();
        spiderColor = this.GetComponent<SpriteRenderer>().color;
    }
	

	void Update () {
        // If on the beat, decrement the wait time.
        if ((onBeat.beatMask != 0) && (onBeat.beatMask != repeatCheck)) {
            beatWait--;
        }
        repeatCheck = onBeat.beatMask;

        // Make the spider flash red when it's waiting to attack.
        this.GetComponent<SpriteRenderer>().color = spiderColor;
        if ((beatWait <= 6) && (beatWait != 0) && (beatWait % 2 == 0)) {
            this.GetComponent<SpriteRenderer>().color = new Color32(208, 144, 144, 255);
        }

        // Attack if it's time to do so.
        if (beatWait <= 0) {
            GameObject stringShot = Instantiate(stringSprite) as GameObject;
            beatWait = Random.Range(minBeat, maxBeat);
        }
    }
}
