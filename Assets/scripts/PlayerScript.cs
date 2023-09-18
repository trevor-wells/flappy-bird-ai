using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float flapStrength;
    [SerializeField] private LogicScript logic;
    [SerializeField] private AudioSource flapSound;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource gameOverSound;
    public bool birdIsAlive = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && birdIsAlive)
        {
            logic.TogglePause();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) == true && !logic.isPaused && birdIsAlive)
        {
            rb.velocity = Vector2.up * flapStrength;
            flapSound.Play();
        }
        transform.eulerAngles = new Vector3(0, 0, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (birdIsAlive)
        {
            hitSound.Play();
            gameOverSound.Play();
        }
        logic.GameOver();
        birdIsAlive = false;
    }
}
