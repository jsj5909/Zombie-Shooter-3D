using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState {IDLE, CHASE, ATTACK }
    
    private CharacterController _controller;

    private Player _player;

    Vector3 _velocity;

    [SerializeField]
    private float _speed = 2;

    [SerializeField]
    private float _gravity = 1;

    [SerializeField]
    private float _attackDelay = 1.5f;

    [SerializeField]
    private EnemyState _currentState = EnemyState.CHASE;

    private Health _playerHealth;

    private float _nextAttack = -1;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        _player = GameObject.Find("Player").GetComponent<Player>();

        _playerHealth = _player.GetComponent<Health>();

        _velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        switch(_currentState)
        {
            case EnemyState.CHASE:
                CalculateMovement();
                    break;

            case EnemyState.ATTACK:
                    Attack();
                    break;

                    
            default:
                break;

        }

    }

    private void Attack()
    {
        if (Time.time > _nextAttack)
        {
            if (_playerHealth != null)
                _playerHealth.Damage(10);
            _nextAttack = Time.time + _attackDelay;
        }
    }

    void CalculateMovement()
    {
        //check if grounded
        //move (calculate direction,



        if (_controller.isGrounded)
        {
            Vector3 direction = (_player.transform.position - transform.position).normalized;

            Quaternion rotationToPlayer = Quaternion.LookRotation(direction);

            transform.rotation = rotationToPlayer;

            _velocity = _speed * direction;


        }

        // _velocity = transform.TransformDirection(_velocity);    

        _velocity.y -= _gravity;



        _controller.Move(_velocity * Time.deltaTime);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           _currentState = EnemyState.ATTACK;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _currentState = EnemyState.CHASE;
        }
    }

   
  
}
