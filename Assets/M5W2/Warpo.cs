using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Warpo : MonoBehaviour
{
    public UnityEvent onWarp;

    private void OnTriggerEnter(Collider other) => onWarp?.Invoke();
}
