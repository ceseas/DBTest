using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInsert : MonoBehaviour
{
    public string _email;
    public string _name;
    public string _password;

	void Start ()
    {
        InsertData(_email, _name, _password);
	}
	
	void Update ()
    {
	}

    void InsertData(string email, string name, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email_post", email);
        form.AddField("name_post", name);
        form.AddField("password_post", password);

        WWW www = new WWW("http://localhost/youtube_test/userInsert.php", form);
    }
}
