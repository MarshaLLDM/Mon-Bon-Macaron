using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonUI : MonoBehaviour
{
    // ������ ��� �������� �������
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
