using UnityEngine;

public class ItemsUI : MonoBehaviour
{
	[SerializeField] private FlashlightUI _flashlightUI;

	private PlayerItemHolder _playerItemHolder;

	private void Awake()
	{
		_playerItemHolder = Player.Instance.PlayerItemHolder;

		_flashlightUI.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		_playerItemHolder.OnItemChanged += UpdateUI;
	}

	private void OnDisable()
	{
		_playerItemHolder.OnItemChanged -= UpdateUI;
	}

	private void UpdateUI(HoldableItem item)
	{
		_flashlightUI.gameObject.SetActive(false);
		_flashlightUI.RemoveFlashlight();

		switch (item)
		{
			case Flashlight flashlight:
				_flashlightUI.gameObject.SetActive(true);
				_flashlightUI.SetFlashlight(flashlight);
				break;
		}
	}
}
