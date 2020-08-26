using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Shop : InteractableObject
{
    [SerializeField] protected int _stat;
    [SerializeField] private int _statIncrease;
    [SerializeField] private int _cost;
    [SerializeField] private int _costIncrease;
    [SerializeField] private TMP_Text _costLabel;
    [SerializeField] private TMP_Text _statLabel;

    protected override sealed void Start()
    {
        base.Start();

        _costLabel.text = _cost.ToString() + " g";
        _statLabel.text = "+" + _stat.ToString();
    }

    protected override sealed void Activate(Player player)
    {
        if (player.BuyItem(_cost))
        {
            SellItem(player);
            UpdateItem();
        }
        else
        {
            Debug.Log("недостаточно денег");
        }

        _isUsed = false;
    }

    protected virtual void UpdateItem()
    {
        _stat += _statIncrease;
        _cost += _costIncrease;

        _costLabel.text = _cost.ToString() + " g";
        _statLabel.text = "+" + _stat.ToString();
    }

    protected abstract void SellItem(Player player);
}
