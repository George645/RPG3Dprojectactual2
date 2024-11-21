using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    public void Start()
    {
        dumbthing();
    }
    IEnumerable dumbthing()
    {
        for (int i = 0; i<5; i++)
        {
            transform.Translate(new Vector3(-1, 0) * 5 * Time.deltaTime);
            yield return null;
            transform.Translate(new Vector3(1, 0) * 5 * Time.deltaTime);
            yield return null;
            yield return null;
        }
    }
}
