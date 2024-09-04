using System;
using DefaultNamespace.Enums;
using UnityEngine;

namespace Tile
{
    public class GameTile : MonoBehaviour
    {
        public GameTile nextGameTile;
        public TileType tileType;
        public SpriteRenderer image;
        public Transform nextArrow;

        public GameTile upTile;
        public GameTile downTile;
        public GameTile leftTile;
        public GameTile rightTile;

        public float rayDistance = 1f;
        public LayerMask tileLayerMask;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            RotateArrow();
            DetectTiles();
            AssignColorBaseOnType();
        }

        private void RotateArrow()
        {
            var direction = nextGameTile.transform.position - transform.position;

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            nextArrow.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private void DetectTiles()
        {
            Vector2 upDirection = Vector2.up;
            Vector2 downDirection = Vector2.down;
            Vector2 leftDirection = Vector2.left;
            Vector2 rightDirection = Vector2.right;


            RaycastHit2D hitUp = Physics2D.Raycast((Vector2)transform.position + upDirection, upDirection, rayDistance,
                tileLayerMask);
            RaycastHit2D hitDown = Physics2D.Raycast((Vector2)transform.position + downDirection, downDirection,
                rayDistance, tileLayerMask);
            RaycastHit2D hitLeft = Physics2D.Raycast((Vector2)transform.position + leftDirection, leftDirection,
                rayDistance, tileLayerMask);
            RaycastHit2D hitRight = Physics2D.Raycast((Vector2)transform.position + rightDirection, rightDirection,
                rayDistance, tileLayerMask);

            if (hitUp.collider != null)
            {
                upTile = hitUp.collider.GetComponent<GameTile>();
            }
            else
            {
                upTile = null;
            }

            if (hitDown.collider != null)
            {
                downTile = hitDown.collider.GetComponent<GameTile>();
            }
            else
            {
                downTile = null;
            }

            if (hitLeft.collider != null)
            {
                leftTile = hitLeft.collider.GetComponent<GameTile>();
            }
            else
            {
                leftTile = null;
            }

            if (hitRight.collider != null)
            {
                rightTile = hitRight.collider.GetComponent<GameTile>();
            }
            else
            {
                rightTile = null;
            }
        }

        private void AssignColorBaseOnType()
        {
            switch (tileType)
            {
                case TileType.White:
                    break;
                case TileType.Red:
                    image.color = Color.red;
                    break;
                case TileType.Blue:
                    image.color = Color.blue;
                    break;
                case TileType.Green:
                    image.color = Color.green;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}