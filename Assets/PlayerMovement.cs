using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    Rigidbody rb;
    public float margin;
    public float speed = 5f;
    public float jumpForce = 500f;
    public bool isJump = false;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = ReInput.players.GetPlayer(0);
    }
    private void Update()
    {
        Jump();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        float moveHorizontal = player.GetAxis("Move Horizontal");
        float movement = player.GetAxis("Move Horizontal") * speed;
        rb.velocity = new Vector2(movement, rb.velocity.y);
    }
    void Jump()
    {
        if (player.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
        if(rb.velocity.y<0){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y>0&&!player.GetButton("Jump")){
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    bool IsGrounded()
    {
        Vector3 v3a = new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z);
        Vector3 v3b = new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z);
        if (Physics.Raycast(v3a, -Vector3.up, margin) || Physics.Raycast(v3b, -Vector3.up, margin))
        {
            return true;
        }
        else
            return false;

    }
}
