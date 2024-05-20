using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private int _groundCounter = 0;
    public bool IsGround { get {  return _groundCounter > 0; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
            _groundCounter++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
            _groundCounter--;
    }
}
