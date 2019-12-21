using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Bonus, IPoolObject
{
    public ParticleSystem effect;
    public GameObject model;

    private const int score = 1;


    public override void OnTaken()
    {
        effect.Play();
        model.SetActive(false);
        GameManager.Instance.AddScore(score);
    }



    public void OnSpawn()
    {
        
    }



    public void OnDespawn()
    {
        model.SetActive(true);
    }
}
