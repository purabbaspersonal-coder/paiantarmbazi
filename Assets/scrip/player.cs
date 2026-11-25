using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class player : MonoBehaviour
{
    public int speed;
    public int jump;
    Rigidbody2D myrig;
    Animator myanim;
    public int coins;
    public GameObject gameOverText;
    public GameObject winText;
    public int bulletCount = 3;
    bool facingRight = true;
    public TMP_Text bulletText;


    public static bool gameEnded = false;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public bool onSafePlatform = false;
    // Start is called before the first frame update
    void Start()
    {
        myrig = GetComponent<Rigidbody2D>();
        myanim = GetComponent<Animator>();
        bulletText.text = bulletCount.ToString();

        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }

        if (transform.position.y < -8)
        {
            GameOver();
        }
        if (isDead)
            return;

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            transform.localScale = new Vector3(Mathf.Abs( transform.localScale.x), transform.localScale.y, transform.localScale.z);
            facingRight = true;
            myanim.SetBool("Run", true);
        
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            facingRight = false;
            myanim.SetBool("Run", true);

        }
        else
        {
            myanim.SetBool("Run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            myrig.velocity = new Vector2(myrig.velocity.x, jump);
           
        }
        if (myrig.velocity.y > 0.1f)
        {
            myanim.SetBool("jump", true);
            myanim.SetBool("Downjump", false);
        }
        else if (myrig.velocity.y < -0.1f)
        {
            myanim.SetBool("jump", false);
            myanim.SetBool("Downjump", true);
        }
        else
        {
            myanim.SetBool("jump", false);
            myanim.SetBool("Downjump", false);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            myanim.SetBool("Downjump", true);
        }
    }
    bool isDead = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("stone"))
        {
            myanim.SetTrigger("Die");
            StartCoroutine(ShowGameOver());
        }

        if (collision.gameObject.CompareTag("SafePlatform"))
        {
            onSafePlatform = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            WinGame();
        }

        
    }
    IEnumerator ShowGameOver()
    {
        isDead = true;

        yield return new WaitForSeconds(1f);

        gameOverText.SetActive(true);
    }
    public void GameOver()
    {
        myanim.SetTrigger("Die");
        isDead = true;
        myrig.velocity = Vector2.zero;
        myrig.simulated = false;
        gameOverText.SetActive(true);

        gameEnded = true;

    }

    IEnumerator NextLevel()
    {
        winText.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void WinGame()
    {
        isDead = true;

        StartCoroutine(NextLevel());
    }

    void Shoot()
    {
        if (bulletCount <= 0)
            return;

        bulletCount--;
        bulletText.text = bulletCount.ToString();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (facingRight)
        {
            rb.velocity = new Vector2(12f, 0);
        }
        else
        {
            rb.velocity = new Vector2(-12f, 0);

            bullet.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

  

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SafePlatform"))
        {
            onSafePlatform = false;
        }
    }
}
