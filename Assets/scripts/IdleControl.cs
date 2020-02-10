using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdleControl : MonoBehaviour
{
    public float horizontalSpeed;
    float speedX;
    public float verticalImpulse;
    Rigidbody2D rb;
    bool isGrounded;
    Animator anim;
    bool facingRight = true;
    public GameObject finishText;
    bool jumpAgain = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        anim.SetBool("isWalking", false);
        float translate = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.A))
        {
            speedX = -horizontalSpeed;
            anim.SetBool("isWalking", true);
            if (facingRight)
            {
                Flip();
            }
            facingRight = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            speedX = horizontalSpeed;
            anim.SetBool("isWalking", true);
            if (!facingRight)
            {
                Flip();
            }
            facingRight = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpAgain)
        {
            rb.AddForce(new Vector2(0, verticalImpulse), ForceMode2D.Impulse);
            StartCoroutine(AllowJump());
        }
        transform.Translate(speedX, 0, 0);
        speedX = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isGrounded", true);
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("Level1");
        }
        if (collision.gameObject.tag == "Finish")
        {
            finishText.SetActive(true);
            StartCoroutine(Waiting(1));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            anim.SetBool("isGrounded", false);
        }
    }

    void Flip()
    {
        
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator Waiting(int seconds)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    IEnumerator AllowJump()
    {
        jumpAgain = false;
        yield return new WaitForSeconds(0.5f);
        jumpAgain = true;
    }
}