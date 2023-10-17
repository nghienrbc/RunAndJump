using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private GameObject playerSprite;
    private Rigidbody2D playerRigidBody2D;
    private float movePlayerVector;
    private bool facingRight;
    public float speed = 4.0f;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        playerSprite = transform.Find("PlayerSprite").gameObject;
        anim = (Animator)playerSprite.GetComponent(typeof(Animator));
    }

    // Update is called once per frame
    void Update()
    {
        movePlayerVector = Input.GetAxis("Horizontal");
        playerRigidBody2D.velocity = new Vector2(movePlayerVector * speed, playerRigidBody2D.velocity.y);
        if (movePlayerVector > 0 && !facingRight)
        {
            Flip();
        }
        else if (movePlayerVector < 0 && facingRight)
        {
            Flip();
        }
        anim.SetFloat("speed", Mathf.Abs(movePlayerVector));
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = playerSprite.transform.localScale;
        theScale.x *= -1;
        playerSprite.transform.localScale = theScale;
    }
}
