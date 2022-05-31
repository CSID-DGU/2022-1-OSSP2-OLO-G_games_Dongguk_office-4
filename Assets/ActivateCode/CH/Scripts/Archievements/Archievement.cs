using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Archievement
{
    // 도전과제 메타데이터
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public string Hint { get; protected set; }

    // 도전과제 달성 여부
    public bool Archieved { get; protected set; } = false;

    // 도전과제 달성률
    // (모든 도전과제는 정수 카운트 형태라고 가정 (1회성은 1번 카운트))
    public int Goal { get; protected set; }
    public int Progress { get; protected set; } = 0;

    // 보상 이벤트 콜백
    public event Action RewardCallbacks;

    // 생성자
    public Archievement(string title, string description, string hint, int goal)
    {
        this.Title = title;
        this.Description = description;
        this.Hint = hint;
        this.Goal = goal;
    }

    // 달성율 증가
    public bool IncreaseProgress(int amount = 1)
    {
        this.Progress += amount;

        this.Archieved = this.Progress >= Goal;
        // 달성시 콜백s 실행
        if (this.Archieved)
            this.RewardCallbacks.Invoke();

        return this.Archieved;
    }

    //// 달성율 감소
    //public bool DecreaseProgress(int amount = 1)
    //{
    //    this.Progress -= this.amount;
    //    this.Progress = this.Progress < 0 ? 0 : this.Progress;

    //    return this.Archieved = this.Progress >= this.Goal;
    //}
}
