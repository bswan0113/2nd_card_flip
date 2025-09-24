using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    // private const float ScaleFor6x6 = 0.7f;
    // private const float ScaleFor8x8 = 0.5f;
    [SerializeField] private float boardSpacingX = 0.7f;
    [SerializeField] private float boardSpacingY = 0.7f;
    [SerializeField] private Camera mainCamera;

    public class CameraPositionData
    {
        public Vector2 position;
        public float cameraSize;
    }

    public void CreateBoard(int[] cardDeck, int boardSize)
    {
        // float adjustRate = adjustingCardSize(boardSize);
        for (int i = 0; i < cardDeck.Length; i++)
        {
            GameObject go = Instantiate(cardPrefab, transform);
            float x = (i % boardSize) * boardSpacingX;
            float y = (i / boardSize) * boardSpacingY;
            //go.transform.position = new Vector2(x,y);
            // zoo
            StartCoroutine(EffectManager.instance.MoveCard(go, new Vector3(x, y, 1f)));
            Card cardComponent = go.GetComponent<Card>();

            if (cardComponent != null)
            {
                cardComponent.Setting(cardDeck[i]);
            }
            this.ChangeCameraView(boardSize);

        }
    }

    // private float adjustingCardSize(int boardsize)
    // {
    //     float scale = 1.0f;
    //     if (boardsize == 6)
    //     {
    //         scale = ScaleFor6x6;
    //     }
    //     else if (boardsize == 8)
    //     {
    //         scale = ScaleFor8x8;
    //     }
    //
    //     return scale;
    // }

    public void ClearBoard()
    {
        List<GameObject> childrenToDestroy = new List<GameObject>();
        foreach (Transform child in transform)
        {
            childrenToDestroy.Add(child.gameObject);
        }

        foreach (GameObject child in childrenToDestroy)
        {
            Destroy(child);
        }
    }

    void ChangeCameraView(int boardSize)
    {
        CameraPositionData data = new CameraPositionData();

        if (boardSize == 4)
        {
            data.position = new Vector2(1f,1.5f);
            data.cameraSize = 2.5f;
        }else if (boardSize == 6)
        {
            data.position = new Vector2(1.7f,2f);
            data.cameraSize = 4f;
        }else if (boardSize == 8)
        {
            data.position = new Vector2(2.5f,3f);
            data.cameraSize = 5f;
        }
        mainCamera.transform.position = data.position;
        mainCamera.orthographicSize = data.cameraSize;
    }
}