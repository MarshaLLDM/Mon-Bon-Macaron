using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ObjectSelectionUI : MonoBehaviour
{

    //������������ ��������� ���� � ������� ������������


    [SerializeField] private GameObject _selectionPanel; //������ ��� ����������� ����
    [SerializeField] private GameObject _horizontalGroup; //������ ��� ������ ������������
    [SerializeField] private GameObject _containerPrefab; // ������ ���������� � �������� �������
    [SerializeField] private List<KitchenObject> kitchenObjects; //������ ������������

    private Player _playerMovement; // ������ �� ������ �������� ������

    public event Action<KitchenObject> OnObjectSelected;

    private void Start()
    {
        // ����� ������ ������ �� ����
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _playerMovement = player.GetComponent<Player>();
        }

        PopulateSelectionPanel();
        _selectionPanel.SetActive(false);
    }

    private void PopulateSelectionPanel()
    {
        foreach (var kitchenObject in kitchenObjects)
        {
            var container = Instantiate(_containerPrefab, _horizontalGroup.transform);
            var button = container.GetComponentInChildren<Button>();
            var image = button.GetComponent<Image>(); // �������� ��������� Image �� �������� ������
            image.sprite = kitchenObject._sprite; // ������������� ����������� �� kitchenObject
            button.onClick.AddListener(() => SelectObject(kitchenObject));
        }
    }

    public void ShowSelectionPanel()
    {
        _selectionPanel.SetActive(true);
        _playerMovement.enabled = false;
    }

    private void SelectObject(KitchenObject kitchenObject)
    {
        OnObjectSelected?.Invoke(kitchenObject);
        _selectionPanel.SetActive(false);
        _playerMovement.enabled = true;
    }
}
