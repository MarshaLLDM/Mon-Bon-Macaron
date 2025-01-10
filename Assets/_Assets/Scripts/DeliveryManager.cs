using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    //��������� �������, ������� ����� �����������

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeTimerExpired;
    private int _successedRecipedAmount = 0; //���-�� �������� �������� �� ���� ����
    [SerializeField] private int _successedRecipedAmountMax; //���-�� �������� �������� �� ��� ����
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO _recipeListSO;

    private List<RecipeWithTimer> _waitingRecipeList;
    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax = 4; //������������ ���-�� �������� � ��������

    private void Awake()
    {
        Instance = this;
        _waitingRecipeList = new List<RecipeWithTimer>();
        LoadSuccessedRecipedAmount();

    }

    private void Update()   
    {
        if (GameManager.Instance.IsGamePlaing()) // �������� ��������� ����
        {
            _spawnRecipeTimer -= Time.deltaTime; //���������� ������� ��� ��������� ��������
        if (_spawnRecipeTimer < 0)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if (_waitingRecipeList.Count < _waitingRecipeMax) //���� ���������� �������� ������ �������������, �� ����� ��������� ���������� �������
            {
                RecipeSO randomRecipe = _recipeListSO._recipeSOList[UnityEngine.Random.Range(0, _recipeListSO._recipeSOList.Count)]; //������� ��������� ������
                RecipeWithTimer newRecipe = new RecipeWithTimer { Recipe = randomRecipe, Timer = 20f };

                _waitingRecipeList.Add(newRecipe); //����������� ������ � ��������� ����

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }


            // ��������� ������ ��� ������� �������
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

    public void DeliverRecipe(PlateKitchenObject _plateKitchenObject) //������� �������� �������
    {
        for (int i = 0; i < _waitingRecipeList.Count; i++)
        {
            RecipeWithTimer recipeWithTimer = _waitingRecipeList[i];
            RecipeSO _waitingRecipeSO = recipeWithTimer.Recipe;

            if (_waitingRecipeSO._kitchenObjects.Count == _plateKitchenObject.GetKitchenObjectsList().Count) //���� ������ ���-�� ��������
            {
                bool _plateContentsMatchesRecipe = true;
                foreach (KitchenObject _recipeKitchenObjectSO in _waitingRecipeSO._kitchenObjects) //�������� ���� ������������ �� �������
                {
                    bool _ingredientFound = false;
                    foreach (KitchenObject _plateKitchenObjectSO in _plateKitchenObject.GetKitchenObjectsList()) //�������� ���� ������������ �� �������
                    {
                        if (_plateKitchenObjectSO == _recipeKitchenObjectSO) //�������� ��������� �� �������, ������� �� ����� �������� �� ������� ��������
                        {
                            //��� ���������
                            _ingredientFound = true;
                            break;
                        }
                    }
                    if (!_ingredientFound) //���� �� �����
                    {
                        //�� ������ ����������� ���
                        _plateContentsMatchesRecipe = false;
                    }
                }
                if (_plateContentsMatchesRecipe)
                {
                    //����� �������� ���������� ������

                    _successedRecipedAmount++;
                    _successedRecipedAmountMax++;

                    _waitingRecipeList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    GameManager.Instance.IncreaseGameTime(15f); // ���������� ������� �� 15 ������
                    return;
                }
            }
        }
        //�� ���� �� �������� �� ��������
        //����� ����������� ������������ ������
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        GameManager.Instance.ReduceGameTime(30f); // ���������� ������� �� 30 ������
    }

    public List<RecipeWithTimer> GetWaitingRecipeList()
    {
        return _waitingRecipeList;
    }

    public int GetSucceseedReciped() //������ ��������� ���-�� �������� �������� �� ���� ����
    {
        return _successedRecipedAmount;
    }

    public int GetSucceseedRecipedMax() //������ ��������� ���-�� �������� �������� �� ��� ����
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
