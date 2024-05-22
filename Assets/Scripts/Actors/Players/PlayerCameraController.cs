using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _back, _front, _up;

    public void SwitchCamera(PersonalCameraPositionEnum cameraPosition)
    {
        _back.Priority = cameraPosition == PersonalCameraPositionEnum.Back ? 10 : 0;
        _front.Priority = cameraPosition == PersonalCameraPositionEnum.Front ? 10 : 0;
        _up.Priority = cameraPosition == PersonalCameraPositionEnum.Up ? 10 : 0;
    }
}
