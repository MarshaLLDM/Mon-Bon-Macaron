using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStartStaticManager : MonoBehaviour
{
    //�������� �������� � ����������� ������� � ������ ��������

    private void Awake()
    {
        CuttingCounter.ResetStartStatic();
        BaseCounter.ResetStartStatic();
        TrashCounter.ResetStartStatic();
    }
}
