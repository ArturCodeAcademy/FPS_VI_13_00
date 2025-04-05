using UnityEngine;
using UnityEngine.UI;

public class FlashlightUI : MonoBehaviour
{
	[SerializeField] private Image _flashlightIcon;
	[SerializeField] private Image _lightIcon;

    private Flashlight? _flashlight;

    public void SetFlashlight(Flashlight flashlight)
	{
		RemoveFlashlight();
		_flashlight = flashlight;
		_flashlight.ItemChanged += UpdateUI;
		UpdateUI(_flashlight);
	}

	public void RemoveFlashlight()
	{
		if (_flashlight is null)
			return;

		_flashlight.ItemChanged -= UpdateUI;
		_flashlight = null;
	}

	private void UpdateUI(HoldableItem _)
	{
		_flashlightIcon.color = _flashlight.IconColor;
		_lightIcon.color = _flashlight.IconColor;
		_lightIcon.gameObject.SetActive(_flashlight.IsOn);
	}
}
