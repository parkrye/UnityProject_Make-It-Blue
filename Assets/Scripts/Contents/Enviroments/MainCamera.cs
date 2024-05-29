using Cinemachine;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Camera _camera;
    public Camera Camera { get { return _camera; } }
    private CinemachineBrain _brain;
    public CinemachineBrain Brain { get { return _brain; } }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _brain = GetComponent<CinemachineBrain>();
    }
}