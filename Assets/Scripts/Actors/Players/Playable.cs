using UnityEngine;

public class Playable : MonoBehaviour
{
    [SerializeField]
    private Transform _rightHand, _leftHand;

    private NormalAnimationController _animator;

    private void Awake()
    {
        _animator = GetComponent<NormalAnimationController>();
        if (_animator == null)
            Debug.Log(gameObject.name + " lost NormalAnimationController");
    }

    public Transform GetHand(bool isRightHand)
    {
        if (isRightHand)
            return _rightHand;
        return _leftHand;
    }

    public void PlayMoveAnimation(float speed)
    {
        _animator.PlayMoveAnimation(speed);
    }

    public void PlayActionAnimation(int index)
    {
        _animator.PlayActionAnimation(index);
    }
}
