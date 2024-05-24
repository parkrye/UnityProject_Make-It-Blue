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

    private void Update()
    {
        if (_turnInput != Vector3.zero)
        {
            if (Actor.Focus.localPosition.y < 0f)
            {
                if (_turnInput.x < 0f)
                    _turnInput.x = 0f;
            }
            else if (Actor.Focus.localPosition.y > 2f)
            {
                if (_turnInput.x > 0f)
                    _turnInput.x = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_moveInput != Vector3.zero)
            Controller.Move(Actor.MoveSpeed * _moveInput * Time.fixedDeltaTime);

        if (_turnInput != Vector3.zero)
        {
            transform.Rotate(Actor.TurnSpeed * Vector3.up * _turnInput.y * Time.fixedDeltaTime);
            Actor.Focus.Translate(Actor.TurnSpeed * Vector3.up * _turnInput.x * Time.fixedDeltaTime);
        }
    }

    public void Move(Vector2 input)
    {
        _moveInput = transform.forward * input.y + transform.right * input.x;
    }

    public void Turn(Vector2 input)
    {
        _turnInput = Vector3.up * input.x + Vector3.right * input.y * 0.1f;
    }

    public void Action(BaseAction action)
    {
        action.Action();
    }
}
