using UnityEngine;
using Unity.Services.Authentication;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public class LoginManager : MonoBehaviour
{
    public static event Action AuthenticationSucceeded;
    public string Token;
    void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        LoginWithGooglePlayGames();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LoginWithGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate((status) =>
        {
            if(status == SignInStatus.Success)
            {
                Debug.Log("Login with Google Play games successful.");
                AuthenticationSucceeded?.Invoke();

                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    Token = code;
                });
            }
            else
            {
                Debug.Log("Login Unsuccessful");
            }
        });
    }

    public async void Logout(){
        AuthenticationService.Instance.SignOut(clearCredentials: true);
        
        AuthenticationService.Instance.ClearSessionToken();
        
        Debug.Log("Logged out of game profile session.");
    }
}
