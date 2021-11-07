using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickups : MonoBehaviour
{
    [SerializeField] int amount = 100;
    [SerializeField] AudioClip coinpickupSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinpickupSFX, Camera.main.transform.position);
        Destroy(gameObject);
        FindObjectOfType<GameSession>().DispayScore(amount);
    }
}
