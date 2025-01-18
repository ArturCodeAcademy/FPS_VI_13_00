using System;
using TMPro;
using UnityEngine;

public class ObjectsInfoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _infoText;

    [Space(3)]
    [SerializeField] private PlayerInteractor _playerInteractor;

	private IInformationContainer? _informationContainer;

	private void Awake()
	{
		UpdateUI();
	}

	private void OnEnable()
	{
		if (_playerInteractor is not null)
			_playerInteractor.ObjectWithInfoChanged += OnInformationContainerChanged;
	}

	private void OnDisable()
    {
		if (_playerInteractor is not null)
			_playerInteractor.ObjectWithInfoChanged -= OnInformationContainerChanged;
	}

	private void OnInformationContainerChanged(IInformationContainer? obj)
	{
		if (_informationContainer is not null)
			_informationContainer.StateChanged -= UpdateUI;

		_informationContainer = obj;

		if (_informationContainer is not null)
			_informationContainer.StateChanged += UpdateUI;

		UpdateUI(obj);
	}

	private void UpdateUI(object sender, EventArgs e)
	{
		UpdateUI(_informationContainer);
	}

	private void UpdateUI(IInformationContainer? obj = null)
    {
		_nameText.text = obj?.GetMainInformation() ?? string.Empty;
		_infoText.text = obj?.GetSecondaryInformation() ?? string.Empty;
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(SetParams))]
    private void SetParams()
    {
		_playerInteractor ??= GetComponentInParent<PlayerInteractor>();
        _nameText ??= transform.Find("ObjectNameText")?.GetComponent<TMP_Text>();
        _infoText ??= transform.Find("ObjectInfoText")?.GetComponent<TMP_Text>();
	}

#endif
}
