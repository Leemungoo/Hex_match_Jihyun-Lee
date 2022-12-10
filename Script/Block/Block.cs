using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Board
{ 
    public class Block : MonoBehaviour
    {
        public Vector2 axialCoordinate = new Vector2();
        private Board board;
        private GameObject otherBlock;
        
        //axial
        public int x; 
        public int y;
        public int targetX;
        public int targetY;

        //screen
        public float screenX;
        public float screenY;
        public float targetScreenX;
        public float targetScreenY;

        Vector2 axialPT;
        Vector2 screenPT;

        public float sideLength = Board.sideLength; 

        private Vector2 firstTouchPosition;
        private Vector2 finalTouchPosition;
        private Vector2 tempPosition;
        public float swipeAngle = 0;

        private void Start()
        {
            board = FindObjectOfType<Board>();

            screenX = transform.position.x;
            screenY = transform.position.y;

            screenPT = new Vector2(screenX, screenY);
            axialPT = CoordinateHelper.screenToAxial(screenPT, sideLength);
            Debug.Log($"Start ScreenPT = {screenPT}");
            Debug.Log($"Start AxialPT = {axialPT}");
        }

        private void Update()
        {
        }

        private void OnMouseDown()
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스 입력좌표를 월드좌표로 변환
            Debug.Log($"{firstTouchPosition}");
        }

        private void OnMouseUp()
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }

        void CalculateAngle()
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180/Mathf.PI; // radian -> degree 전환
            Debug.Log($"{swipeAngle}");

            MoveBlocks();
        }

        void MoveBlocks()
        {
            if(swipeAngle > 0 && swipeAngle <= 60) // 북동
            {
                otherBlock = board.allBlocks[x, y+1];
                otherBlock.GetComponent<Block>().y -= 1;
                y += 1;
                Debug.Log("NE");
            }
            else if(swipeAngle > 60 && swipeAngle <= 120) // 북
            {
                otherBlock = board.allBlocks[x+1, y];
                otherBlock.GetComponent<Block>().x -= 1;
                x += 1;
                Debug.Log("N");
            }
            else if (swipeAngle > 120 && swipeAngle <= 180) // 북서
            {
                otherBlock = board.allBlocks[x+1, y-1];
                otherBlock.GetComponent<Block>().x -= 1;
                otherBlock.GetComponent<Block>().y += 1;
                x += 1;
                y -= 1;
                Debug.Log("NW");
            }
            else if (swipeAngle > -180 && swipeAngle <= -90) // 남서
            {
                otherBlock = board.allBlocks[x, y-1];
                otherBlock.GetComponent<Block>().y += 1;
                y -= 1;
                Debug.Log("SW");
            }
            else if (swipeAngle > -120 && swipeAngle <= -60) // 남
            {
                otherBlock = board.allBlocks[x-1, y];
                otherBlock.GetComponent<Block>().x += 1;
                x -= 1;
                Debug.Log("N");
            }
            else if (swipeAngle > -60 && swipeAngle <= 0) // 남동
            {
                otherBlock = board.allBlocks[x-1, y+1];
                otherBlock.GetComponent<Block>().x += 1;
                otherBlock.GetComponent<Block>().y -= 1;
                x -= 1;
                y += 1;
                Debug.Log("SE");
            }
        }
    }
}
