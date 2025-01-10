using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    //Ожидающие рецепты, которые можно приготовить

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeTimerExpired;
    private int _successedRecipedAmount = 0; //Кол-во успешных рецептов за одну игру
    [SerializeField] private int _successedRecipedAmountMax; //Кол-во успешных рецептов за всю игру
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO _recipeListSO;

    private List<RecipeWithTimer> _waitingRecipeList;
    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax = 4; //Максимальное кол-во рецептов в ожидании

    private void Awake()
    {
        Instance = this;
        _waitingRecipeList = new List<RecipeWithTimer>();
        LoadSuccessedRecipedAmount();

    }

    private void Update()   
    {
        if (GameManager.Instance.IsGamePlaing()) // Проверка состояния игры
        {
            _spawnRecipeTimer -= Time.deltaTime; //Уменьшение таймера для появления рецептов
        if (_spawnRecipeTimer < 0)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if (_waitingRecipeList.Count < _waitingRecipeMax) //Если количество рецептов меньше максимального, то будет генерация рандомного рецепта
            {
                RecipeSO randomRecipe = _recipeListSO._recipeSOList[UnityEngine.Random.Range(0, _recipeListSO._recipeSOList.Count)]; //Берется рандомный рецепт
                RecipeWithTimer newRecipe = new RecipeWithTimer { Recipe = randomRecipe, Timer = 20f };

                _waitingRecipeList.Add(newRecipe); //Добавляется рецепт в ожидающий лист

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }


            // Обновляем таймер для каждого рецепта
            for (int i = _waitingRecipeList.Count - 1; i >= 0; i--)
            {
                _waitingRecipeList[i].Timer -= Time.deltaTime;
                if (_waitingRecipeList[i].Timer <= 0)
                {
                    _waitingRecipeList.RemoveAt(i);
                    OnRecipeTimerExpired?.Invoke(this, EventArgs.Empty); 
                }
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject _plateKitchenObject) //Функция доставки рецепта
    {
        for (int i = 0; i < _waitingRecipeList.Count; i++)
        {
            RecipeWithTimer recipeWithTimer = _waitingRecipeList[i];
            RecipeSO _waitingRecipeSO = recipeWithTimer.Recipe;

            if (_waitingRecipeSO._kitchenObjects.Count == _plateKitchenObject.GetKitchenObjectsList().Count) //Есть нужное кол-во рецептов
            {
                bool _plateContentsMatchesRecipe = true;
                foreach (KitchenObject _recipeKitchenObjectSO in _waitingRecipeSO._kitchenObjects) //Проверка всех ингредиентов на рецепте
                {
                    bool _ingredientFound = false;
                    foreach (KitchenObject _plateKitchenObjectSO in _plateKitchenObject.GetKitchenObjectsList()) //Проверка всех ингредиентов на тарелке
                    {
                        if (_plateKitchenObjectSO == _recipeKitchenObjectSO) //Проверка совпадают ли рецепты, которые из листа ожидания со списком рецептом
                        {
                            //Они совпадают
                            _ingredientFound = true;
                            break;
                        }
                    }
                    if (!_ingredientFound) //Если не нашел
                    {
                        //То такого ингредиента нет
                        _plateContentsMatchesRecipe = false;
                    }
                }
                if (_plateContentsMatchesRecipe)
                {
                    //Игрок приготов правильный рецепт

                    _successedRecipedAmount++;
                    _successedRecipedAmountMax++;

                    _waitingRecipeList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    GameManager.Instance.IncreaseGameTime(15f); // Увеличение времени на 15 секунд
                    return;
                }
            }
        }
        //Не один из рецептов не подходит
        //Игрок приготовоил неправильный рецепт
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        GameManager.Instance.ReduceGameTime(30f); // Уменьшение времени на 30 секунд
    }

    public List<RecipeWithTimer> GetWaitingRecipeList()
    {
        return _waitingRecipeList;
    }

    public int GetSucceseedReciped() //Метода получения кол-ва успешных рецептов за одну игру
    {
        return _successedRecipedAmount;
    }

    public int GetSucceseedRecipedMax() //Метода получения кол-ва успешных рецептов за всю игру
    {
        return _successedRecipedAmountMax;
    }


    private void OnDestroy()
    {
        SaveSuccessedRecipedAmount();
    }

    private void SaveSuccessedRecipedAmount()
    {
        PlayerPrefs.SetInt("SuccessedRecipedAmount", _successedRecipedAmountMax);
        PlayerPrefs.Save();
    }

    private void LoadSuccessedRecipedAmount()
    {
        _successedRecipedAmountMax = PlayerPrefs.GetInt("SuccessedRecipedAmount", 0);
    }
}

public class RecipeWithTimer
{
    public RecipeSO Recipe { get; set; }
    public float Timer { get; set; }
}
