using UnityEngine;

public class UserData
{
    //int---------------------------------------------------------------

    public static int session
    {
        get { return PlayerPrefs.GetInt(DataKeynames.session, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.session, value); }
    }

    public static int winPvPDaily
    {
        get { return PlayerPrefs.GetInt(DataKeynames.winPvPDaily, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.winPvPDaily, value); }
    }

    public static int mergeFishQuest
    {
        get { return PlayerPrefs.GetInt(DataKeynames.mergeFishQuest, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.mergeFishQuest, value); }
    }

    public static int getUltimateCombo
    {
        get { return PlayerPrefs.GetInt(DataKeynames.getUltimateCombo, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.getUltimateCombo, value); }
    }

    public static int dayActive
    {
        get { return PlayerPrefs.GetInt(DataKeynames.dayActive, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.dayActive, value); }
    }

    public static int adShowedCount
    {
        get { return PlayerPrefs.GetInt(DataKeynames.adShowedCount, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.adShowedCount, value); }
    }

    public static int userOldScore
    {
        get { return PlayerPrefs.GetInt(DataKeynames.userOldScore, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.userOldScore, value); }
    }

    public static int userRank
    {
        get { return PlayerPrefs.GetInt(DataKeynames.userRank, 9999); }
        set { PlayerPrefs.SetInt(DataKeynames.userRank, value); }
    }

    public static int topScoreRank
    {
        get { return PlayerPrefs.GetInt(DataKeynames.topScoreRank, 1000000); }
        set { PlayerPrefs.SetInt(DataKeynames.topScoreRank, value); }
    }

    public static int firstTimeShowRemoveAds
    {
        get { return PlayerPrefs.GetInt(DataKeynames.firstTimeShowRemoveAds, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.firstTimeShowRemoveAds, value); }
    }

    public static int countCompleteLevel
    {
        get { return PlayerPrefs.GetInt(DataKeynames.countCompleteLevel, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.countCompleteLevel, value); }
    }

    public static int countPlayGame
    {
        get { return PlayerPrefs.GetInt(DataKeynames.countPlayGame, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.countPlayGame, value); }
    }

    public static int countBoosterNet
    {
        get { return PlayerPrefs.GetInt(DataKeynames.countBoosterNet, 1); }
        set { PlayerPrefs.SetInt(DataKeynames.countBoosterNet, value); }
    }

    public static int countBoosterDart
    {
        get { return PlayerPrefs.GetInt(DataKeynames.countBoosterDart, 1); }
        set { PlayerPrefs.SetInt(DataKeynames.countBoosterDart, value); }
    }

    public static int countBoosterBomb
    {
        get { return PlayerPrefs.GetInt(DataKeynames.countBoosterBomb, 1); }
        set { PlayerPrefs.SetInt(DataKeynames.countBoosterBomb, value); }
    }

    public static int countBoosterShake
    {
        get { return PlayerPrefs.GetInt(DataKeynames.countBoosterShake, 1); }
        set { PlayerPrefs.SetInt(DataKeynames.countBoosterShake, value); }
    }

    public static int countBoosterDestroy
    {
        get { return PlayerPrefs.GetInt(DataKeynames.countBoosterDestroy, 1); }
        set { PlayerPrefs.SetInt(DataKeynames.countBoosterDestroy, value); }
    }
    public static int countMergeSharks
    {
        get { return PlayerPrefs.GetInt(DataKeynames.countMergeSharks, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.countMergeSharks, value); }
    }

    public static int countMergeDolphines
    {
        get { return PlayerPrefs.GetInt(DataKeynames.countMergeDolphines, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.countMergeDolphines, value); }
    }

    public static int countMergeWhales
    {
        get { return PlayerPrefs.GetInt(DataKeynames.countMergeWhales, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.countMergeWhales, value); }
    }

    public static int curentScore
    {
        get { return PlayerPrefs.GetInt(DataKeynames.curentScore, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.curentScore, value); }
    }

    public static int countUserBooster
    {
        get { return PlayerPrefs.GetInt(DataKeynames.countUserBooster, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.countUserBooster, value); }
    }
    // planet

    public static int currentIDplanet
    {
        get { return PlayerPrefs.GetInt(DataKeynames.currentIdPlanet, 0); }
        set { PlayerPrefs.SetInt(DataKeynames.currentIdPlanet, value); }
    }

    //float--------------------------------------------------------------

    public static float friction
    {
        get { return PlayerPrefs.GetFloat(DataKeynames.friction, 0); }
        set { PlayerPrefs.SetFloat(DataKeynames.friction, value); }
    }

    public static float bounciness
    {
        get { return PlayerPrefs.GetFloat(DataKeynames.bounciness, 0); }
        set { PlayerPrefs.SetFloat(DataKeynames.bounciness, value); }
    }

    public static float gravity
    {
        get { return PlayerPrefs.GetFloat(DataKeynames.gravity, 0); }
        set { PlayerPrefs.SetFloat(DataKeynames.gravity, value); }
    }

    //bool---------------------------------------------------------------

    public static bool GetStatusCompleteDailyQuest(int indexQuest)
    {
        return PlayerPrefs.GetInt(string.Concat(DataKeynames.questComplete, indexQuest), 0) == 1;
    }

    public static void SetStatusCompleteDailyQuest(int indexQuest, bool status)
    {
        PlayerPrefs.SetInt(string.Concat(DataKeynames.questComplete, indexQuest), status ? 1 : 0);
    }

    public static bool GetStatusClaimQuest(int indexQuest)
    {
        return PlayerPrefs.GetInt(string.Concat(DataKeynames.claimQuest, indexQuest), 0) == 1;
    }

    public static void SetStatusClaimQuest(int indexQuest, bool status)
    {
        PlayerPrefs.SetInt(string.Concat(DataKeynames.claimQuest, indexQuest), status ? 1 : 0);
    }

    public static bool GetStatusCompleteAchievement(int indexAchievement, int indexlevelA)
    {
        return PlayerPrefs.GetInt(string.Concat(DataKeynames.achievementComplete, indexAchievement, indexlevelA), 0) == 1;
    }

    public static void SetStatusCompleteAchievement(int indexAchievement, int indexlevelA, bool status)
    {
        PlayerPrefs.SetInt(string.Concat(DataKeynames.achievementComplete, indexAchievement, indexlevelA), status ? 1 : 0);
    }

    public static bool GetStatusClaimAchievement(int indexAchievement, int indexlevelA)
    {
        return PlayerPrefs.GetInt(string.Concat(DataKeynames.claimAchievement, indexAchievement, indexlevelA), 0) == 1;
    }

    public static void SetStatusClaimAchievement(int indexAchievement, int indexlevelA, bool status)
    {
        PlayerPrefs.SetInt(string.Concat(DataKeynames.claimAchievement, indexAchievement, indexlevelA), status ? 1 : 0);
    }

    public static bool GetStatusFirstTimeUserBooster(int indexBooster)
    {
        return PlayerPrefs.GetInt(string.Concat(DataKeynames.firstTimeUserBooster, indexBooster), 0) == 1;
    }

    public static void SetStatusFirstTimeUserBooster(int indexBooster)
    {
        PlayerPrefs.SetInt(string.Concat(DataKeynames.firstTimeUserBooster, indexBooster), 1);
    }

    public static bool statusSound
    {
        get { return PlayerPrefs.GetInt(DataKeynames.statusSound, 1) == 1; }
        set { PlayerPrefs.SetInt(DataKeynames.statusSound, value ? 1 : 0); }
    }

    public static bool statusMusic
    {
        get { return PlayerPrefs.GetInt(DataKeynames.statusMusic, 1) == 1; }
        set { PlayerPrefs.SetInt(DataKeynames.statusMusic, value ? 1 : 0); }
    }

    public static bool statusVibrate
    {
        get { return PlayerPrefs.GetInt(DataKeynames.statusVibrate, 1) == 1; }
        set { PlayerPrefs.SetInt(DataKeynames.statusVibrate, value ? 1 : 0); }
    }

    public static bool theFirstDay
    {
        get { return PlayerPrefs.GetInt(DataKeynames.theFirstDay, 1) == 1; }
        set { PlayerPrefs.SetInt(DataKeynames.theFirstDay, value ? 1 : 0); }
    }

    public static bool isRated
    {
        get { return PlayerPrefs.GetInt(DataKeynames.isRated, 0) == 1; }
        set { PlayerPrefs.SetInt(DataKeynames.isRated, value ? 1 : 0); }
    }

    //string---------------------------------------------------------------

    public static string dataRank
    {
        get { return PlayerPrefs.GetString(DataKeynames.dataRank, string.Empty); }
        set { PlayerPrefs.SetString(DataKeynames.dataRank, value); }
    }

    public static string oldDay
    {
        get { return PlayerPrefs.GetString(DataKeynames.oldDay, string.Empty); }
        set { PlayerPrefs.SetString(DataKeynames.oldDay, value); }
    }
}
