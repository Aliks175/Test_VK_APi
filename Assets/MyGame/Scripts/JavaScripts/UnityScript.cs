using System.Runtime.InteropServices;
using UnityEngine;

public class UnityScript : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void UnityPluginRequestJs();

    [DllImport("__Internal")]
    private static extern void UnityPluginSubJs();

    public GameObject goControl;


    public void RequestJs() // вызываем из событий unity
    {
        UnityPluginRequestJs();

        UnityPluginSubJs();
    }

    public void ResponseOk()
    {
        goControl.GetComponent<Control>().ResponseFromJsOk();
    }
    public void ResponseError()
    {
        goControl.GetComponent<Control>().ResponseFromJsError();
    }
}
