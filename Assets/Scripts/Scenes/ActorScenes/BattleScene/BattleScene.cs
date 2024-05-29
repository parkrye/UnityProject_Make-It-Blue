using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattleScene : ActorScene, IValueTrackable
{
    private int _enemyCount;
    private bool _onGame;
    private Transform[] _enemySpawnPositions;

    protected override async UniTask LoadingRoutine()
    {
        GameManager.System.AddValueTrackAction(ValueTrackEvent);
        await UniTask.Delay(100);

        Progress = 1f;
    }

    public virtual void ValueTrackEvent(ValueTrackEnum valueEnum)
    {
        switch (valueEnum)
        {
            default:
                break;
            case ValueTrackEnum.CrossHead:
                if (GameManager.UI.OpenView<MainView>("MainView", out var mainView))
                    mainView.TurnCrossHead(StaticValues.CrossHead);
                break;
        }
    }

    protected override void InitScene()
    {
        base.InitScene();

        _enemySpawnPositions = GameObject.Find("EnemySpawnPosition").GetComponentsInChildren<Transform>();

        GameManager.System.PlayerActor.EquipEquipments(
            GameManager.System.CurrentMission.Weapon, GameManager.System.CurrentMission.Items);

        var index = 0;
        foreach (var enemyGroup in GameManager.System.CurrentMission.Mission.EnemyGroupArray)
        {
            foreach (var enemy in enemyGroup.EnemyArray)
            {
                var spawned = GameManager.Resource.Instantiate(enemy, _enemySpawnPositions[index].position, Quaternion.identity, transform, false);

                if (index < _enemySpawnPositions.Length - 1)
                    index++;
                else
                    index = 0;
            }
        }
    }

    protected override void InitActors()
    {
        _actors = FindObjectsOfType<BaseActor>();
        foreach (var actor in _actors)
        {
            actor.InitForBattle();
            actor.ActorDiedEvent.AddListener(OnActorDied);
        }
    }

    public void StartBattle()
    {
        _onGame = true;
    }

    private void OnActorDied(bool isPlayer)
    {
        if (_onGame == false)
            return;

        if (isPlayer)
        {
            OnPlayerDefeat();
            _onGame = false;
        }
        else
        {
            _enemyCount--;
            if (_enemyCount <= 0)
            {
                OnPlayerWin();
                _onGame = false;
            }
        }
        
    }


    private void OnPlayerDefeat()
    {

    }

    private void OnPlayerWin()
    {

    }
}
