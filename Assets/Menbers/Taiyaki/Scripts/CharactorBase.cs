using System.Threading;
using UnityEngine;

public class CharactorBase : MonoBehaviour
{
    [SerializeField] private int _attack;
    public int Attack { get => _attack; }
    [SerializeField] private int _hp;
    public int HP { get => _hp; set => _hp = value; }
    [SerializeField] private int _moveSpeed;
}
