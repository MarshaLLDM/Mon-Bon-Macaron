using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    // Визуализация тарелки

    [SerializeField] private PlatesCounter _platesCounter;
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private Transform _plateVisualPrefab;

    private List<GameObject> _plateVisuaGameObjectsList;

    private void Awake()
    {
        _plateVisuaGameObjectsList = new List<GameObject>();
    }

    private void Start()
    {
        _platesCounter.OnPlateSpawned += _platesCounter_OnPlateSpawned;
        _platesCounter.OnPlateRemoved += _platesCounter_OnPlateRemoved;
    }

    private void _platesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject _plateGameObject = _plateVisuaGameObjectsList[_plateVisuaGameObjectsList.Count-1];
        _plateVisuaGameObjectsList.Remove(_plateGameObject);
        Destroy(_plateGameObject);
    }

    private void _platesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform _plateVisualTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);

        float _plateOffsetY = .1f;
        _plateVisualTransform.localPosition = new Vector3(0, _plateOffsetY * _plateVisuaGameObjectsList.Count, 0);

        _plateVisuaGameObjectsList.Add(_plateVisualTransform.gameObject);
    }
}
