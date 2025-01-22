using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{
    public Action onHit;

    void OnTriggerEnter(Collider other) {
        onHit?.Invoke();
    }
}
