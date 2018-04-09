using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour
{

    public string _name;
    public string _email;
    public string _password;

	void Start ()
    {
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(TryLogin(_name, _email, _password));
        }
    }

    private IEnumerator TryLogin(string name, string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("name_post", name);
        form.AddField("email_post", email);
        form.AddField("password_post", password);

        WWW www = new WWW("http://localhost/youtube_test/login.php", form);
        yield return www;

        Debug.Log(www.text);
    } 

}
