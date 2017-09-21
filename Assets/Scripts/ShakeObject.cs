using UnityEngine;
using System.Collections;

public class ShakeObject : MonoBehaviour {

    // Shake parameters: float shakeLength, float shakeAmount
    private float shakeAmount = 0;      // How fast it should shake.
    private float shake = 0;            // How long it should shake.

    private Vector3 originalPos;        // Backup of the sprite's position at the start of the space.

    void Start() {
        originalPos = this.transform.position;
    }

	// Main code.
	void Update () {
        if (shake >= 0) {
            this.transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime;
        }
        else
            this.transform.position = originalPos;
    }

    // Call this method to start the shaking effect.
    public void Shake(float length, float amount) {
        shake = length;
        shakeAmount = amount;
    }
}
