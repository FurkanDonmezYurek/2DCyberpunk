using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //Animation
    Animator animator;
    AttackSystem attackSystem;

    //Health Bar
    public Slider healthSlider;
    public float maxHealth;
    public float currentHealth;
    

    void Start()
    {
        character = 1;
        rb = GetComponent<Rigidbody2D>();
        moveObject = GetComponent<MoveObject>();
        tr = this.gameObject.GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>(); 
        attackSystem = GetComponentInChildren<AttackSystem>();
        
        
        //LifeBar start full
        currentHealth = maxHealth;
        UpdateLifeBar();
    }

    void Update()
    {
        //life bugfix
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
         UpdateLifeBar();
        //character transition
        if(moveObject.jumping == false)
        {
        if(Input.GetAxis("Mouse ScrollWheel")<0&&character !=2){
            character = 2;
            animator.SetTrigger("isTransition");
            
        }
        if(Input.GetAxis("Mouse ScrollWheel")>0&& character !=1){
            character = 1;
            animator.SetTrigger("isTransition");
            
        }
        if(Input.GetKey(KeyCode.Q)&& character !=0){
            character = 0;
            animator.SetTrigger("isTransition");
        }
        }
        animator.SetInteger("Character",character);
        //

        //Flip
        moveObject.GameObjectFlip();
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

         //Jump
        if(character == 1)
        {
        moveObject.Jump(jump);
        }
        
        //
        if(moveObject.jumping == true){
            animator.SetBool("Jump",true);
        }else{
            animator.SetBool("Jump",false);
        }
    }
    private void FixedUpdate() {
       
        //for Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        
        switch(character){
            case <0:
            character = 2;
            break;
            case >2:
            character = 1;
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
        animator.SetBool("isDashing", true);
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
        animator.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    public void ResetAllForces()
    {
        rb.velocity = Vector3.zero; // Hızı sıfırla
    }

    void Shoot(){
        GameObject bulletClone = Instantiate(
                                attackSystem.bullet,
                                attackSystem.firePosition.position,
                                Quaternion.identity
                            );
        bulletClone.GetComponent<Rigidbody2D>().velocity = attackSystem.firePosition.right * attackSystem.fireSpeed;
    }

    private void UpdateLifeBar()
    {
        healthSlider.value = currentHealth / maxHealth;
    }
}
