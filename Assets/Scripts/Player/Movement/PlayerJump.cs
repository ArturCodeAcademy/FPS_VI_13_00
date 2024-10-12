using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterGravityController))]
public class PlayerJump : MonoBehaviour
{
    [Header("Params")]
	[SerializeField, Min(0.5f)] private float _jumpHeight = 1f;
	[SerializeField, Min(1)] private int _jumpCount = 2;
	[SerializeField] private float _minVelocityToJump = -0.5f;

	private CharacterGravityController _gravityController;

	private int _jumpLeft;
	private float _jumpVelocity;

	private void Awake()
	{
		_gravityController = Player.Instance.GravityController;
	}

	private void Start()
	{
		RefreshJumpsCount();
		_jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * _jumpHeight);
	}

	private void Update()
	{
		if (   Input.GetButtonDown("Jump")
			&& _jumpLeft > 0
			&& _gravityController.VerticalVelocity > _minVelocityToJump)
		{
			_gravityController.SetVelocity(_jumpVelocity);
			_jumpLeft--;
		}
	}

	private void OnEnable()
	{
		_gravityController.Landed += RefreshJumpsCount;
	}

	private void OnDisable()
	{
		_gravityController.Landed -= RefreshJumpsCount;
	}

	private void RefreshJumpsCount(object sender = null, LandEventArgs e = null)
	{
		_jumpLeft = _jumpCount;
	}
}
