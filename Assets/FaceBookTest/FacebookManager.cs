using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Facebook.Unity;

public class FacebookManager : MonoBehaviour
{
    public LoggedinCanvas _loggedinCanvas;
    public NotLoggedinCanvas _notLoggedinCanvas;

    private void Awake()
    {
        _loggedinCanvas.gameObject.SetActive(false);
        _notLoggedinCanvas.gameObject.SetActive(false);

        if (!FB.IsInitialized)
        {
            FB.Init(InitCompleteCallback);

            _notLoggedinCanvas.gameObject.SetActive(true);
        }
    }

    void Start ()
    {
	}
	
	void Update ()
    {
	}

    public void Login()
    {
        if (!FB.IsLoggedIn)
        {
            FB.LogInWithReadPermissions(new List<string> { "user_friends" }, LoginCallback);
        }
    }

    void InitCompleteCallback()
    {
        Debug.Log("FB Init successfully");
    }

    void LoginCallback(ILoginResult result)
    {
        if (result.Error == null)
        {
            Debug.Log("login success");
            _notLoggedinCanvas.gameObject.SetActive(false);
            _loggedinCanvas.gameObject.SetActive(true);

            FB.API("me/picture?width=100&height=100", HttpMethod.GET, ProfilePictureCallback);
            FB.API("me?fields=first_name", HttpMethod.GET, ProfileNameCallback);
        }
        else
        {
            Debug.Log("login failed " + result.Error);
        }
    }

    public void Logout()
    {
        FB.LogOut();
        _notLoggedinCanvas.gameObject.SetActive(true);
        _loggedinCanvas.gameObject.SetActive(false);
    }

    private void ProfilePictureCallback(IGraphResult result)
    {
        Texture2D image = result.Texture;
        _loggedinCanvas._playerProfileImage.sprite = Sprite.Create(image, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
    }

    private void ProfileNameCallback(IGraphResult result)
    {
        IDictionary<string, object> profile = result.ResultDictionary;
        _loggedinCanvas._playerNameText.text = "Hello " + profile["first_name"];
    }
}
