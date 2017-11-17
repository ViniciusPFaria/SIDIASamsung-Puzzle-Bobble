using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _Instance;
    public static GameManager INSTANCE//lazzy singleton
    {
        get
        {
            return _Instance;
        }
    }


    private void Awake()
    {
        _Instance = this;//lazzy singleton
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        BubbleManager.INSTANCE.genInitMatrix(6,6);
    }
    

}
//impar igual e para frente, par igual e para tras