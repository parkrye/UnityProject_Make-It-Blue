using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _back, _front, _up;

    public void SwitchCamera(PersonalCameraPosition cameraPosition)
    {
        _back.Priority = cameraPosition == PersonalCameraPosition.Back ? 10 : 0;
        _front.Priority = cameraPosition == PersonalCameraPosition.Front ? 10 : 0;
        _up.Priority = cameraPosition == PersonalCameraPosition.Up ? 10 : 0;
    }
}
