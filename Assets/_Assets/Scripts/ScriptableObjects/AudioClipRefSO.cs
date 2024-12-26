using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioClipRefSO : ScriptableObject
{
    //������ ������
    public AudioClip[] _chop; //���� ������
    public AudioClip[] _deliverySuccessed; //���� �������� ��������
    public AudioClip[] _deliveryFalied; //���� ��������� ��������
    public AudioClip[] _step; //���� �����
    public AudioClip[] _objectDrop; //���� ��������� ������
    public AudioClip[] _objectPickUp; //���� �������� �������
    public AudioClip _stoveSizzle; //���� ������� �����
    public AudioClip[] _trash; //���� ������;
    public AudioClip[] _warning; //���� ��������������;
}
