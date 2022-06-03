using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    //Scores
    public static int SCORE;
    public static int highScore;

    //Text to active when player collide with obstacle
    public GameObject gameOver;

    //Speed controll
    public float startSpeed = 2f;
    public float acceleration = 0.1f;
    public float maxAcceleration = 5.5f;


    //Used to set amplifier
    public float slowingRestoreSpeed = 0.15f;
    public float setAmplifierSpeedOnTrigger = 0.6f;

    //Width and Height of the screen in Unity units
    public float width = 6;
    public float height = 10;

    //When to spawn Slow
    public int spawnCoinAfter = 40;

    //Prefabs to instantiate
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;

    
    private int previousDistance;
    private int startPos;

    //Uses to slowing down the speed for a while
    private float amplifier;

    private bool isDead = false;

    SpriteRenderer spriteRenderer;
    public ParticleSystem particleSys;
    public TrailRenderer trailRenderrer;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPos = (int)transform.position.y;
        SCORE = 0;

        amplifier = 1f;

        //Prevent display from sleeping
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

	}


    void Update() {

        //Restart game when player is dead and user taped on screen
        if (isDead) {
            if (Input.touchCount > 0)
            {
                SceneManager.LoadScene("MainScene");
            }
            return;
    }


        //Rotating player by accelerometer
        transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(Input.acceleration.x * 90, -50f, 50f));



        //Distance and setting score
        int distanceY = (int) Mathf.Abs(transform.position.y - startPos);
        SCORE = distanceY;


        //Spawning obstacles

        if (distanceY > previousDistance) {
            Vector3 spawnPos = new Vector3(Random.Range(-width/2, width/2), Random.Range(-1f,1f) - distanceY - height);

            

            previousDistance = distanceY;

            //Spawn coin or obstacle
            if (distanceY % spawnCoinAfter == 0)
            {
                Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            }
            else {
                Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
            }
        }

        //Check the amplifier. If is lower than 1f increase it;
        if (amplifier < 1) {
            amplifier += slowingRestoreSpeed * Time.deltaTime;
            if (amplifier > 1)
            {
                amplifier = 1;
            }
        }

        //Setting speed and moving player
        float accelerationSpeed = (distanceY+1) * acceleration;
        accelerationSpeed = Mathf.Sqrt(accelerationSpeed);
        if (accelerationSpeed > maxAcceleration)
            accelerationSpeed = maxAcceleration;
        transform.position -= transform.up * Time.deltaTime * (startSpeed+accelerationSpeed) * amplifier;
    }

    //When collide with obstacle
    private void OnCollisionEnter2D(Collision2D collision)
    {

        isDead = true;

        

        //Disable Rendering
        spriteRenderer.enabled = false;

        //Disable Trail
        trailRenderrer.enabled = false;

        //Start Particle System
        particleSys.Play();

        //Enable GameOverText
        gameOver.SetActive(true);

        //Set highScore
        if (SCORE > highScore) {
            highScore = SCORE;
        }

       

    }

    //When trigger with somethning
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Slow")){
            amplifier = setAmplifierSpeedOnTrigger;
            Destroy(collision.gameObject);
        }
    }

}
