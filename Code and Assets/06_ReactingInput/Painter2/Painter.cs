using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class Painter : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D background, cannonBarrel;
    Vector2 barrelPosition, barrelOrigin;
    float angle;

    Texture2D colorRed, colorGreen, colorBlue;
    Color currentColor;
    Vector2 colorOrigin;
    
    MouseState currentMouseState, previousMouseState;
    KeyboardState currentKeyboardState, previousKeyboardState;

    [STAThread]
    static void Main()
    {
        Painter game = new Painter();
        game.Run();
    }

    public Painter()
    {
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        background = Content.Load<Texture2D>("spr_background");

        cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");
        barrelPosition = new Vector2(72, 405);
        barrelOrigin = new Vector2(cannonBarrel.Height, cannonBarrel.Height) / 2; 
        
        colorRed = Content.Load<Texture2D>("spr_cannon_red");
        colorGreen = Content.Load<Texture2D>("spr_cannon_green");
        colorBlue = Content.Load<Texture2D>("spr_cannon_blue");
        currentColor = Color.Blue;
        colorOrigin = new Vector2(colorRed.Width, colorRed.Height) / 2;
    }

    protected override void Update(GameTime gameTime)
    {
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();

        if (currentKeyboardState.IsKeyDown(Keys.R) && previousKeyboardState.IsKeyUp(Keys.R))
        {
            currentColor = Color.Red;
        }
        else if (currentKeyboardState.IsKeyDown(Keys.G) && previousKeyboardState.IsKeyUp(Keys.G))
        {
            currentColor = Color.Green;
        }
        else if (currentKeyboardState.IsKeyDown(Keys.B) && previousKeyboardState.IsKeyUp(Keys.B))
        {
            currentColor = Color.Blue;
        }

        double opposite = currentMouseState.Y - barrelPosition.Y;
        double adjacent = currentMouseState.X - barrelPosition.X;
        angle = (float)Math.Atan2(opposite, adjacent);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin();
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        spriteBatch.Draw(cannonBarrel, barrelPosition, null, Color.White, angle, barrelOrigin, 1.0f, SpriteEffects.None, 0);

        // determine the sprite based on the current color
        Texture2D currentSprite;
        if (currentColor == Color.Red)
            currentSprite = colorRed;
        else if (currentColor == Color.Green)
            currentSprite = colorGreen;
        else
            currentSprite = colorBlue;

        // draw that sprite
        spriteBatch.Draw(currentSprite, barrelPosition, null, Color.White, 0f, colorOrigin, 1.0f, SpriteEffects.None, 0);

        spriteBatch.End();
    }
}