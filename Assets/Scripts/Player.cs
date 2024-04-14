using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public float speed;

    [Header("Arrow Attack")]
    public Arrow arrow;
    public float fireRate;

    float timeCount;


    Vector2 playerDirection;

    // Update is called once per frame
    void Update()
    {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));

        timeCount += Time.deltaTime;

        if (timeCount > fireRate)
        {
            float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Instantiate(arrow, transform.position, rotation);
            
            timeCount = 0;
        }
        if (playerDirection.x > 0)
            sprite.flipX = false;

        if (playerDirection.x < 0)
            sprite.flipX = true;

        if (playerDirection.sqrMagnitude > 0)
            animator.SetBool("walking", true);
        else
            animator.SetBool("walking", false);


    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + playerDirection.normalized * speed * Time.deltaTime);
    }
}
