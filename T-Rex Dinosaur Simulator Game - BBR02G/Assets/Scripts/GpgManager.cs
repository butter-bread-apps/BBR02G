using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GpgManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {


        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()

            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        if (!Social.localUser.authenticated)
        {


            Social.localUser.Authenticate((bool success) =>
            {
                Debug.Log("User logged in to GPG");
            });
        }

    }

    public void UnlockAchievement()
    {

    }


}
