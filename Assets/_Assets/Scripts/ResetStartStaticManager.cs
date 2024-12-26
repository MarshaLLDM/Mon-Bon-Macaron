using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStartStaticManager : MonoBehaviour
{
    //Очищение подписок у статических классов в других скриптах

    private void Awake()
    {
        CuttingCounter.ResetStartStatic();
        BaseCounter.ResetStartStatic();
        TrashCounter.ResetStartStatic();
    }
}
