using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static FadeActionMode;//これがあるとAction設定のときにタイミング指定で補完が出るよ

public class FadeManager : SingletonBase<FadeManager>
{
    [SerializeField, Header("フェード速度")] private float _fadeSpeed = 1;
    [SerializeField] private GameObject _fadeCanvas;
    private FadeAndLoad _load;

    Action _beforeAction = null;
    Action _afterAction = null;
    Action _finishAction = null;

    /// <summary>
    /// 外部からActionを設定する
    /// </summary>
    /// <param name="timing">フェードのどのタイミングで実行するか</param>
    /// <param name="action">設定する関数</param>
    public void AddAction(FadeActionMode timing, Action action)
    {
        switch (timing)
        {
            case BeforeFade:
                _beforeAction += action;
                break;
            case AfterFade:
                _afterAction += action;
                break;
            case FinishFade:
                _finishAction += action;
                break;
        }
    }

    /// <summary>
    /// 外部から設定したActionを削除する
    /// </summary>
    /// <param name="timing">フェードのどのタイミングで実行するようにしたか</param>
    /// <param name="action">削除する関数</param>
    public void RemoveAction(FadeActionMode timing, Action action)
    {
        switch (timing)
        {
            case BeforeFade:
                _beforeAction -= action;
                break;
            case AfterFade:
                _afterAction -= action;
                break;
            case FinishFade:
                _finishAction -= action;
                break;
        }
    }

    protected override void Awake()
    {
        base.Awake();//SingletonBaseのAwakeを実行
        _load = new FadeAndLoad
        {
            Image = _fadeCanvas.GetComponentInChildren<Image>(),
            Speed = _fadeSpeed
        };

    }

    private void Start()
    {
        _ = FirstFade();
    }

    /// <summary>
    /// 最初のフェード
    /// </summary>
    private async UniTask FirstFade()
    {
        Debug.Log("FirstFade");
        _afterAction?.Invoke();
        await _load.FadeSystem<Enum>(-1, Color.black, Color.clear);
        _finishAction?.Invoke();

        _fadeCanvas.SetActive(false);
    }


    /// <summary>
    /// フェードを呼び出す関数
    /// </summary>
    /// <param name="sceneName">遷移先のシーンの名前</param>
    /// <param name="startOrigin">[省略可]FillOriginEnum.csのEnum 省略すると透明度フェード</param>
    /// <param name="endOrigin">[省略可]FillOriginEnum.csのEnum 省略すると透明度フェード</param>
    /// <param name="startColor">[省略可]フェード開始時の色　省略すると黒　透明度フェードなら透明</param>
    /// <param name="midColor">[省略可]画面が見えなくなった時の色　省略すると黒</param>
    /// <param name="midColor2">[省略可]画面が見えなくなったあと色をさらに変えたいときに使う</param>
    /// <param name="endColor">[省略可]フェード終了時の色　省略すると黒　透明度フェードなら透明</param>
    public async UniTask Fade<TOriginEnum>(string sceneName, TOriginEnum startOrigin = default, TOriginEnum endOrigin = default, Color startColor = default, Color midColor = default, Color midColor2 = default, Color endColor = default) where TOriginEnum : Enum
    {
        //defaultを変換
        if (startColor == default) startColor = Color.black;//色省略なら黒に
        if (startOrigin == null) startColor = new Color (startColor.r,startColor.g,startColor.b,0f);//origin省略なら透明に
        

        if (midColor == default) midColor = Color.black;//色省略なら黒に

        if (endColor == default) endColor = Color.black;//色省略なら黒に
        if (endOrigin == null) endColor = new Color(endColor.r, endColor.g, endColor.b, 0f);//origin省略なら透明に

        _fadeCanvas.SetActive(true);
        Color finalMid = midColor;

        await _load.FadeSystem(+1, startColor, midColor, startOrigin);
        _beforeAction?.Invoke();
        if (midColor2 != default)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            await _load.FadeSystem<Enum>(-1, midColor, midColor2);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            finalMid = midColor2;
        }

        await SceneManager.LoadSceneAsync(sceneName);
        _afterAction?.Invoke();

        await _load.FadeSystem(-1, finalMid, endColor, endOrigin);
        _finishAction?.Invoke();

        _fadeCanvas.SetActive(false);
    }
}