using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [System.Serializable] private class ColliderEvent : UnityEvent<Collider> { }

    [SerializeField] private ColliderEvent _enterEvent = null;
    [SerializeField] private ColliderEvent _stayEvent = null;
    [SerializeField] private ColliderEvent _exitEvent = null;

    private void OnTriggerStay(Collider other)
    {
        if (_stayEvent == null) return;
        _stayEvent.Invoke(other);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_stayEvent == null) return;
        _stayEvent.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (_stayEvent == null) return;
        _stayEvent.Invoke(other);
    }
}