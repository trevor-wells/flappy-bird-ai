using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapScript : MonoBehaviour
{
    public LogicScript logic;
    public MyLittleFlappyAgent bird;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("gap"))
            logic.AddScore();
    }
}
