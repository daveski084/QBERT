using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBert : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] AudioClip _jumpSound, _landSound, _fallSound, _deathSound;
    [SerializeField] GameObject _speechBubble;

    static readonly int jumpTrigger = Animator.StringToHash("Jump");
    static readonly int landTrigger = Animator.StringToHash("Land");
    bool isJumping = false, isDead = false;
    Transform _transform;
    Rigidbody _rigidbody;
    Vector3 _startPosition;

    BoardManager boardmanager => GameManager.Instance.boardManager;

    public Transform Body { get; private set; }

    private void Awake()
    {
        _transform = transform;
        Body = _transform.GetChild(0);
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isPlaying || isDead || isJumping) return;
        
    }

    public void ResetQBert(Vector3 qBertSpawnPosition, Vector3 qBertSpawnRotation)
    {
        isJumping = false;
        isDead = false;
        _animator.StartPlayback();
        _startPosition = _transform.position = qBertSpawnRotation;
        Body.eulerAngles = qBertSpawnRotation;
        _rigidbody.velocity = _rigidbody.angularVelocity = Vector3.zero;
        gameObject.SetActive(true);
    }

    bool ReceivedPlayerInput(Vector3 desiredfacing, Vector3 landingPosition)
    {
        landingPosition = _transform.position;
        desiredfacing = BoardManager.noChange;

        //up and right [NorthEast]
        if(Input.GetKey(KeyCode.E))
        {
            desiredfacing = BoardManager.northEast;
            landingPosition.y += 3;
            landingPosition.z += 3; 
        }

        //up and left [NorthWest]
        if(Input.GetKey(KeyCode.Q))
        {
            desiredfacing = BoardManager.northWest;
            landingPosition.y += 3;
            landingPosition.x -= 3; 
        }

        //Down and right [NorthEast] 
        if (Input.GetKey(KeyCode.C))
        {
            desiredfacing = BoardManager.southEast;
            landingPosition.y -= 3;
            landingPosition.x += 3;
        }
    }
}
