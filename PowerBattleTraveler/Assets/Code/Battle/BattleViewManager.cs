﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;

namespace Battle {

/// <summary>
/// バトルの表示を各ビューに伝える
/// </summary>
public class BattleViewManager : MonoBehaviour
{
    [SerializeField] private TurnRootView m_TurnRootView = default;     //< ターンのビュー

    [SerializeField] private BattleMenuRootView m_BattleMenuRootView = default;      //< 戦闘メニュー
    /// <summary>
    /// バトルコマンドメニュー取得
    /// </summary>
    public BattleMenuView GetBattleMenu() { return m_BattleMenuRootView.BattleMenu; }

    [SerializeField] private BattleAllyInfoRootView m_BattleAllyInfoRootView = default;  //< 味方のビュー

    [SerializeField] private ActorsRootView m_ActorsRootView = default;  //< キャラクターのビュー

    public ActorsRootView GetActorsView() {return m_ActorsRootView;}

    /// <summary>
    /// Viewの初期化
    /// </summary>
    public async UniTask SetupView(BattleDataManager battleData)
    {
        await UniTask.Yield();

        // バトルメニュー
        m_BattleMenuRootView.SetupView();

        // キャラのView初期化
        foreach (var actor in battleData.Actors)
        {
            Debug.Log(actor.Key);
            m_ActorsRootView.AddActor(actor);
            
            if (actor.Value.ActorType == ActorType.PLAYER) {
                m_BattleAllyInfoRootView.AddAlly(actor);
            }
        }

        // ターン表記の初期化
        m_TurnRootView.SetupView();
    }











    public async UniTask AnimationAsync()
    {
        Debug.Log("演出開始");

        await SomeAnim();

        Debug.Log("演出終了");
    }

    public async UniTask SomeAnim()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(4));
        Debug.Log("1");
    }

    public void setTurn(int turn)
    {
        m_TurnRootView.SetTurn(turn);
    }
}
} // Battle
