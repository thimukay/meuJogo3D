using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public enum PlayerStates
    {
        IDLE,
        WALKING,
        JUMP,
    }

    public StateMachine<PlayerStates> stateMachine;

    public Animator _currentPlayer;
    public Rigidbody myRididBody;

    private void Start()
    {
        Init();
    }

    private void Awake()
    {
        if (_currentPlayer != null)
        {
           // _currentPlayer = Instantiate(_currentPlayer, transform);
        }
    }

    public void Init()
    {
        stateMachine = new StateMachine<PlayerStates>();

        stateMachine.Init();
        stateMachine.RegisterStates(PlayerStates.IDLE, new StateBase());
        stateMachine.RegisterStates(PlayerStates.WALKING, new StateBase());
        stateMachine.RegisterStates(PlayerStates.JUMP, new StateBase());

        stateMachine.SwitchState(PlayerStates.IDLE);
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    public void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jumping");
            myRididBody.velocity = Vector3.up * 5;

            stateMachine.SwitchState(PlayerStates.JUMP);
            _currentPlayer.SetBool("Idle", false);
            _currentPlayer.SetBool("Jumping", true);
        }
        else
        {
            Debug.Log("Idle");
            stateMachine.SwitchState(PlayerStates.IDLE);
            _currentPlayer.SetBool("Idle", true);
            _currentPlayer.SetBool("Jumping", false);
        }
    }

    public void HandleMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Walking");
            transform.Translate(transform.forward * 5 * Time.deltaTime);
            stateMachine.SwitchState(PlayerStates.WALKING);
            _currentPlayer.SetBool("Idle", false);
            _currentPlayer.SetBool("Walking", true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("Walking back");
            transform.Translate(transform.forward * -5 * Time.deltaTime);
            stateMachine.SwitchState(PlayerStates.WALKING);
            _currentPlayer.SetBool("Idle", false);
            _currentPlayer.SetBool("Walking", true);
        }
        else
        {
            Debug.Log("Idle");
            stateMachine.SwitchState(PlayerStates.IDLE);
            _currentPlayer.SetBool("Idle", true);
            _currentPlayer.SetBool("Walking", false);
        }
    }
}
