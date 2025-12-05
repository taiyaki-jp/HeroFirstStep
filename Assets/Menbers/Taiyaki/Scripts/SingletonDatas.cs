/// <summary>
/// シーンを超えて各変数を保持するためのスクリプト
/// </summary>
public class SingletonDatas : SingletonBase<SingletonDatas>
{
    public bool IsWin{get;set;}
    protected override void Awake()
    {
        base.Awake();
       IsWin = false;
    }
}
