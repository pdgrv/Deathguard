using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shop : InteractableObject
{
    [SerializeField] private int _cost;
    [SerializeField] private int _costIncrease;

    protected override sealed void Activate(Player player)
    {
        if (player.EnoughMoney(_cost))
        {
            if (BuyItem(player))
                UpdateItem();
            else
                Debug.Log("Нет товара");
        }
        else
        {
            Debug.Log("недостаточно денег");
        }

        _isUsed = false;
    }

    protected virtual void UpdateItem()
    {
        _cost += _costIncrease;
    }

    protected abstract bool BuyItem(Player player);
}
