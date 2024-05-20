using UnityEngine;

public class PlayerActorController : MonoBehaviour
{
    private PlayerActor Actor;
    private CharacterController Controller;

    private Vector3 _moveInput, _turnInput;

    private void Awake()
    {
        Actor = GetComponent<PlayerActor>();
        if (Actor == null)
            Debug.Log(gameObject.name + " lost PlayerActor");
        Controller = GetComponent<CharacterController>();
        if (Controller == null)
            Debug.Log(gameObject.name + " lost CharacterController");
    }

    private void FixedUpdate()
    {
        if (_moveInput != Vector3.zero)
            Controller.Move(Actor.MoveSpeed * _moveInput * Time.fixedDeltaTime);

        if (_turnInput != Vector3.zero)
            transform.Rotate(Actor.TurnSpeed * _turnInput * Time.fixedDeltaTime);
    }

    public void Move(Vector2 input)
    {
        _moveInput = transform.forward * input.y + transform.right * input.x;
    }

    public void Turn(Vector2 input)
    {
        _turnInput = Vector3.up * input.x;
    }
}
