using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedVisualCounter : MonoBehaviour
{
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private GameObject[] _visualGameObjectArray;
    private void Start()
    {
        Player.Instance._OnSelectedCounterChanged += Player__OnSelectedCounterChanged;
    }

    private void Player__OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e._selectCounter == _baseCounter)  
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject _visualGameObject in _visualGameObjectArray)
        {
            _visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject _visualGameObject in _visualGameObjectArray)
        {
            _visualGameObject.SetActive(false);
        }
    }
}
