using UnityEngine;
using TMPro;

public class TextChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultText;
    private string stageName;

    // Start is called before the first frame update
    void Start()
    {
        stageName = StageController.stageName;
    }

    // Update is called once per frame
    void Update()
    {
        resultText.text = stageName;
    }

}
