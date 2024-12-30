using System;
using UnityEngine;
public class ContainerCounterWithWindows : BaseCounter
{
    //Взаимодействие игрока с объектом отображается в окне

    public event EventHandler OnObjectSelected;

    [SerializeField] private ObjectSelectionUI objectSelectionUI;

    private void Start()
    {
        if (objectSelectionUI == null)
        {
            Debug.LogError("ObjectSelectionUI is not assigned in the inspector.");
            return;
        }
        objectSelectionUI.OnObjectSelected += HandleObjectSelected;
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            objectSelectionUI.ShowSelectionPanel();
        }
    }

    private void HandleObjectSelected(KitchenObject kitchenObjectSO)
    {
        Player.Instance.SetKitchenObject(LinkKitchenObject.SpawnKitchenObject(kitchenObjectSO, Player.Instance));
        OnObjectSelected?.Invoke(this, EventArgs.Empty);
    }
}
