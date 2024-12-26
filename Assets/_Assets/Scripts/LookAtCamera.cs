using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
     //Выбор как смотрит камера
    private enum Mode
    {
        CameraForward, //Камера смотрит вперед
        CameraForwardInverted, //Камера смотрит назад
    }

    [SerializeField] private Mode _mode;



    void LateUpdate()
    {
        switch (_mode)
        {
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
