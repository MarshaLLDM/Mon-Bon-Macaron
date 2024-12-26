using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player _player;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetBool(IS_WALKING, _player.IsWalking());
    }
}

