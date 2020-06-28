using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class Medal
{
    public Sprite Image;
    public int MinimumPoints;
}

public class GameoverScore : MonoBehaviour
{
    public Text Score;
    public Image Medal;
    public GameObject Record;

    public Medal[] Medals;

	void Start ()
    {
        RefreshPoints();
        RefreshMedal();
        RefreshRecord();
    }

    int GetCurrentPoints()
    {
        return PlayerPrefs.GetInt("current_points", 0);
    }

    void RefreshPoints()
    {
        var points = GetCurrentPoints();
        Score.text = points + " points!";
    }

    void RefreshMedal()
    {
        var points = GetCurrentPoints();

        Medal.sprite = Medals
            .Where(medal => medal.MinimumPoints <= points)
            .OrderBy(medal => medal.MinimumPoints)
            .Last()
            .Image;
    }

    void RefreshRecord()
    {
        var currentPoints = GetCurrentPoints();
        var recordPoints = PlayerPrefs.GetInt("record_points", 0);

        bool isRecord = (currentPoints > recordPoints);

        if (isRecord)
            PlayerPrefs.SetInt("record_points", currentPoints);

        Record.SetActive(isRecord);
        Debug.Log(currentPoints + " / " + recordPoints);
    }
}
