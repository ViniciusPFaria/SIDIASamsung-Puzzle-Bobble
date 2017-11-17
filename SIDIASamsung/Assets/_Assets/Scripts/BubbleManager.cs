using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField]
    private int matrixTotalSizeY, matrixTotalSizeX;

    public static BubbleManager INSTANCE//lazzy singleton
    {
        get; private set;
    }

    private void Awake()
    {
        INSTANCE = this;
        relationMatrix = new Bubble[matrixTotalSizeY, matrixTotalSizeX];
    }

    public Bubble[,] relationMatrix { get; private set; }
    
    [SerializeField]
    private GameObject bubblePrefab;
    
    public void genInitMatrix(int lines, int column)
    {
        

        for (int i = 0; i < lines; i++)//hardcode
        {
            for (int j = 0; j < column; j++)
            {
                int rand = Random.Range(0, 2);
                //rand = 1;
                if (rand == 0)
                {
                    //dar random da cor depois
                    relationMatrix[i, j] = null;
                }
                else
                {
                    SpriteRenderer bubbleSpriteRender = bubblePrefab.GetComponent<SpriteRenderer>();
                    float offSetX = 0;
                    Vector3 newBubblePos = Vector2.zero;

                    if (!(i % 2 == 0))//verifica se eh impar
                    {
                        offSetX = bubbleSpriteRender.bounds.extents.x;
                        newBubblePos.x = (float)offSetX;

                        if (j == relationMatrix.GetLength(1) - 1)
                        {
                            continue;//se for o ultimo valor, encerra o loop. Puzzle Bobble tem linhas com uma bolha a menos
                        }
                    }

                    newBubblePos.x += bubbleSpriteRender.bounds.extents.x * 2 * j;//j coluna =x
                    newBubblePos.y -= bubbleSpriteRender.bounds.extents.y * 2 * i;//i linha =y
                    newBubblePos.z = 0;

                    Bubble newBubble = Instantiate(bubblePrefab, this.transform, false).GetComponent<Bubble>();
                    newBubble.transform.localPosition = newBubblePos;

                    newBubble.posInX = j;
                    newBubble.posInY = i;
                    relationMatrix[i, j] = newBubble;
                }
            }
        }

        CleanLooseParts();
    }

    //verifica quais peças estão seguras e quias vão ser eliminadas
    public void CleanLooseParts()
    {
        ResetChainSafe();
        for (int j = 0; j < relationMatrix.GetLength(1); j++)
        {
            if (relationMatrix[0, j])
            {
                relationMatrix[0, j].isStable = true;
                relationMatrix[0, j].GetComponent<SpriteRenderer>().color = Color.green;
                StartCoroutine(relationMatrix[0, j].SafeChain());
            }
        }

        StartCoroutine(DestroyLooseParts());
    }

    private IEnumerator DestroyLooseParts()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < relationMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < relationMatrix.GetLength(1); j++)
            {
                if (relationMatrix[i, j])
                {
                    if (relationMatrix[i, j].isStable)
                    {
                        relationMatrix[i, j].GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    else
                    {
                        relationMatrix[i, j].DestryMySelf();
                    }
                }
            }
        }
    }
    public void AddBubble(int line, int column, Bubble newBubble)
    {
        //destroy if greater than the matrix
        if (line > relationMatrix.GetLength(0) || column > relationMatrix.GetLength(0))
        {
            newBubble.DestryMySelf();
            return;
        }else
        {
            line = Mathf.Max(0, line);
            column = Mathf.Max(0, column);
        }

        float offSetX = 0;

        if (!relationMatrix[line, column])
        {
            relationMatrix[line, column] = newBubble;
            Vector3 sizeBubble = newBubble.GetComponent<SpriteRenderer>().bounds.extents * 2;
            Vector2 calcPos = new Vector2(sizeBubble.x * column,
                -sizeBubble.y * line);

            if (!(line % 2 == 0))//verifica se eh impar
            {
                offSetX = sizeBubble.x / 2;
                calcPos.x += (float)offSetX;//cast to get small numbers
            }
            

            newBubble.transform.parent = this.transform;

            newBubble.transform.localPosition = calcPos;
        }
        else
        {
            print("espaco nao existe");
        }
    }

    public IEnumerator DestroyHitList(List<Bubble> destroyChainList)
    {
        yield return new WaitForSeconds(0.1f);
        if (destroyChainList.Count > 2)
        {
            foreach (Bubble bubble in destroyChainList)
            {
                bubble.DestryMySelf();
            }
        }

        destroyChainList.Clear();
        CleanLooseParts();
    }

    private void ResetChainSafe()
    {
        for (int i = 1; i < relationMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < relationMatrix.GetLength(1); j++)
            {
                if (relationMatrix[i, j])
                {
                    relationMatrix[i, j].isStable = false;
                    relationMatrix[i, j].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }

}
