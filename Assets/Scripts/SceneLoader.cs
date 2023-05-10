using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Cargar escena
    public void LoadScene(int sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    //Cerrar aplicaci�n
    public void closeGame()
    {
        Application.Quit();
    }
}