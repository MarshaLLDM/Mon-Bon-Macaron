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
    private int _successedRecipedAmount; //���-�� �������� ��������
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO _recipeListSO;


    private List<RecipeSO> _waitingRecipeSOList;
    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax = 4; //������������ ���-�� �������� � ��������

    private void Awake()
    {
        Instance = this;

        _waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime; //���������� ������� ��� ��������� ��������
        if ( _spawnRecipeTimer < 0)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if (_waitingRecipeSOList.Count < _waitingRecipeMax) //���� ���������� �������� ������ �������������, �� ����� ��������� ���������� �������
            {
                RecipeSO _waitingRecipeSo = _recipeListSO._recipeSOList[UnityEngine.Random.Range(0, _recipeListSO._recipeSOList.Count)]; //������� ��������� ������
                _waitingRecipeSOList.Add(_waitingRecipeSo); //����������� ������ � ��������� ����

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject _plateKitchenObject) //������� �������� �������
    {
        for (int i=0;  i < _waitingRecipeSOList.Count; i++) 
        {
            RecipeSO _waitingRecipeSO = _waitingRecipeSOList[i];

            if(_waitingRecipeSO._kitchenObjects.Count == _plateKitchenObject.GetKitchenObjectsList().Count) //���� ������ ���-�� ��������
            {
                bool _plateContentsMatchesRecipe = true;
                foreach (KitchenObject _recipeKitchenObjectSO in _waitingRecipeSO._kitchenObjects) //�������� ���� ������������ �� �������
                {
                    bool _ingredientFound = false;
                    foreach (KitchenObject _plateKitchenObjectSO in _plateKitchenObject.GetKitchenObjectsList()) //�������� ���� ������������ �� �������
                    {
                        if(_plateKitchenObjectSO == _recipeKitchenObjectSO) //�������� ��������� �� �������, ������� �� ����� �������� �� ������� ��������
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
                if(_plateContentsMatchesRecipe)
                {
                    //����� �������� ���������� ������

                    _successedRecipedAmount++;

                    _waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //�� ���� �� �������� �� ��������
        //����� ����������� ������������ ������
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
