using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputCurrentSelected 
{
    bool IsCurrentItemSelected<T>(T currentItem) where T : MonoBehaviour;
}
