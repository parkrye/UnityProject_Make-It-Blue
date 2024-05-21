using UnityEngine;
using UnityEngine.Events;

public class ExternalTriggerChecker : MonoBehaviour
{
    public UnityEvent<Collider> TriggerEnter = new UnityEvent<Collider>();
    public UnityEvent<Collider> TriggerStay = new UnityEvent<Collider>();
    public UnityEvent<Collider> TriggerExit = new UnityEvent<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TriggerStay?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit?.Invoke(other);
    }
}
