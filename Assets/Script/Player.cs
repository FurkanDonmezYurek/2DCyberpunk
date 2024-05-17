using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement
    MoveObject moveObject;
    Rigidbody2D rb;
    public float speed;
    public float jump;
    public int character;
    
    //Dash
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    private TrailRenderer tr;

    void Start()
    {
        character = 1;
        rb = GetComponent<Rigidbody2D>();
        moveObject = GetComponent<MoveObject>();
        tr = this.gameObject.GetComponent<TrailRenderer>();
    }

    void Update()
    {
        //character transition
        if(Input.GetKeyDown(KeyCode.Q)){
            character --;
        }
        if(Input.GetKeyDown(KeyCode.E)){
            character ++;
        }
        //

        //Jump
        if(character == 1)
        {
        moveObject.Jump(jump);
        }
        //

        //for Dash
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            if(character == 1){
            StartCoroutine(Dash());

            }
        }
        //
    }
    private void FixedUpdate() {
        //for Movement
        switch(character){
            case <0:
            character = 2;
            break;
            case >2:
            character = 0;
            break;
            case 0:
            moveObject.HorizontalControls(speed/3);
            break;
            case 1:
            moveObject.HorizontalControls(speed);
            break;
            case 2:
            moveObject.HorizontalControls(speed/2);
            break;
        }
        //
        
    }
    private IEnumerator Dash()
    {
        int originalLayer = this.gameObject.layer;
        this.gameObject.layer = 9;
        float originalSpeed = speed;
        speed = 0;
        //canDamage = false;
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        moveObject.Dash(dashingPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        this.gameObject.layer = originalLayer;
        ResetAllForces();
        speed = originalSpeed;
        //canDamage = true;
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    public void ResetAllForces()
    {
        rb.velocity = Vector3.zero; // Hızı sıfırla
    }

}
