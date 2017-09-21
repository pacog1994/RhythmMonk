using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class magicStatus : MonoBehaviour
{
    public Image Bar;
    public Text magic_txt;
    
    // Locate monk.
    public GameObject monk;

    public GameObject slashSprite;      // Slash effect for attack.
    private GameObject slashEffect;
    public GameObject blockSprite;      // Block effect for blocking.
    private GameObject blockEffect;
    public ParticleSystem healEffect;   // Heal effect for healing.
    public GameObject summonCircle;     // Summon circle for, well, summoning.

    public int count;

    private float maxMagic;
    private float currMagic;


    void Start() {
        currMagic = 0f;
        maxMagic = 50f;
        count = 0;
        Bar.fillAmount = 0;
    }

    // Fills the magic bar up.
    public void increaseMagic(float magicAmount)
    {
        // If filled, unfill and increase counter
        int combo = this.GetComponent<Combo>().combo;
        if (combo < 10)
            combo = 1;
        else if (combo < 20)
            combo = 2;
        else if (combo < 30)
            combo = 3;
        else
            combo = 5;
        currMagic += magicAmount * combo;

        if (currMagic >= maxMagic) {
            currMagic -= maxMagic;
            count++;
        }

        float calcHealth = currMagic / maxMagic;
        Bar.fillAmount = calcHealth;

        magic_txt.text = "Magic " + count.ToString();
    }



    // Update - Handle magic attacks.
    void Update()
    {
        Animator monkAni = monk.GetComponent<Animator>();
        monkAni.SetInteger("AttackType", 0);
        if (monkAni.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            if (count >= 1 && Input.GetKeyDown(KeyCode.Q))         {            // Block
                count -= 1;
                monkAni.SetInteger("AttackType", 3);
                blockEffect = Instantiate(blockSprite);
                StartCoroutine(DestroyTimer(blockEffect));
            }

            else if (count >= 2 && Input.GetKeyDown(KeyCode.W)) {       // Attack
                count -= 2;
                this.GetComponent<BossHP>().hurtBoss(10f, .4f);
                slashEffect = Instantiate(slashSprite);
                StartCoroutine(DestroyTimer(slashEffect));
                monkAni.SetInteger("AttackType", 2);
            }

            else if (count >= 5 && Input.GetKeyDown(KeyCode.E)) {       // Heal
                count -= 5;
                ParticleSystem healing = Instantiate(healEffect) as ParticleSystem;
                monkAni.SetInteger("AttackType", 1);
                this.GetComponent<healthStatus>().increaseHealth(40);
            }

            else if (count >= 10 && Input.GetKeyDown(KeyCode.R)) {      // Summon
                count -= 10;
                GameObject createdCircle = Instantiate(summonCircle) as GameObject;
                monkAni.SetInteger("AttackType", 1);
                this.GetComponent<BossHP>().hurtBoss(70f, 3f);
            }
        }
        magic_txt.text = "Magic " + count.ToString();
    }

    IEnumerator DestroyTimer(GameObject spriteToSlash)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(spriteToSlash);
    }

}
