using UnityEngine;
using System.Collections;

public class RotatePentacle : MonoBehaviour {
    public float animationLength = 3;       // How long to wait before shrinking the pentacle.

    private float rotateAngle = 0;          // Angle to rotate the pentacle.
    private float timer = 0;

    void Update () {
        rotateAngle++;
        timer += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
        

        if ((transform.localScale.x < 1) && (timer < animationLength)) {
            transform.localScale += new Vector3(0.05f, 0.05f, 0);
        } else if (timer > animationLength) {
            transform.localScale -= new Vector3(0.05f, 0.05f, 0);
            if (transform.localScale.x <= 0){
                Destroy(this.gameObject);
            }
        }
    }
}
