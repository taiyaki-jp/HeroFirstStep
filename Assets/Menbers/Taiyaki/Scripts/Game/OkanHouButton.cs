using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class OkanHouButton : MonoBehaviour
{
    [SerializeField]private OkanHou _okanHou;
    [SerializeField] private float _chargeMax;
    private Button _button;
    private Slider _chargeSlider;
    private float _chargeValue=0;

    private void Start()
    {
        _button = this.GetComponent<Button>();
        _chargeSlider = this.GetComponentInChildren<Slider>();
        _button.onClick.AddListener(Fire);
        _button.interactable = false;
        _chargeSlider.maxValue = _chargeMax;
        _chargeSlider.value = 0;
        _ = Charge();
    }
    private void Fire()
    {
        _button.interactable = false;
        _chargeValue = 0;
        _ = _okanHou.Fire();
        _ = Charge();
    }

    private async UniTask Charge()
    {
        while (_chargeValue < _chargeMax)
        {
            _chargeValue += Time.deltaTime;
            _chargeSlider.value = _chargeValue;
            await UniTask.Yield();
        }
        _button.interactable = true;
    }
}
