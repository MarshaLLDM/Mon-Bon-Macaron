using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonUI : MonoBehaviour
{
    // Скрипт для закрытия кнопкой
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
