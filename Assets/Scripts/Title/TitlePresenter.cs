using UnityEngine;

public class TitlePresenter : MonoBehaviour
{
    private async void Start()
    {
        await SoundManager.Init();
        SoundManager.Instance.PlayBGM(BGMTypeEnum.Title);
    }
}
