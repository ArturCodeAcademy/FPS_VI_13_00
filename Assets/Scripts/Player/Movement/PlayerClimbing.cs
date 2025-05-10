using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterGravityController))]
[RequireComponent(typeof(PlayerJump))]
public class PlayerClimbing : MonoBehaviour
{
	[field:SerializeField] public float ClimbSpeed { get; private set; } = 5f;
	[field:SerializeField] public LayerMask ClimbableLayerMask { get; private set; } = default;

	private Camera _camera;
	private CharacterController _characterController;
	private CharacterGravityController _gravityController;
	private PlayerJump _playerJump;

	public bool IsClimbing { get; private set; } = false;

	private void Awake()
	{
		_camera = Player.Instance.Camera;
		_characterController = Player.Instance.CharacterController;
		_gravityController = Player.Instance.GravityController;
		_playerJump = Player.Instance.PlayerJump;
	}

	private void Update()
    {
        if (!IsClimbing)
			return;

		_gravityController.SetVelocity(Mathf.Max(_gravityController.VerticalVelocity, 0f));

		bool isLookingUp = _camera.transform.forward.y > 0;
		Vector3 dirrection = isLookingUp ? Vector3.up : Vector3.down;
		float input = Input.GetAxis("Vertical");

		Vector3 velocity = dirrection * input * ClimbSpeed;
		_characterController.Move(velocity * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (((1 << other.gameObject.layer) & ClimbableLayerMask) != 0)
		{
			IsClimbing = true;
			_gravityController.SetVelocity(0f);
			_characterController.Move(Vector3.zero);

			_playerJump.Jumped += EndClimbing;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (((1 << other.gameObject.layer) & ClimbableLayerMask) != 0)
		{
			EndClimbing();
		}
	}

	private void EndClimbing(object sender = null, System.EventArgs e = null)
	{
		IsClimbing = false;
		_gravityController.SetVelocity(0f);
		_characterController.Move(Vector3.zero);
		
		_playerJump.Jumped -= EndClimbing;
	}
}
