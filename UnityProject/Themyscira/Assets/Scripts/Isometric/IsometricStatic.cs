using UnityEngine;
using System.Collections;

public class IsometricStatic : IsometricBase
{
    // Use this for initialization
    void Start()
    {
        SetZ();
    }

    private void OnBeforeTransformParentChanged()
    {
       // SetZ();
    }
}
