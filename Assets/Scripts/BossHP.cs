using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHP : MonoBehaviour {

    public float curr_health;
    public GameObject boss;
    public Image health;
    public Text healthBar;
    private float maxHealth = 100f;
    private bool green;
    private bool yellow;

	// Use this for initialization
	void Start () {
        curr_health = maxHealth;
        green = true;
        yellow = false;
	}

    // Update is called once per frame
    void Update() {
        if (healthBar != null) {
			getCurr ();
			healthBar.text = "Health " + curr_health.ToString();
            calcHealth();
            checkColor();

            if (green == true) {
                health.color = new Color32(98, 221, 54, 255);
            } else if (yellow == true) {
                health.color = new Color32(199, 194, 29, 255);
            } else {
                health.color = new Color32(199, 40, 29, 255);
            }
        }
    }

    public void checkColor() {
        if (curr_health >= maxHealth / 2) {
            green = true;
            yellow = false;
        } else if ((curr_health <= maxHealth / 2) && (curr_health > maxHealth / 4)) {
            green = false;
            yellow = true;
        } else {
            green = false;
            yellow = false;
        }
    }

    public void hurtBoss(float damage, float shakeLength)
    {
        curr_health -= damage;
        if (boss != null) {
            boss.GetComponent<ShakeObject>().Shake(shakeLength, .2f);
            if (curr_health <= 0f) {
                Destroy(boss);
                Destroy(health);
                Destroy(healthBar);
            }
        }
    }

    public void calcHealth() {
        float calcHealth = curr_health / maxHealth;

        health.fillAmount = calcHealth;
        checkColor();

    }
	public float getCurr() 
	{
		return curr_health;
	}
}
