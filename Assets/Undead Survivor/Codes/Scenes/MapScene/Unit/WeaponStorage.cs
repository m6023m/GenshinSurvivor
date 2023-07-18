using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

public class WeaponStorage : MonoBehaviour
{
    float speed = 50.0f;

    private void Update()
    {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }

}
