using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MyLittleFlappyAgent : Agent
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float flapStrength;
    [SerializeField] private LogicScript logic;
    public bool birdIsAlive = true;
    private bool spacePressed = false;
    public GameObject pipeSpawner;
    private GameObject mySpawner;
    private PipeSpawnScript pipeScript;
    public GameObject groundSpawner;
    const float worldHeight = 20f;
    public float pipeSpawnXPos = 9f;

    public override void OnEpisodeBegin()
    {
        Instantiate(groundSpawner, new Vector2(14.2f, -9.95f), groundSpawner.transform.rotation);
        mySpawner = Instantiate(pipeSpawner, new Vector2(9f, 9.5f), pipeSpawner.transform.rotation);
        pipeScript = mySpawner.GetComponent<PipeSpawnScript>();
        transform.localPosition = new Vector2(-2.5f, 2.5f);
        rb.velocity = Vector2.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //we must normalize all observations so all values fall between -1 and 1. this improves the efficiency of the machine learning algorithm

        // need vertical position of the bird
        float vertPos = (transform.position.y - 2.5f) / (worldHeight/2);
        sensor.AddObservation(vertPos);

        //need vertical velocity of the bird
        float vertVel = rb.velocity.y / (worldHeight *2f);
        sensor.AddObservation(vertVel);
        
        //need X position of the next pipe
        float nextPipePosX = (pipeScript.nextPipePosX / pipeSpawnXPos);
        if (nextPipePosX != 0f)
            sensor.AddObservation(nextPipePosX);
        else
            sensor.AddObservation(1f);

        //need Y position of the next gap
        float nextGapPosY = ((pipeScript.nextGapPosY - 9.5f) / (worldHeight/2));
        sensor.AddObservation(nextGapPosY);

        //need to know what the last action was
        sensor.AddObservation(spacePressed ? 1f : -1f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        transform.eulerAngles = new Vector3(0, 0, rb.velocity.y);
        int flap = actions.DiscreteActions[0];
        if (flap == 0)
            spacePressed = false;
        if (flap == 1 && !spacePressed)
        {
            rb.velocity = Vector2.up * flapStrength;
            spacePressed = true;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("gap"))
            SetReward(1f);
        else if (collider.CompareTag("pipe") || collider.CompareTag("ground") || collider.CompareTag("top"))
        {
            SetReward(-0.1f);
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("pipe"))
                Destroy(o);
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("ground"))
                Destroy(o);
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("gap"))
                Destroy(o);
            logic.playerScore = 0;
            logic.onesPlaceBig.transform.localPosition = new Vector3(-1.72f, 9.5f, 0f);
            logic.tensPlaceBig.transform.localPosition = new Vector3(-2.37f, 9.5f, 0f);
            logic.hundredsPlaceBig.transform.localPosition = new Vector3(-3.02f, 9.5f, 0f);
            pipeScript.pipeCount = 0;
            pipeScript.pipeList.Clear();
            EndEpisode();
        }
    }
}
