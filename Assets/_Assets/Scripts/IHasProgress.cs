using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    // Интерфейс для визуализации прогресса

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged; //Обработка визуального прогресса
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float _progressNormalized;
    }

}
