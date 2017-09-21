using UnityEngine;
using System.Collections;

public class stringShot : MonoBehaviour
{
    private byte alpha;
    private GameObject monk;
    private GameObject GM;

    public float stringSpeed;          // How many seconds before the string hits the monk.

    void Start()
    {
        GM = GameObject.Find("GM");
        monk = GameObject.Find("Monk");
        Vector3 thisPos = this.transform.position;
        Vector3 monkPos = monk.transform.position;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2((monkPos.x - thisPos.x) / stringSpeed, (monkPos.y - thisPos.y) / stringSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha <= 220)
            alpha += 25;
        else alpha = 255;

        this.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, alpha);
    }

    void OnTriggerStay2D(Collider2D touched)
    {
        if (monk.GetComponent<Animator>().GetBool("Hurt") == true){
            monk.GetComponent<Animator>().SetBool("Hurt", false);
            Destroy(this.gameObject);
        } else if (touched.gameObject.tag == "Player")
        {
            if (monk.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Block") == false) {
                GM.GetComponent<healthStatus>().decreaseHealth(20);
            }
        }
    }
}