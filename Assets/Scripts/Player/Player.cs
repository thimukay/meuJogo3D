using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour//, IDamageable
{
    public List<Collider> colliders;
    public Animator animator;
    
    
    public CharacterController characterController;
    public float speed = 1f; 
    public float turnSpeed = 1f; 
    public float gravity = 9.8f; 
    private float vSpeed = 0f;


    public float jumpSpeed = 15f;
    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Run Setup")] 
    public KeyCode keyRun = KeyCode.LeftShift; 
    public float speedRun = 1.5f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    public HealthBase healthBase;

    private bool _alive = true;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnDamage += OnKill;
    }


    #region LIFE
    private void OnKill(HealthBase h)
    {
        if (_alive && healthBase.currentLife >= 0)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);
        }
        
    }
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }

    #endregion

    void Update() 
    { 
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0); 
        var inputAxisVertical = Input.GetAxis("Vertical"); 
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded) 
        { 
            vSpeed = 0; 
            if (Input.GetKeyDown(jumpKeyCode)) 
            { 
                vSpeed = jumpSpeed;
                animator.SetBool("Jump", true);
            } else
            {
                animator.SetBool("Jump", false);
            }
        }

        vSpeed -= gravity * Time.deltaTime; 
        speedVector.y = vSpeed;


        var isWalking = inputAxisVertical != 0; 
        if (isWalking) { 
            if (Input.GetKey(keyRun)) { 
                speedVector *= speedRun; 
                animator.speed = speedRun; 
            } else { 
                animator.speed = 1; 
            } 
        }

        characterController.Move(speedVector * Time.deltaTime);


        animator.SetBool("Run", inputAxisVertical != 0);
    }

    
}
