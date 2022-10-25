using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public AudioSource musicSource;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;

    public float speed;
    public Text score;
    public Text lives;
    public GameObject winText;
    public GameObject loseText;
    private int scoreValue = 0;
    private int livesValue;

    

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score:" + scoreValue.ToString();
        
        livesValue = 3;
        SetLivesText();

        winText.SetActive(false);
        loseText.SetActive(false);

        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    
    }
    
    void SetLivesText()
    {
        lives.text = "Lives:" + livesValue.ToString();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        
        if(scoreValue >= 8)
        {
            winText.SetActive(true);  
        }
        if(livesValue <= 0)
        {
            loseText.SetActive(true);
            Destroy(gameObject);
        }
        
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if(scoreValue == 4)
            {
               livesValue = 3;
               SetLivesText(); 
            }

            if(scoreValue >= 8)
            {
                musicSource.clip = musicClipOne;
                musicSource.Stop();
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }
            
    
        }   

        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives:" + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        
        if(scoreValue == 4)
        {
            transform.position = new Vector2(63.56f, -0.26f);
        }
    }
    

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3),ForceMode2D.Impulse);
            }
        }
    }
}