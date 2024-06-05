using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CameraWallChecker : MonoBehaviour
{
    private CinemachineTransposer _transposer;
    private bool _isWall;
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
        while (gameObject != null)
        {
            await UniTask.Delay(100);
            if (GameManager.System.PlayerActor == null)
                continue;

            if (Physics.Raycast(transform.position, GameManager.System.PlayerActor.CenterPosition - transform.position, out var frontHit))
                _isWall = frontHit.collider.CompareTag("Wall");

            if (_isWall)
                continue;

            if (Physics.SphereCast(transform.position, 0.2f, Vector3.up, out var aroundHit))
                _isWall = aroundHit.collider.CompareTag("Wall");
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
}
