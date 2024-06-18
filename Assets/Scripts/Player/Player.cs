using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Cloth;

public class Player : Singleton<Player>//, IDamageable
{
    public List<Collider> colliders;
    public Animator animator;

    public PlayerAbilityShoot pAs;
    
    
    public CharacterController characterController;
    public float speed = 10f; 
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

    [Header("Life")]
    public HealthBase healthBase;

    [Space]
    [SerializeField]private ClothChanger _clothChanger;
    [SerializeField]private ClothSetup _clothSetup;

    private bool _alive = true;
    private bool _jumping = false;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
        if (pAs == null) pAs = GetComponent<PlayerAbilityShoot>();
    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();

        _clothSetup = ClothManager.Instance.GetSetupByType(ClothType.COMMON);

        if (!SaveManager.Instance.Setup.LevelEnded)
        {
            transform.position = SaveManager.Instance.Setup.checkpoint;
            ChangeTexture(SaveManager.Instance.Setup.clothSetup, .2f);
        }

        if (!SaveManager.Instance.Setup.gameStarted) _clothChanger.ChangeTexture();
        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    #region LIFE
    private void OnKill(HealthBase h)
    {
        if (_alive && healthBase.getCurrentLife() <= 0f)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);
            pAs.enabled = false;
            Invoke(nameof(Revive), 3f);
        }
        
    }

    private void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), .1f);
        EffectsManager.Instance.ResetVignette();
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
        pAs.enabled = true;
    }

    public void Damage(HealthBase h)
    {
        //flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
        ShakeCamera.Instance.Shake();
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

            if (_jumping)
            {
                _jumping = false;
                animator.SetTrigger("Land");
            }
            vSpeed = 0; 
            if (Input.GetKeyDown(jumpKeyCode)) 
            { 
                vSpeed = jumpSpeed;

                if (!_jumping)
                {
                    _jumping = true;
                    animator.SetTrigger("Jump");
                }
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

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            //_clothChanger.ChangeTexture();
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        this.speed = speed;
        //StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }
    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public ClothSetup getCloth()
    {
        return _clothSetup;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        _clothSetup = setup;
        _clothChanger.ChangeTexture(setup);
        //StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }
    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    }

}
