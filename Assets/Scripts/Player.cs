using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    
    bool win;

    public AudioSource musicSource;
    
    public AudioClip backgroundmusic;

    public AudioClip winner;

    public AudioClip loser;

    public AudioClip intro;
    
    public Text winText;

    public float timeLeft = 12.0f;

    public ParticleSystem burst;

    public Text losetext;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        musicSource.clip = backgroundmusic;
        musicSource.Play();
        PlaySound(intro);
    }

    // Update is called once per frame
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        timeLeft -= Time.deltaTime;

        if (win == true)
        { 
            timeLeft = 500;

            if (timeLeft == 500)
            {
                speed = 0;
                winText.text = "You Win!";
            }
        }

        if (timeLeft < 0)
        {  
          timeLeft = 500;
          PlaySound(loser);
        }  

        if (timeLeft <= 10)
        {
            speed = 3;
        }

        if (timeLeft >= 10)
        {
            speed = 0;
        } 
    }

    void FixedUpdate()
    {
         if (Input.GetKey("escape"))
        {
        Application.Quit();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);       
    }

    public void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.gameObject.CompareTag("nut"))
        {
            other.gameObject.SetActive (false);
            win = true;
            PlaySound(winner);
            ParticleSystem projectileObject = Instantiate(burst, rd2d.position + Vector2.up * 0.5f, Quaternion.identity);

        }
    }
}
