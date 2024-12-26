using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething; //Функция для воспроизведения звука

    public event EventHandler <OnSelectedCounterChangedEventArgs> _OnSelectedCounterChanged; //Событие для остальных счетчиков
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter _selectCounter;
    }

    [SerializeField] private float movespeed = 9f;
    [SerializeField] private GameInput _gameinput;
    [SerializeField] private LayerMask _counterlayerMask;
    [SerializeField] private Transform _kitchenObjectHoldPoint;



    private bool isWalking; //Проверка ходьбы
    private Vector3 _lastInsteractDir; //последнее нахождение
    private BaseCounter _selectedCounter; //Выбранный стол
    private LinkKitchenObject _onkitchenObjects; // что лежит на контейнере


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Больше одного");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameinput.OnInteractAction += Gameinput_OnInteractAction;
        _gameinput.OnInteractAlternateAction += _gameinput_OnInteractAlternateAction;
    }

    private void _gameinput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if(!GameManager.Instance.IsGamePlaing()) return;

        if (_selectedCounter != null)
        {
            _selectedCounter.InteractAlternate(this);
        }
    }

    private void Gameinput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaing()) return;

        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = _gameinput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDir != Vector3.zero)
        {
            _lastInsteractDir = moveDir;
        }

        float _interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInsteractDir, out RaycastHit _raycasthit, _interactDistance, _counterlayerMask))
        {
            if (_raycasthit.transform.TryGetComponent(out BaseCounter _baseCounter))
            {
                if (_baseCounter != _selectedCounter)
                {
                    SetSelectedCounter(_baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
            }
            else
            {
                SetSelectedCounter(null);
            }

  //      Debug.Log(_selectedCounter);
    }
    private  void HandleMovement() //Функция перемещения
    {
        Vector2 inputVector = _gameinput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = movespeed * Time.deltaTime;
        float _playerRaduis = 0.7f;
        float _playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRaduis, moveDir, moveDistance);

        if (!canMove)
        {
            //Движение по оси X
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRaduis, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDir.z !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRaduis, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {

                }
            }


        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 10f);
    }

    private void SetSelectedCounter(BaseCounter _selectCounter)
    {
        this._selectedCounter = _selectCounter;

        _OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            _selectCounter = _selectedCounter
        });
    }

    public Transform GetKitchenOjectFollowTransform()
    {
        return _kitchenObjectHoldPoint;
    }
    public void SetKitchenObject(LinkKitchenObject _onkitchenObjects)
    {
        this._onkitchenObjects = _onkitchenObjects;

        if( _onkitchenObjects != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty); //Вызов функции для воспроизведения звука
        }
    }
    public LinkKitchenObject GetKitchenObject()
    {
        return _onkitchenObjects;
    }

    public void ClearKitchenObject()
    {
        _onkitchenObjects = null;
    }

    public bool HasKitchenObject()
    {
        return _onkitchenObjects != null;
    }
}
