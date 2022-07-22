using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionPartical;
    public ParticleSystem dirtPartical;
    private AudioSource playerAudio;
    public Text scoreText;
    public Text gameoverText;
    public Button newGameButton;
    public SpawnManager spawnManager;

    public int score = 0;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    //force applied on player and gravity of game
    public float forceApplied;
    public float gravityModifier;

    public bool playerIsOnGround = true;
    //if jump number is 2 than player can't jump anymore until he lands on ground...
    int jumpNumber = 0;

    public bool gameOver;

    public bool runFast = false;

    private float lerpSpeed = 2f;

    bool introOver = false;
    float introEndPos = 0.5f;
    float introStartPos;

    // Start is called before the first frame update
    void Start()
    {
        introStartPos = transform.position.x;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        // default gravity is 1 ..
        Physics.gravity *= gravityModifier;

        dirtPartical.Stop();
        playerAnim.SetFloat("Speed_f", 0.49f);

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsOnGround && !gameOver && !(jumpNumber == 2))
        {
            jumpNumber++;
            if(jumpNumber == 2)
                playerIsOnGround = false;

            playerRb.AddForce(Vector3.up * forceApplied, ForceMode.Impulse);

            // for jumping animation
            playerAnim.SetTrigger("Jump_trig");
            
            // stop dirt when jumping
            dirtPartical.Stop();

            //jump sound
            playerAudio.PlayOneShot(jumpSound);
        }

        //running fast key making....
        if(Input.GetKey(KeyCode.Mouse0))
        {
            runFast = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            runFast = false;
        }

        if (runFast && playerIsOnGround && !gameOver) {
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else
        {
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }

        //updating score
        updateScore();

        //intro Animation
        if(!introOver)
            introAnimation();

    }


    // space bar spam krke player ko aasman mein lejane se rokta hai ... neeche ka 2 functions...
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            playerIsOnGround = true;
            jumpNumber = 0;

            //play dirt when on ground...
            dirtPartical.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // for death animation when we collide with obstacle...
            int randDeathVal = Random.Range(1, 3);

            playerAnim.SetBool("Alive", false);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", randDeathVal);

            Debug.Log("Game Over");
            gameOver = true;
            gameoverText.gameObject.SetActive(true);
            newGameButton.gameObject.SetActive(true);
            

            //when game over explotion occurs...
            explosionPartical.Play();

            //stop dirt when game over
            dirtPartical.Stop();

            //crash sound on game over
            playerAudio.PlayOneShot(crashSound);
        }
    }
    
    
    void updateScore()
    {
        scoreText.text = "Score : " + score.ToString();
    }

    public void startNewGame()
    {
        score = 0;
        // moving player to start pos....
        transform.position = new Vector3(introStartPos, 0, 0);

        //destroying and removing all obstacals from list...
        spawnManager.removeAllObstacals();

        playerAnim.SetBool("Death_b", false);
        playerAnim.SetInteger("DeathType_int", 0);
        playerAnim.SetFloat("Speed_f", 0.49f);
        playerAnim.SetBool("Alive", true);

        introOver = false;

        gameoverText.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(false);
    }

    void introAnimation()
    {
        
        if (transform.position.x >= introEndPos)
        {
            playerAnim.SetFloat("Speed_f", 1.0f);
            introOver = true;
            gameOver = false;
            dirtPartical.Play();
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * lerpSpeed);
        }
    }


    
}
 