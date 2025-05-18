using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public List<float> PlayerScore { get; private set; } = new();
    public List<int> PlayerTemps { get; private set; } = new();

    public int record { get; private set; } = 100;
    private int idLastRecord = -1;


    private bool someoneWon = false;

    private int numberOfRounds = 1;

    private const int NUMBER_OF_PLAYERS = 4;

    private const int WIN_POINTS_REQUIRED = 20;

    private const int SCORE_FIRST = 4;
    private const int SCORE_SECOND = 2;
    private const int SCORE_THIRD = 1;
    private const int SCORE_FOURTH = 0;

    private const int SCORE_RECORD_BEATEN = 1;

    private void Start()
    {
        InitScores();
    }

    public void InitScores()
    {
        if (PlayerScore.Count == 0)
        {
            for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
            {
                PlayerScore.Add(0);
                PlayerTemps.Add(0);
            }
        }
        else Debug.Log("ERREUR INIT SCORE !!!");
    }

    [ContextMenu("EndRound")]
    public void EndRound()
    {
        if (!someoneWon)
        {
            List<int> order = new() { 4, 3, 2, 1 };
            ApplyEndRound(order);
            numberOfRounds++;
        }
    }

    private void ApplyEndRound(List<int> orderPlayers)
    {
        for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
        {
            PlayerScore[i] += ApplyPoints(orderPlayers[i]);
            PlayerScore[i] = Mathf.Round(PlayerScore[i]);
            if (RecordBeaten(i) && orderPlayers[i] == 1)
            {
                PlayerScore[i] += SCORE_RECORD_BEATEN;
            }
            if (PlayerScore[i] >= WIN_POINTS_REQUIRED)
            {
                someoneWon = true;
                Debug.Log("player " + (i + 1) + " won !");
            }
        }
    }

    private float ApplyPoints(int orderArrived)
    {
        return orderArrived switch
        {
            1 => SCORE_FIRST * ApplyRoundScoreMulti(),
            2 => SCORE_SECOND * ApplyRoundScoreMulti(),
            3 => SCORE_THIRD * ApplyRoundScoreMulti(),
            4 => SCORE_FOURTH * ApplyRoundScoreMulti(),
            _ => 0,
        };
    }

    private float ApplyRoundScoreMulti()
    {
        if (numberOfRounds < 5) return 1f;
        if (numberOfRounds < 10) return 1.25f;
        if (numberOfRounds < 15) return 1.5f;
        return 0;
    }

    private bool RecordBeaten(int idPlayer)
    {
        if (PlayerTemps[idPlayer] < record)
        {
            PlayerTemps[idPlayer] = record;
            if (idPlayer != idLastRecord)
            {
                idLastRecord = idPlayer;
                Debug.Log("nouveau record");
                return false;
            }
        }
        Debug.Log("pas nouveau record");
        return false;
    }
}
