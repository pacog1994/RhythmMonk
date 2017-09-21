using UnityEngine;
using System.Collections;

public class ParticleOneShot : MonoBehaviour {

    private ParticleSystem ps;

    // Initialization
    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
