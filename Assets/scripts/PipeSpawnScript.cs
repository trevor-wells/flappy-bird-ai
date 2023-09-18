using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 2;
    public float timer = 0;
    public float highestOffset = 10;
    public List<GameObject> pipeList;
    public int pipeCount = 0;
    public float birdX = -4.1f;
    public float nextPipePosX;
    public float nextGapPosY;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
            timer += Time.deltaTime;
        else
        {
            SpawnPipe();
            timer = 0;
        }
        nextPipePosX = GetNextPipe().transform.position.x;
        nextGapPosY = GetNextPipe().transform.position.y;
    }

    void SpawnPipe()
    {
        float lowestPoint = transform.localPosition.y - highestOffset;
        float highestPoint = transform.localPosition.y + highestOffset;
 
        GameObject newPipe = Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        pipeList.Add(newPipe);
        pipeCount++;
    }

    public GameObject GetNextPipe() {
        for (int i = 0; i < pipeList.Count; i++) {
            GameObject pipe = pipeList[i];
            if (pipe != null && pipe.transform.position.x > -4.1f)
            {
                return pipe;
            }
        }
        return null;
    }
}
