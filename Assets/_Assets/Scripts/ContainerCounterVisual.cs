using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{

    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainersCounter _containersCounter;
    private Animator _animator; //анимация открытия контейнера

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _containersCounter.OnPlayerGrabbedObject += _containersCounter_OnPlayerGrabbedObject;
    }

    private void _containersCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(OPEN_CLOSE);
    }
}
