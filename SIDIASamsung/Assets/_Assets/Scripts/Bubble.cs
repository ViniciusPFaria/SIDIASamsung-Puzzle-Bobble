using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    public int myType = 0;
    public System.Action stopped;

    public int posInX;
    public int posInY;
    [SerializeField]
    private List<Sprite> listSprites;

    private bool alreadyChecked = false;
    private bool cacheStability = false;

    public bool isStable = false;
    private static List<Bubble> destroyChainList = new List<Bubble>();
    // Use this for initialization
    void Start()
    {
        myType = Random.Range(0, 4);

        switch (myType)
        {
            case 0: GetComponent<SpriteRenderer>().sprite = listSprites[0]; break;
            case 1: GetComponent<SpriteRenderer>().sprite = listSprites[1]; break;
            case 2: GetComponent<SpriteRenderer>().sprite = listSprites[2]; break;
            case 3: GetComponent<SpriteRenderer>().sprite = listSprites[3]; break;
        }

    }

    public void DestryMySelf()
    {
        Destroy(gameObject, 1);
        BubbleManager.INSTANCE.relationMatrix[posInY, posInX].GetComponent<SpriteRenderer>().color = Color.red;
        BubbleManager.INSTANCE.relationMatrix[posInY, posInX].isStable = false;
        BubbleManager.INSTANCE.relationMatrix[posInY, posInX] = null;

        GetComponentInChildren<ParticleSystem>().Play();

    }

    public IEnumerator SafeChain()
    {

        Bubble[,] cacheRelationMatrix = BubbleManager.INSTANCE.relationMatrix;


        for (int yDirection = posInY - 1; yDirection <= posInY + 1; yDirection++)
        {
            for (int xDirection = posInX - 1; xDirection <= posInX + 1; xDirection++)
            {


                int StabelI = Mathf.Min(Mathf.Max(0, yDirection), cacheRelationMatrix.GetLength(0) - 1);//garantia que não vai estoura o index
                int StabelJ = Mathf.Min(Mathf.Max(0, xDirection), cacheRelationMatrix.GetLength(1) - 1);

                if (!cacheRelationMatrix[StabelI, StabelJ])// se não tiver obj pula
                    continue;

                if (StabelI % 2 == 0)//Verifica se é par
                {
                    if (xDirection - posInX == -1 && yDirection - posInY != 0)//não ir para a esquerda quando é par
                    {
                        continue;
                    }
                }
                else if (xDirection - posInX == 1 && yDirection - posInY != 0)
                {
                    continue;
                }


                if (cacheRelationMatrix[StabelI, StabelJ].isStable == false)
                {
                    cacheRelationMatrix[StabelI, StabelJ].isStable = true;
                    cacheRelationMatrix[StabelI, StabelJ].GetComponent<SpriteRenderer>().color = Color.green;
                    yield return new WaitForSeconds(0.12f);
                    StartCoroutine(cacheRelationMatrix[StabelI, StabelJ].SafeChain());

                }
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("CanKick"))//tag to rebound
        {
            print("wall");
            return;
        }

        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();

        if (rigidbody2D.bodyType == RigidbodyType2D.Kinematic)//if it is already stopped
        {
            print("kinematic");
            return;
        }

        if (stopped != null)//avisa que parou para quem estiver escultando
            stopped();

        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        rigidbody2D.velocity = Vector2.zero;
        Vector2 dir = collision.transform.position - transform.position;
        Bubble collisionBubble = collision.transform.GetComponent<Bubble>();

        if (dir.x > 0.01f && collisionBubble.posInY % 2 == 0)
        {
            dir.x = -1;
        }
        else if (dir.x < -0.01f && collisionBubble.posInY % 2 != 0)
        {
            dir.x = 1;
        }

        if (dir.y > 0.01f)
        {
            dir.y = 1;
        }
        else if (dir.y < -0.01f)
        {
            dir.y = -1;
        }

        //define a nova posição baseado na da bola atingida
        posInX = collisionBubble.posInX + (int)dir.x;
        posInY = collisionBubble.posInY + (int)dir.y;
        BubbleManager.INSTANCE.AddBubble(posInY, posInX, this);

        //inicia a lista de destruicao
        destroyChainList.Add(this);

        StartCoroutine(GetHitList());
        StartCoroutine(BubbleManager.INSTANCE.DestroyHitList(destroyChainList));

    }

    //This function is responsible for finding the interconnected bubbles
    public IEnumerator GetHitList()
    {
        Bubble[,] cacheRelationMatrix = BubbleManager.INSTANCE.relationMatrix;

        //percore a lista para todos os lados para encontrar bolhas do mesmo tipo
        for (int yDirection = posInY - 1; yDirection <= posInY + 1; yDirection++)
        {
            for (int xDirection = posInX - 1; xDirection <= posInX + 1; xDirection++)
            {
                int StabelI = Mathf.Min(Mathf.Max(0, yDirection), cacheRelationMatrix.GetLength(0) - 1);//garantia que não vai estoura o index
                int StabelJ = Mathf.Min(Mathf.Max(0, xDirection), cacheRelationMatrix.GetLength(1) - 1);

                if (!cacheRelationMatrix[StabelI, StabelJ] || cacheRelationMatrix[StabelI, StabelJ] == this)// se não tiver obj ou for eu mesmo, pula
                    continue;

                if (StabelI % 2 == 0)//Verifica se é par
                {
                    if (xDirection - posInX == -1 && yDirection - posInY != 0)//não ir para a esquerda quando é par
                    {
                        continue;
                    }
                }
                else if (xDirection - posInX == 1 && yDirection - posInY != 0)
                {
                    continue;
                }

                //if is my type and it's not on the list yet
                if (cacheRelationMatrix[StabelI, StabelJ].myType == myType
                    && !destroyChainList.Contains(cacheRelationMatrix[StabelI, StabelJ]))
                {
                    destroyChainList.Add(cacheRelationMatrix[StabelI, StabelJ]);

                    yield return new WaitForSeconds(0.01f);
                    StartCoroutine(cacheRelationMatrix[StabelI, StabelJ].GetHitList());

                }
            }
        }
    }
    
}