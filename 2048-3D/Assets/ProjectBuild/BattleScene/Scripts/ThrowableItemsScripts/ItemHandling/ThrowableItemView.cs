using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ThrowableItemView : MonoBehaviour
{
    [SerializeField] private List<TextMeshPro> text;

    public void View(int number)
    {
        foreach (var item in text)
        {
            item.text = number.ToString();
        }
    }
}
