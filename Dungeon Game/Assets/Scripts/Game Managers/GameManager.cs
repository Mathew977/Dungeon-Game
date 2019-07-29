using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Source: https://github.com/UnityCommunity/UnitySingleton
public class GameManager : Singleton<GameManager>
{
	
	[SerializeField]
	protected string m_PlayerName;

    //Indicates if it is game over. The set is private to this class.
    public bool IsGameOver { get; private set; }

	protected virtual void Start ()
	{
		// Start your main scene
		// SceneManager.LoadScene ( "Main Menu" );
	}

	public string GetPlayerName ()
	{
		return m_PlayerName;
	}
	
    public void DoGameOver()
    {
        IsGameOver = true;
        StopAllCoroutines();
    }
}
