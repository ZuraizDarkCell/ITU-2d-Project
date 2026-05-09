using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public void AttackRecieved(string instigator) 
    { 
        Debug.Log("Attack Recieved by " + instigator + $"to {gameObject.name}");
    }
}
