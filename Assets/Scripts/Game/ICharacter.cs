using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    /// <returns>string:名前</returns>
    public string Name { get; }

    /// <returns>int:攻撃力</returns>
    public int Attack { get; }

    /// <returns>int:現在HP</returns>
    public int HP { get; }

    /// <returns>SpriteRenderer:そのキャラのSpriteRenderer</returns>
    public SpriteRenderer CharaRenderer { get; }

    /// <returns>bool:プレイヤーかどうか</returns>
    public bool IsPlayer { get; }

    /// <value>遷移先のState</value>
    /// <remarks>setなので実は中身がnullでも良い</remarks>
    public CharacterState State { set; }

    /// <returns>int:攻撃のタイミング</returns>
    /// <remarks>0~3</remarks>
    public int AttackTiming { get; }

    /// <returns>ResistanceData :耐性の有無(Bool)</returns>
    public ResistanceData ResistData { get; }

    /// <summary>
    /// そのキャラにダメージを与える
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    public void DoDamage(int damage);

    /// <summary>
    /// そのキャラをスタンさせる
    /// </summary>
    /// <param name="stanTime"></param>
    public void DoStan(float stanTime);
}

public enum CharacterState
{
    Death,
    Walk,
    Battle,
    Knockback,
}

[Serializable]
public class ResistanceData
{
    public bool Stun = false;
    public bool KnockBack = false;
}