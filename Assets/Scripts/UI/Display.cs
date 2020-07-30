using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Display : MonoBehaviour
{
    [SerializeField] protected TMP_Text _text;

    protected void OnValueChanged(int value)
    {
        _text.text = value.ToString();
    }
}
