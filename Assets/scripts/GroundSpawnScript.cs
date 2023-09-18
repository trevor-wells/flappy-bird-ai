using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawnScript : MonoBehaviour
{
    public GameObject ground;
    public float spawnRate = 2;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGround();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
            timer += Time.deltaTime;
        else
        {
            SpawnGround();
            timer = 0;
        }
    }

    void SpawnGround()
    {
        Instantiate(ground, new Vector2(transform.position.x, transform.position.y), transform.rotation);
    }
}
