using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int diamonds = 0;

    [SerializeField] private TextMeshProUGUI diamondsText;

    [SerializeField] private AudioSource collectSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Diamond")) 
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            diamonds++;
            diamondsText.text = "Diamonds: " + diamonds;
            // Debug.Log("Diamonds: " + diamonds);
        }
    }
}
