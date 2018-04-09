using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Facebook.Unity;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.OurUtils;

using Amazon;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentity.Model;
using Amazon.CognitoSync;
using Amazon.CognitoSync.SyncManager;

public class CognitoLogin : MonoBehaviour
{
    private Dataset _playerInfo;

    public string _identityPoolId;

    public string _region = RegionEndpoint.USEast1.SystemName;
    private RegionEndpoint Region
    {
        get { return RegionEndpoint.GetBySystemName(_region); }
    }

    private CognitoAWSCredentials _credentials;
    private CognitoAWSCredentials Credentials
    {
        get
        {
            if (_credentials == null)
            {
                _credentials = new CognitoAWSCredentials(_identityPoolId, Region);
            }
            return _credentials;
        }
    }

    private CognitoSyncManager _syncManager;
    private CognitoSyncManager SyncManager
    {
        get
        {
            if (_syncManager == null)
            {
                _syncManager = new CognitoSyncManager(Credentials, new AmazonCognitoSyncConfig { RegionEndpoint = Region });
            }
            return _syncManager;
        }
    }

	void Start ()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        _playerInfo = SyncManager.OpenOrCreateDataset("Player1");

        _playerInfo.OnSyncSuccess += OnSyncSuccess;
        _playerInfo.OnSyncFailure += OnSyncFailed;

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration();

        if (!FB.IsInitialized)
        {
            FB.Init(()=> { Debug.Log("Facebook Init successfully"); } );
        }
	}

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _playerInfo.Put("name", "chisu");
            _playerInfo.Put("progress", "1");
            _playerInfo.SynchronizeAsync();
        }
    }


    public void FacebookLogin()
    {
        if (!FB.IsLoggedIn)
        {
            FB.LogInWithReadPermissions(new List<string> { "user_friends" }, 
                                        (ILoginResult result)=>
                                        {
                                            if (result.Error == null)
                                            {
                                                //Credentials.AddLogin("graph.facebook.com", result.AccessToken.TokenString);
                                            }
                                            else
                                            {
                                                Debug.Log("Login failed");
                                            }
                                        });
        }
    }

    public void OnSyncSuccess(object sender, SyncSuccessEventArgs args)
    {
        Debug.Log("sync success");
    }

    public void OnSyncFailed(object sender, SyncFailureEventArgs args)
    {
        Debug.Log("sync failed");
    }

}
