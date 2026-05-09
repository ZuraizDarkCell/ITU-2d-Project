using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectOther : MonoBehaviour
{
    public bool isPlayer = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlayer)
        {
            collision.GetComponent<HealthScript>().AttackRecieved(gameObject.name);
        }
        else if (collision.CompareTag("enemy") && isPlayer)
        {
            collision.GetComponent<HealthScript>().AttackRecieved(gameObject.name);
        }
    }
}
