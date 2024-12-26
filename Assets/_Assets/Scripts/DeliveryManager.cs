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
    private int _successedRecipedAmount; //Кол-во успешных рецептов
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO _recipeListSO;


    private List<RecipeSO> _waitingRecipeSOList;
    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax = 4; //Максимальное кол-во рецептов в ожидании

    private void Awake()
    {
        Instance = this;

        _waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime; //Уменьшение таймера для появления рецептов
        if ( _spawnRecipeTimer < 0)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if (_waitingRecipeSOList.Count < _waitingRecipeMax) //Если количество рецептов меньше максимального, то будет генерация рандомного рецепта
            {
                RecipeSO _waitingRecipeSo = _recipeListSO._recipeSOList[UnityEngine.Random.Range(0, _recipeListSO._recipeSOList.Count)]; //Берется рандомный рецепт
                _waitingRecipeSOList.Add(_waitingRecipeSo); //Добавляется рецепт в ожидающий лист

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject _plateKitchenObject) //Функция доставки рецепта
    {
        for (int i=0;  i < _waitingRecipeSOList.Count; i++) 
        {
            RecipeSO _waitingRecipeSO = _waitingRecipeSOList[i];

            if(_waitingRecipeSO._kitchenObjects.Count == _plateKitchenObject.GetKitchenObjectsList().Count) //Есть нужное кол-во рецептов
            {
                bool _plateContentsMatchesRecipe = true;
                foreach (KitchenObject _recipeKitchenObjectSO in _waitingRecipeSO._kitchenObjects) //Проверка всех ингредиентов на рецепте
                {
                    bool _ingredientFound = false;
                    foreach (KitchenObject _plateKitchenObjectSO in _plateKitchenObject.GetKitchenObjectsList()) //Проверка всех ингредиентов на тарелке
                    {
                        if(_plateKitchenObjectSO == _recipeKitchenObjectSO) //Проверка совпадают ли рецепты, которые из листа ожидания со списком рецептом
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
                if(_plateContentsMatchesRecipe)
                {
                    //Игрок приготов правильный рецепт

                    _successedRecipedAmount++;

                    _waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //Не один из рецептов не подходит
        //Игрок приготовоил неправильный рецепт
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return _waitingRecipeSOList;
    }

    public int GetSucceseedReciped()
    {
        return _successedRecipedAmount;
    }
}
