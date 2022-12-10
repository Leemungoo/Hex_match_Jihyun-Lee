using UnityEngine;

namespace Board
{ 
    public class Board : MonoBehaviour
    {
        #region Cell
        [SerializeField] GameObject cellPrefab;
        public GameObject[,] allCells;

        public static float hexTileHeight = 1.02f; // ������ Cell�� �� ������ ���� �Ÿ�
        public static float sideLength; //������ Cell�� �߽�-������ ���� �Ÿ�
        Vector2 gridOffset; //ù ���� ������ ��ũ����ǥ
        int maxX; //(Axial & Cubic point) �ִ� x��
        #endregion

        #region Block
        [SerializeField] GameObject[] blockPrefab; // public GameObject[] dots;
        [SerializeField] Vector2 dropPos;
        [SerializeField] float dropSpeed;
        public GameObject[,] allBlocks;
        #endregion

        private void Start()
        {
            allBlocks = new GameObject[10, 10];

            sideLength = hexTileHeight / 2;
            gridOffset = new Vector2(0, 0);

            GenerateBoard();
        }

        private void Update()
        {
        }

        void GenerateBoard()
        {
            Cell hc;
            Block bc;

            Vector2 axialPoint = new Vector2();
            Vector2 screenPoint = new Vector2();
            maxX = 3;

            for (int x = -maxX; x <= maxX; x++)
            {
                int yMin = Mathf.Max(-maxX, -x - maxX);
                int yMax = Mathf.Min(maxX, -x + maxX);

                for (int y = yMin; y <= yMax; y++)
                {
                    axialPoint.x = x;
                    axialPoint.y = y;

                    //axial points -> ��ũ����ǥ
                    screenPoint = CoordinateHelper.AxialToScreen(axialPoint, sideLength);
                    screenPoint.x += gridOffset.x;
                    screenPoint.y += gridOffset.y;

                    //Cell ����
                    GameObject hexTile = Instantiate(cellPrefab, screenPoint, Quaternion.identity) as GameObject;
                    hexTile.name = "Cell" + x.ToString() + ", " + y.ToString();
                    hc = hexTile.GetComponent<Cell>();
                    //��ȯ�� axial coordinate ���� Cell.axicalCoordinate ����� ����
                    hc.axialCoordinate = axialPoint;

                    //�� ����
                    int colorRange = Random.Range(0, blockPrefab.Length);
                    GameObject block = Instantiate(blockPrefab[colorRange], screenPoint, Quaternion.identity) as GameObject;
                    bc = block.GetComponent<Block>();
                    allBlocks[x+5, y+5] = block;
                    bc.axialCoordinate = axialPoint;
                }
            }
        }
    }
}
