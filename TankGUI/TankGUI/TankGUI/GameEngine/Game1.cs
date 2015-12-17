using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TankGUI.GameEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public struct PlayerData
    {
        public Vector2 Position;
        public bool IsAlive;
        public Color Color;
        public float Angle;
        public float Power;

        public int playerNum;
        public String Direction;
        public bool IsShot;
        public int Health;
        public int Coin;
        public int Point;
        public String Detail;
    }

    public struct cellData
    {
        public Vector2 Position;
        public String type;  // W-wall   S-stone  L-water   c-coin

    }

    


    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;

        Texture2D backgroundTexture;
        Texture2D foregroundTexture;
        Texture2D markTexture;

        Texture2D cannonTexture;
        Texture2D tankTexture;
        Texture2D shellTexture;

        bool shellFlying = false;
        Vector2 shellPosition;
        //Vector2 rocketDirection;
        //float rocketAngle;
        //float rocketScaling = 1f;


        int screenWidth;
        int screenHeight;
        SpriteFont font;

        float playerScaling;


        PlayerData[] players;
        cellData[][] cells;


        int numberOfPlayers = 5;

        int currentPlayer = 0;

        int blockFactor = 70;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 720;
            
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            

            Window.Title = "World of Tank-SL";

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
           
            backgroundTexture = Content.Load<Texture2D>("field2");
            foregroundTexture = Content.Load<Texture2D>("foreground");
            markTexture = Content.Load<Texture2D>("plane");


            cannonTexture = Content.Load<Texture2D>("cannon");
            tankTexture = Content.Load<Texture2D>("tank");
            shellTexture = Content.Load<Texture2D>("shell0");

            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;

            font = Content.Load<SpriteFont>("myFont");


            SetUpPlayers();

            playerScaling = 40.0f / (float)tankTexture.Width;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            ProcessKeyboard();
            UpdateShell();

            base.Update(gameTime);
        }

        private void UpdateShell()
        {
            if (shellFlying)
            {
                //Vector2 gravity = new Vector2(0, 1);
                //rocketDirection += gravity / 10.0f;
                //rocketPosition += rocketDirection;
                //rocketAngle = (float)Math.Atan2(rocketDirection.X, -rocketDirection.Y);
                /*
                switch (players[currentPlayer].Direction)
                {
                    case "Left":
                        players[currentPlayer].Position.X += 1f;
                        break;
                    case "right":
                        //
                        break;
                    default:
                        //sdcss


                }
                */

            }
        }
        private void ProcessKeyboard()
        {
            KeyboardState keybState = Keyboard.GetState();
            if (keybState.IsKeyDown(Keys.Left))
                for (int x=0; x < 70; x++)
                {
                    players[currentPlayer].Position.X -= 1f;
                }
            if (keybState.IsKeyDown(Keys.Right))
                for (int x = 0; x < 70; x++)
                {
                    players[currentPlayer].Position.X += 1f;
                }

            if (players[currentPlayer].Position.X > 650)
                players[currentPlayer].Position.X = 650;
            if (players[currentPlayer].Position.X < 0)
                players[currentPlayer].Position.X = 0;


            if (keybState.IsKeyDown(Keys.Down))
                for (int x = 0; x < 70; x++)
                {
                    players[currentPlayer].Position.Y += 1f;
                }
            if (keybState.IsKeyDown(Keys.Up))
                for (int x = 0; x < 70; x++)
                {
                    players[currentPlayer].Position.Y -= 1f;
                }


            if (players[currentPlayer].Position.Y > 700)
                players[currentPlayer].Position.Y = 700;
            if (players[currentPlayer].Position.Y < 30)
                players[currentPlayer].Position.Y = 30;

            if (keybState.IsKeyDown(Keys.Enter) || keybState.IsKeyDown(Keys.Space))
            {
                shellFlying = true;

                shellPosition = players[currentPlayer].Position;
                
                shellPosition.X += 20;
                shellPosition.Y -= 10;
                
                
            }

        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            graphics.GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();
            DrawScenery();
            DrawPlayers();
            DrawText();
            DrawShell();


            spriteBatch.End();

            

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void DrawScenery()
        {


            Rectangle screenRectangle = new Rectangle(0, 0, 700, 700);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
            //spriteBatch.Draw(foregroundTexture, screenRectangle, Color.White);

            Rectangle markRectangle = new Rectangle(720, 0, 250, 700);

            spriteBatch.Draw(markTexture, markRectangle, Color.White);
        }

        private void DrawPlayers()
        {
            foreach (PlayerData player in players)
            {
                if (player.IsAlive)
                {

                    int xPos = (int)player.Position.X;
                    int yPos = (int)player.Position.Y;
                    Vector2 cannonOrigin = new Vector2(11, 50);

                    //spriteBatch.Draw(tankTexture, new Vector2(xPos + 20, yPos - 10), null, player.Color, player.Angle, cannonOrigin, playerScaling, SpriteEffects.None, 1);
                    //spriteBatch.Draw(tankTexture, player.Position, null, player.Color, 0, new Vector2(0, tankTexture.Height), playerScaling, SpriteEffects.None, 0);

                    //spriteBatch.Draw(cannonTexture, new Vector2(xPos + 20, yPos - 10), null, player.Color, player.Angle, cannonOrigin, playerScaling, SpriteEffects.None, 1);
                    spriteBatch.Draw(tankTexture, player.Position, null, player.Color,player.Angle, new Vector2(0, tankTexture.Height), playerScaling, SpriteEffects.None,0);
                
                }
            }
        }
        /*
        private void DrawPlayers()
        {
            foreach (cellData[] cellRaw in cells)
            {

                foreach (cellData cell in cellRaw)
                {
                    switch (cell.type)
                    {
                        case "W":
                            //
                            break;
                        case "S":
                            //
                            break;


                    }

                }


            }
        


        }*/


        private void DrawShell()
        {
            if (shellFlying)
                spriteBatch.Draw(shellTexture, shellPosition, null, players[currentPlayer].Color, 0, new Vector2(0, 0), 0, SpriteEffects.None,1);
        }


        private void DrawText()
        {
            PlayerData player = players[currentPlayer];
            int currentAngle = (int)MathHelper.ToDegrees(player.Angle);


            spriteBatch.DrawString(font, "sdsada", new Vector2(750, 100), player.Color);
            spriteBatch.DrawString(font, "Cannon power: " + player.Power.ToString(), new Vector2(750, 120), player.Color);
            
        }

        



        private void SetUpPlayers()
        {
            Color[] playerColors = new Color[10];
            playerColors[0] = Color.Red;
            playerColors[1] = Color.Green;
            playerColors[2] = Color.Blue;
            playerColors[3] = Color.Yellow;
            playerColors[4] = Color.White;
         

            players = new PlayerData[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i].IsAlive = true;
                players[i].Color = playerColors[i];
                players[i].Angle = MathHelper.ToRadians(0);
                players[i].Power = 100;
            }

            /*for (int j = 0; j < numberOfPlayers; j++ )
            {


            }*/
            players[0].Position = new Vector2(0, 0);
            players[1].Position = new Vector2(70, 70);
            players[2].Position = new Vector2(100, 100);
            players[3].Position = new Vector2(140, 140);
        }


        public void playerMove()
        {




        }

    }
}
