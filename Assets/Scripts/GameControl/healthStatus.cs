using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthStatus : MonoBehaviour {

    public AudioSource gameMusic;
	public Image Bar;
	public Text healthTxt;
    public GameObject monk;
    public float currHealth;
    private float maxHealth = 100f;

	// Use this for initialization
	void Start () {
		currHealth = maxHealth;
	}


	public void decreaseHealth(float damage)
	{
        currHealth -= damage;
        float calcHealth = currHealth / maxHealth;
        Bar.fillAmount = calcHealth;
        monk.GetComponent<Animator>().SetBool("Hurt", true);

        if (currHealth == 0) {
            gameMusic.Stop();

            AudioSource defeatMusic = this.transform.GetChild(0).GetComponent<AudioSource>();
            defeatMusic.Play();
            Time.timeScale = 0f;
		}
	}

    public void increaseHealth(float heal)
    {
        currHealth += heal;
        if (currHealth > maxHealth)
            currHealth = maxHealth;
    }
	public float getCurr()
	{
		return currHealth;
	}

		
	// Update is called once per frame
	void Update () {
		getCurr (); 
		healthTxt.text = "Health " + currHealth.ToString();
        if (currHealth >= maxHealth / 2) {
			Bar.color = new Color32 (98, 221, 54, 255);
		} else if (currHealth > maxHealth / 4) {
			Bar.color = new Color32 (199, 194, 29, 255); 
		} else {
			Bar.color = new Color32 (199, 40, 29, 255);
		}

	}	
}
