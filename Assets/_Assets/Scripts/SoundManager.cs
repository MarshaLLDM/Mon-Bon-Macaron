using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Воспроизвезедение звуков

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume"; //Переменная для сохранения звуков

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefSO _audioClipRefSO;

    private float _volume = 1f;  //Сколько громкости

    private void Awake()
    {
        Instance = this;

        _volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f); //Загрузка значений информации о звуке
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrash += TrashCounter_OnAnyObjectTrash;
    }

    private void TrashCounter_OnAnyObjectTrash(object sender, System.EventArgs e)
    {
        TrashCounter _trashCounter = sender as TrashCounter;
        PlaySound(_audioClipRefSO._trash, _trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter _baseCounter = sender as BaseCounter;
        PlaySound(_audioClipRefSO._objectDrop, _baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(_audioClipRefSO._objectPickUp, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter _cuttingCounter = sender as CuttingCounter;
        PlaySound(_audioClipRefSO._chop, _cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter _deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipRefSO._deliveryFalied, _deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter _deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipRefSO._deliverySuccessed, _deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] _audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(_audioClipArray[Random.Range(0, _audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip _audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(_audioClip, position, volumeMultiplier * _volume);
    }

    public void PlayStepSound(Vector3 position, float volume)
    {
        PlaySound(_audioClipRefSO._step, position, volume);
    }
    public void PlayCountDownSound()
    {
        PlaySound(_audioClipRefSO._warning, Vector3.zero);
    }
    public void PlayWarningSOund(Vector3 position)
    {
        PlaySound(_audioClipRefSO._warning, position);
    }

    public void ChangeVolume() //Функция изменение грмкости
    {
        _volume += .1f;
        if (_volume > 1f)
        {
            _volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, _volume); //Получение значения звука
        PlayerPrefs.Save(); //Сохранение значения звука
    }

    public float GetVolume()
    {
        return _volume;
    }
}
