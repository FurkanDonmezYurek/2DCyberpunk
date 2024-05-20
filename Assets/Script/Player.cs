using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //GameOverScreen
    [SerializeField] private GameObject LifeBar;
    [SerializeField] private GameObject GameOverPanel;

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
    public float damageCD;
    public bool canDamage;
    float damageTime;
    

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
        canDamage = true;
    }

    void Update()
    {
        if (currentHealth == 0)
        {
            LifeBar.SetActive(false);

            if (GameOverPanel != null)
            {
                bool isActive = GameOverPanel.activeSelf;
                GameOverPanel.SetActive(!isActive);
            }


            if (GameOverPanel != null)
            {
                GameOverPanel.SetActive(true);
            }
        }
        //life bugfix
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
         UpdateLifeBar();

         if (canDamage == false)
        {
            damageTime = Time.time;
            if (damageCD < damageTime)
            {
                canDamage = true;
                damageTime = 0;
            }
        }
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && canDamage)
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
            moveObject.HorizontalControls(speed*1.5f);
            break;
            case 2:
            moveObject.HorizontalControls(speed/2);
            break;
        }
        //
        
    }
    public void TakeDamge(float damage)
    {
        if (canDamage == true)
        {
            currentHealth -= damage;
            animator.SetTrigger("TakeDamage");
            UpdateLifeBar();
            damageCD = Time.time + 0.5f;
            canDamage = false;
        }
    }
    private IEnumerator Dash()
    {
        int originalLayer = this.gameObject.layer;
        this.gameObject.layer = 9;
        float originalSpeed = speed;
        speed = 0;
        canDamage = false;
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
        canDamage = true;
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

    
    public void UpperCut(){
        rb.AddForce(Vector3.up * jump/2, ForceMode2D.Impulse);
    }
}
