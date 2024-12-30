using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterWithWindowsVisual : MonoBehaviour
{

    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounterWithWindows _containersCounterWithWindows;
    private Animator _animator; //анимация открытия контейнера

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _containersCounterWithWindows.OnObjectSelected += _containersCounterWithWindow_OnPlayerGrabbedObject;
    }

    private void _containersCounterWithWindow_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(OPEN_CLOSE);
    }
}
