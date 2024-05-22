using Raylib_cs;
using System.Numerics;

namespace Game
{
    public class PlayerComponent : Component
    {
        private Texture2D character;
        private int spriteHeight;
        private int spriteWidth;
        private int frameX = 0;
        private int frameY = 0;

        private float positionPlayerX = 0;
        private float positionPlayerY = 0;

        private const float speed = 25.0f;
        private float frameCounter = 0;
        private const float frameSpeed = 8.0f; // Frames per second

        private bool isAttacking = false;


        private bool isCollision = false;



        private Vector2 screenPosition; 


        

        public PlayerComponent(string imagePath, int spriteWidth, int spriteHeight )
        {
            character = Raylib.LoadTexture(imagePath);
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
        }

     


        public Rectangle playerRec() {

            return new Rectangle(spriteWidth * frameX, spriteHeight * frameY, spriteWidth, spriteHeight);
        }
      

        public Rectangle collisionRecPlayer ()
        {
            return new Rectangle(screenPosition.X, screenPosition.Y, spriteWidth - 2, spriteHeight -2 );
        }


       

        public void hitBox(bool isColliding)
        {

            this.isCollision = isColliding;

            Color color = isColliding ? Color.Red : Color.Blue;
            Raylib.DrawRectangleLinesEx(collisionRecPlayer(), 2, color);




        }


        public override void Draw()
        {




            if (isAttacking)
            {
                attackAnimation();
            }
            else
            {

                moveAnimation();

                // Move animation with keys
                if (Raylib.IsKeyDown(KeyboardKey.Up))
                {
                    frameY = 1;
                   positionPlayerY -= speed * Raylib.GetFrameTime();
                }
                else if (Raylib.IsKeyDown(KeyboardKey.Down))
                {
                    frameY = 0;
                    positionPlayerY += speed * Raylib.GetFrameTime();
                }
                else if (Raylib.IsKeyDown(KeyboardKey.Right))
                {
                    frameY = 3;
                   positionPlayerX += speed * Raylib.GetFrameTime();
                }
                else if (Raylib.IsKeyDown(KeyboardKey.Left))
                {
                    frameY = 2;
                   positionPlayerX -= speed * Raylib.GetFrameTime();
                }
                else if (frameX < 2)
                {
                    frameX = 0;
                }


                if(isCollision)
                {
                   
                        Raylib.DrawText("hello benny how are you", 0, 0, 30, Color.Red);

                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Z))
            {
                isAttacking = true;
                frameX = 2; // Start attack animation
            }

          
            screenPosition = new Vector2(500 / 2 + positionPlayerX, 380 / 2 + positionPlayerY);

            Raylib.DrawRectangleLinesEx(collisionRecPlayer(), 2, Color.Red);
            Raylib.DrawTextureRec(character, playerRec(), screenPosition, Color.White);

        }

        private void moveAnimation()
        {
            frameCounter += Raylib.GetFrameTime();
            if (frameCounter >= 1.0f / frameSpeed)
            {
                frameCounter = 0;
                frameX++;
                if (frameX > 1) frameX = 0;
            }
        }

        private void attackAnimation()
        {
            frameCounter += Raylib.GetFrameTime();
            if (frameCounter >= 1.0f / frameSpeed)
            {
                frameCounter = 0;
                frameX++;
                if (frameX > 3)
                {
                    frameX = 0; 
                    isAttacking = false; 
                }
            }
        }
    }
}
