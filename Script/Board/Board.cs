using UnityEngine;

namespace Board
{ 
    public class Board : MonoBehaviour
    {
        #region Cell
        [SerializeField] GameObject cellPrefab;
        public GameObject[,] allCells;

        public static float hexTileHeight = 1.02f; // 육각형 Cell의 두 꼭지점 사이 거리
        public static float sideLength; //육각형 Cell의 중심-꼭지점 사이 거리
        Vector2 gridOffset; //첫 셀을 생성할 스크린좌표
        int maxX; //(Axial & Cubic point) 최대 x값
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

                    //axial points -> 스크린좌표
                    screenPoint = CoordinateHelper.AxialToScreen(axialPoint, sideLength);
                    screenPoint.x += gridOffset.x;
                    screenPoint.y += gridOffset.y;

                    //Cell 생성
                    GameObject hexTile = Instantiate(cellPrefab, screenPoint, Quaternion.identity) as GameObject;
                    hexTile.name = "Cell" + x.ToString() + ", " + y.ToString();
                    hc = hexTile.GetComponent<Cell>();
                    //변환된 axial coordinate 값을 Cell.axicalCoordinate 멤버로 저장
                    hc.axialCoordinate = axialPoint;

                    //블럭 생성
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
