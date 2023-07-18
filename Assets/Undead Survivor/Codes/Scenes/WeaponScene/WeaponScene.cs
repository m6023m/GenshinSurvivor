using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WeaponScene : MonoBehaviour
{
    private void Awake()
    {
        WeaponManager.instance.SetEnable(true);
    }
    private void OnDisable()
    {
        WeaponManager.instance.SetEnable(false);
    }

}

