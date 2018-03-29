using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Ranking : MonoBehaviour
{
    [Tooltip("ランキング計算に用いるスコアを入力します")]
    public int score;
    private int desplayRankingNum = 10;//ランキングを表示する数
    private int ranking = 0;
    private const int MAX_RANKINGNUMBER = 1000;
    private static int[] rankingID = new int[MAX_RANKINGNUMBER];
    private static int[] rankingScore = new int[MAX_RANKINGNUMBER];
    private static int[] rankingNum = new int[MAX_RANKINGNUMBER];

    private bool isRecordRanking = false;

    void Start()
    {
        //DeleteAllData();
        SetRanking(score);
        GetRanking();
    }
    //全てのデータを消す(PlayerPrefsを全削除するので注意)
    private void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }
    private void GetRanking()
    {
        for (int i = 0; i < desplayRankingNum; i++)
        {
            Debug.Log("No:" + rankingNum[i] + "score:" + rankingScore[i] + ":ID:" + rankingID[i]);
        }
    }

    private void SetRanking(int score)
    {
        int num = 1;
        int sameRankCounter = 0;
        int ID;
        bool isRankSet = false;

        if (PlayerPrefs.GetInt("IDNumber") == 0)
        {
            PlayerPrefs.SetInt("IDNumber", 0);
        }

        PlayerPrefs.SetInt("IDNumber", PlayerPrefs.GetInt("IDNumber") + 1);
        ID = PlayerPrefs.GetInt("IDNumber");
        PlayerPrefs.SetInt("ID" + ID + "Score", score);

        while (num <= MAX_RANKINGNUMBER)
        {

            int rankID = PlayerPrefs.GetInt("Rank" + num + "ID");
            int rankScore = PlayerPrefs.GetInt("ID" + rankID + "Score");
            if (isRankSet)
            {
                if (rankScore != score)
                {
                    ChangePrefsRanking(num - 1, sameRankCounter);
                    break;
                }
                else
                {
                    SetDisPlayRanking(rankID, num, ranking, rankScore);
                    sameRankCounter++;
                }
            }
            else if (rankScore <= score)
            {
                ranking = num;
                isRankSet = true;
                if (rankScore == score)
                {
                    sameRankCounter++;
                }
            }
            else
            {
                SetDisPlayRanking(rankID, num, num, rankScore);
            }
            num++;
        }
        SetRankID(ID, ranking);
    }


    private void ChangePrefsRanking(int rankNum, int sameCounter)
    {

        int number = MAX_RANKINGNUMBER - 1;
        while (number >= rankNum)
        {
            int rankID = PlayerPrefs.GetInt("Rank" + number + "ID");
            SetRankID(rankID, number + 1);
            number--;
        }
        number++;
        while (sameCounter > 0 && number >= 1)
        {
            int rankID = PlayerPrefs.GetInt("Rank" + number + "ID");
            SetRankID(rankID, (number + 1), ranking);
            number--;
            sameCounter--;
        }
    }
    private void SetRankID(int id, int no)
    {
        PlayerPrefs.SetInt("Rank" + no + "ID", id);
        int rankID = PlayerPrefs.GetInt("Rank" + no + "ID");
        int rankScore = PlayerPrefs.GetInt("ID" + rankID + "Score");
        SetDisPlayRanking(rankID, no, no, rankScore);
    }
    private void SetRankID(int id, int no, int ranking)
    {
        PlayerPrefs.SetInt("Rank" + no + "ID", id);
        int rankID = PlayerPrefs.GetInt("Rank" + no + "ID");
        int rankScore = PlayerPrefs.GetInt("ID" + rankID + "Score");
        SetDisPlayRanking(rankID, no, ranking, rankScore);
    }
    private void SetDisPlayRanking(int ID, int no, int ranking, int score)
    {
        rankingID[no - 1] = ID;
        rankingScore[no - 1] = score;
        rankingNum[no - 1] = ranking;
    }
}
