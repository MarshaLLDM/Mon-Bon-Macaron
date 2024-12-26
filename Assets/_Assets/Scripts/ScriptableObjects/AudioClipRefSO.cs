using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioClipRefSO : ScriptableObject
{
    //Список звуков
    public AudioClip[] _chop; //Звук резать
    public AudioClip[] _deliverySuccessed; //Звук успешной доставки
    public AudioClip[] _deliveryFalied; //Звук неудачной доставки
    public AudioClip[] _step; //Звук шагов
    public AudioClip[] _objectDrop; //Звук опускания обекта
    public AudioClip[] _objectPickUp; //Звук поднятия объекта
    public AudioClip _stoveSizzle; //Звук шипения плиты
    public AudioClip[] _trash; //Звук мусора;
    public AudioClip[] _warning; //Звук предупреждения;
}
