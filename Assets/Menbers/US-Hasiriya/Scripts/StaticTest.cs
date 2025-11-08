using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaticTest : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    // Start is called before the first frame update
    void Start()
    {
        _startButton.onClick.AddListener(GetName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetName()
    {
        StageController.CurrentStageNumber();
        SceneManager.LoadScene("UekusaResult");
    }
}
