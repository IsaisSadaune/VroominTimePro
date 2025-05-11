using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public List<int> PlayerScore { get; private set; }
    private bool someoneWon = false;



    private const int WINPOINTSREQUIRED = 10;
    private const int NUMBEROFPLAYERS = 4;

    private const int SCOREFORFIRST = 3;
    private const int SCOREFORSECOND = 2;
    private const int SCOREFORTHIRD = 1;
    private const int SCOREFORFOURTH = 0;


    private void Start()
    {
        InitScores();
    }


    public void InitScores()
    {
        if (PlayerScore.Count == 0)
        {
            for (int i = 0; i < NUMBEROFPLAYERS; i++)
            {
                PlayerScore.Add(0);
            }
        }
        else Debug.Log("ERREUR INITIALIZATION SCORE !!!");
    }
    private void SetWinner(int id)
    {
        someoneWon = true;
        id++;
        Debug.Log("player n°" + id + " wins !");
    }

    #region resultCourse
    private void FirstArrived(int id)
    {
        PlayerScore[id] += SCOREFORFIRST;
        if (EnoughToWin(PlayerScore[id])) SetWinner(id);
    }
    private void SecondArrived(int id)
    {
        PlayerScore[id] += SCOREFORSECOND;
        if (EnoughToWin(PlayerScore[id])) SetWinner(id);
    }
    private void ThirdArrived(int id)
    {
        PlayerScore[id] += SCOREFORTHIRD;
        if (EnoughToWin(PlayerScore[id])) SetWinner(id);
    }
    private void FourthArrived(int id)
    {
        PlayerScore[id] += SCOREFORFOURTH;
        if (EnoughToWin(PlayerScore[id])) SetWinner(id);
    }

    public void ResultCourse(int firstId)
    {
        FirstArrived(firstId);
    }
    public void ResultCourse(int firstId, int secondId)
    {
        FirstArrived(firstId);
        SecondArrived(secondId);
    }
    public void ResultCourse(int firstId, int secondId, int thirdId)
    {
        FirstArrived(firstId);
        SecondArrived(secondId);
        ThirdArrived(thirdId);
    }
    public void ResultCourse(int firstId, int secondId, int thirdId, int fourthId)
    {
        FirstArrived(firstId);
        SecondArrived(secondId);
        ThirdArrived(thirdId);
        FourthArrived(fourthId);
    }
    #endregion

    [ContextMenu("ResultCourseTest4Players")]
    public void ResultTest() => ResultCourse(0, 1, 2, 3);

    #region utilePlusTard
    private int GetFirst() => PlayerScore.Max();
    private int GetSecond() => PlayerScore.Where(x => x < GetFirst()).Max();
    private int GetThird() => PlayerScore.Where(x => x > GetLast()).Min();
    private int GetLast() => PlayerScore.Min();
    #endregion

    private static bool EnoughToWin(int score) => score >= WINPOINTSREQUIRED;

    [ContextMenu("ResetScores")]
    public void ResetScores()
    {
        someoneWon = false;
        PlayerScore.Clear();
        InitScores();
    }
}
