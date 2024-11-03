using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class ThrowableItemView
{
    [SerializeField] private List<TextMeshProUGUI> text;

    public void View(int number)
    {
        foreach (var item in text)
        {
            item.text = number.ToString();
        }
    }
}
