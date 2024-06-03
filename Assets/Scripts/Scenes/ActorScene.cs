using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class ActorScene : BaseScene
{
    public MainCamera Camera { get; private set; }

    protected BaseActor[] _actors;

    public override async void LoadAsync(params Object[] parameters)
    {
        GameManager.System.PlayerActor = FindObjectOfType<PlayerActor>();

        InitScene();
        InitActors();

        Camera = FindObjectOfType<MainCamera>();

        await LoadingRoutine();
    }

    protected override async UniTask LoadingRoutine()
    {
        await UniTask.Delay(0);
    }

    protected virtual void InitScene()
    {
        if (GameManager.UI.OpenUI<MainView>(PublicUIEnum.Main, out var mainView))
        {
            mainView.SendSubtitles();
        }

        if (GameManager.Data.Play.Model != null)
        {
            var model = GameManager.Resource.Instantiate(GameManager.Data.Play.Model, GameManager.System.PlayerActor.Model);
            model.transform.localPosition = Vector3.zero;
            model.transform.localEulerAngles = Vector3.zero;

            if (GameManager.Data.Play.HaloShape != null)
            {
                var halo = GameManager.Resource.Instantiate(GameManager.Data.Play.HaloShape, model.Find("HaloRoot").transform);
                halo.transform.localPosition = Vector3.zero;
                halo.transform.localEulerAngles = Vector3.zero;

                var haloColor = GameManager.Resource.Load<Material>("Actors/Halos/HaloColor");
                haloColor.color = GameManager.Data.Play.HaloColor;
            }
        }
        else
        {
            GameManager.System.PlayerActor.Camera.SwitchCamera(PersonalCameraPositionEnum.FirstView);
        }
    }

    protected virtual void InitActors()
    {

    }
}
