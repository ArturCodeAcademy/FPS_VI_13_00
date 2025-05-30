using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterGravityController))]
[RequireComponent(typeof(PlayerCrouch))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [field:SerializeField] public LayerMask PlayerLayerMask { get; private set; }

    [field:Header("Components")]
	[field: SerializeField] public Camera Camera { get; private set; }
	[field: SerializeField] public CharacterController CharacterController { get; private set; }
	[field: SerializeField] public CharacterGravityController GravityController { get; private set; }
	[field: SerializeField] public Oxygen Oxygen { get; private set; }
	[field: SerializeField] public PlayerCrouch PlayerCrouch { get; private set; }
	[field: SerializeField] public PlayerClimbing PlayerClimbing { get; private set; }
	[field: SerializeField] public PlayerInteractor PlayerInteractor { get; private set; }
	[field: SerializeField] public PlayerItemHolder PlayerItemHolder { get; private set; }
	[field: SerializeField] public PlayerJump PlayerJump { get; private set; }
	[field: SerializeField] public PlayerView PlayerView { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(TryGetComponents))]
	private void TryGetComponents()
	{
		Camera = GetComponentInChildren<Camera>();
		CharacterController = GetComponent<CharacterController>();
		GravityController = GetComponent<CharacterGravityController>();
		Oxygen = GetComponentInChildren<Oxygen>();
		PlayerCrouch = GetComponent<PlayerCrouch>();
		PlayerClimbing = GetComponent<PlayerClimbing>();
		PlayerInteractor = GetComponentInChildren<PlayerInteractor>();
		PlayerItemHolder = GetComponentInChildren<PlayerItemHolder>();
		PlayerJump = GetComponent<PlayerJump>();
		PlayerView = GetComponentInChildren<PlayerView>();
	}

#endif
}
