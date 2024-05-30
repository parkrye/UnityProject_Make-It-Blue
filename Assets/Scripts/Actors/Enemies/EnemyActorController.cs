using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyActorController : MonoBehaviour
{
    private EnemyActor _actor;
    private NavMeshAgent _agent;

    private Vector3 _findPosition;
    private Transform _target;

    private float _range, _viewAngle, _timer;
    private int _reloadDelay;
    [SerializeField] private bool _isWork = true, _isShootable = false, _isLookable = false;

    private void Awake()
    {
        _actor = GetComponent<EnemyActor>();
        if (_actor == null)
            Debug.Log($"name lost EnemyActor!");

        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
            Debug.Log($"{name} lost NavMeshSurface!");

        _range = _actor.EnemyData.GetRange * 2f;
        _viewAngle = Mathf.Cos(45f) * Mathf.Deg2Rad;
        _reloadDelay = (int)(_actor.EnemyData.GetShotDelay * 1000);

        _actor.ActorDiedEvent.AddListener((t) => _isWork = false);

        ReloadRouitne().Forget();
    }

    private void Start()
    {
        _findPosition = GameManager.System.PlayerActor.transform.position;
    }

    public void Tick()
    {
        _agent.SetDestination(_findPosition);

        if (_target == null)
        {
            SearchTarget();

            if (_timer > 10f)
            {
                _timer = 0f;
                _findPosition = GameManager.System.PlayerActor.transform.position;
            }
        }
        else
        {
            LookTarget();

            if (_isLookable)
            {
                if (_isShootable)
                    ShotTarget();
            }
            else
            {
                _findPosition = _target.position;
            }

            if (_timer > 10f)
            {
                _timer = 0f;
            }
        }

        _timer += Time.deltaTime;
    }

    private void LookTarget()
    {
        _isLookable = Physics.RaycastAll(new Ray(transform.position, _target.position - transform.position)).Where(t => t.collider.CompareTag("Wall")).Count() == 0;
    }

    private void ShotTarget()
    {
        var hitable = _target.GetComponent<IHitable>();
        var conditionable = _target.GetComponent<IConditionalbe>();
        hitable.Hit(Calculator.CalcuateDamage(
            _actor.EnemyData.Damage, _actor.EnemyData.Accuracy, hitable.GetStatus(StatusEnum.Avoid), 
            condition: conditionable == null ? 0 : conditionable.GetConditionCount()));
    }

    private void SearchTarget()
    {
        var targets = Physics.OverlapSphere(transform.position, _range, LayerMask.GetMask("Player"));

        foreach (var target in targets)
        {
            var normalDir = (target.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, normalDir) > _viewAngle)
            {
                _target = target.transform;
                return;
            }
        }

        _target = null;
    }

    private async UniTask ReloadRouitne()
    {
        while(_isWork)
        {
            await UniTask.Delay(_reloadDelay * 1000);
            _isShootable = true;
        }
    }
}
