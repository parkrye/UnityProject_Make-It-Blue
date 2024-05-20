using UnityEngine;

public class Playable : MonoBehaviour
{
    public string Name { get; set; }
    public int HP { get; set; }
    public int SP { get; set; }

    [SerializeField]
    private Transform[] _weapons;

    private NormalAnimationController _animator;

    private void Awake()
    {
        _animator = GetComponent<NormalAnimationController>();
        if (_animator == null)
            Debug.Log(gameObject.name + " lost NormalAnimationController");
    }

    public void Equip()
    {
        foreach (var weapon in _weapons)
        {
            weapon.gameObject.SetActive(true);
        }
    }

    public void UnEquip()
    {
        foreach (var weapon in _weapons)
        {
            weapon.gameObject.SetActive(false);
        }
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
