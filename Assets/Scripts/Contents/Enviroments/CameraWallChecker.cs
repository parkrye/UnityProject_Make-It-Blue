using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CameraWallChecker : MonoBehaviour
{
    private CinemachineTransposer _transposer;
    private bool _isWall, _isWork;
    private float _initialZValue;

    private void Awake()
    {
        _transposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
        _initialZValue = _transposer.m_FollowOffset.z;
    }

    private void Start()
    {
        WallCheckRoutine().Forget();
    }

    private async UniTask WallCheckRoutine()
    {
        _isWork = true;
        while (_isWork)
        {
            if (GameManager.System.PlayerActor != null)
            {
                if (Physics.Raycast(transform.position, GameManager.System.PlayerActor.CenterPosition - transform.position, out var frontHit))
                    _isWall = frontHit.collider.CompareTag("Wall");

                if (_isWall == false)
                {
                    if (Physics.SphereCast(transform.position, 0.2f, Vector3.up, out var aroundHit))
                        _isWall = aroundHit.collider.CompareTag("Wall");
                }
            }

            await UniTask.Delay(100);
        }
    }

    private void LateUpdate()
    {
        if (_isWall)
        {
            if (_transposer.m_FollowOffset.z < 0f)
                _transposer.m_FollowOffset.z = Mathf.Lerp(_transposer.m_FollowOffset.z, 0f, Time.deltaTime);
        }
        else
        {
            if (_transposer.m_FollowOffset.z > _initialZValue)
                _transposer.m_FollowOffset.z = Mathf.Lerp(_transposer.m_FollowOffset.z, _initialZValue, Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        _isWork = false;
    }
}
