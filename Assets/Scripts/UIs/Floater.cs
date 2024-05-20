using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Linq;

public class Floater : BaseUI
{
    private CinemachineBrain _mainCamera;

    public override async UniTask OnInit()
    {
        await base.OnInit();
        _mainCamera = FindObjectsOfType<CinemachineBrain>().FirstOrDefault();
    }

    private void LateUpdate()
    {
        if (_mainCamera == null)
            return;

        transform.LookAt(transform.position + _mainCamera.transform.forward);
    }
}