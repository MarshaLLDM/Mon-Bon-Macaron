using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    private int _successedRecipedAmount;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO _recipeListSO;

    private List<RecipeSO> _waitingRecipeSOList;
    private List<float> _recipeTimers;
    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax = 4;
    private float _recipeDuration = 15f; // Длительность рецепта

    private void Awake()
    {
        Instance = this;
        _waitingRecipeSOList = new List<RecipeSO>();
        _recipeTimers = new List<float>();
    }

    private void Update()
    {

        _spawnRecipeTimer -= Time.deltaTime;
        if (_spawnRecipeTimer < 0)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if (_waitingRecipeSOList.Count < _waitingRecipeMax)
            {
                RecipeSO _waitingRecipeSo = _recipeListSO._recipeSOList[UnityEngine.Random.Range(0, _recipeListSO._recipeSOList.Count)];
                _waitingRecipeSOList.Add(_waitingRecipeSo);
                if (GameManager.Instance.IsGamePlaing()) //Проверка состояния игры - "IsGamePlaing"
                {
                    _recipeTimers.Add(_recipeDuration); // Добавляем таймер для нового рецепта
                }


                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }

        // Обновляем таймеры рецептов
        for (int i = _recipeTimers.Count - 1; i >= 0; i--)
        {
            _recipeTimers[i] -= Time.deltaTime;
            if (_recipeTimers[i] < 0)
            {
                // Таймер истек, удаляем рецепт и вызываем ReduceGameTime
                _waitingRecipeSOList.RemoveAt(i);
                _recipeTimers.RemoveAt(i);
                GameManager.Instance.ReduceGameTime(30f); // Уменьшение времени на 30 секунд
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject _plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecipeSOList.Count; i++)
        {
            RecipeSO _waitingRecipeSO = _waitingRecipeSOList[i];

            if (_waitingRecipeSO._kitchenObjects.Count == _plateKitchenObject.GetKitchenObjectsList().Count)
            {
                bool _plateContentsMatchesRecipe = true;
                foreach (KitchenObject _recipeKitchenObjectSO in _waitingRecipeSO._kitchenObjects)
                {
                    bool _ingredientFound = false;
                    foreach (KitchenObject _plateKitchenObjectSO in _plateKitchenObject.GetKitchenObjectsList())
                    {
                        if (_plateKitchenObjectSO == _recipeKitchenObjectSO)
                        {
                            _ingredientFound = true;
                            break;
                        }
                    }
                    if (!_ingredientFound)
                    {
                        _plateContentsMatchesRecipe = false;
                    }
                }
                if (_plateContentsMatchesRecipe)
                {
                    _successedRecipedAmount++;
                    _waitingRecipeSOList.RemoveAt(i);
                    _recipeTimers.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    GameManager.Instance.IncreaseGameTime(15f); // Увеличение времени на 15 секунд
                    return;
                }
            }
        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        GameManager.Instance.ReduceGameTime(30f); // Уменьшение времени на 30 секунд
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return _waitingRecipeSOList;
    }

    public int GetSucceseedReciped()
    {
        return _successedRecipedAmount;
    }

    public float GetRemainingTimeForRecipe(int index)
    {
        if (index >= 0 && index < _recipeTimers.Count)
        {
            return _recipeTimers[index];
        }
        return 0f;
    }

    public float GetRecipeDuration()
    {
        return _recipeDuration;
    }
}
