namespace FadeOrigins
{
    public enum Horizontal
    {
        Left = 0,
        Right,
    }

    public enum Vertical
    {
        Bottom = 0,
        Top,
    }

    public enum Radial_90
    {
        BottomLeft = 0,
        TopLeft,
        TopRight,
        BottomRight,
    }

    public enum Radial_180
    {
        Bottom = 0,
        Left,
        Top,
        Right,
    }

    public enum Radial_360
    {
        Bottom = 0,
        Left,
        Top,
        Right,
    }
}

/// <value>BeforeFade シーン遷移前の画面が隠れた時</value>
/// <value>AfterFade シーン遷移後画面が隠れている時</value>
/// <value>FinishFade シーン遷移後画面が完全に開けた時</value>
public enum FadeActionMode
{
    BeforeFade,//シーン遷移前
    AfterFade,
    FinishFade
}
