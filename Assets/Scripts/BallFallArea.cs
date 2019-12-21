using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFallArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.Instance.SetGameMode(GameMode.GM_GAMEOVER);
        }
    }
}
