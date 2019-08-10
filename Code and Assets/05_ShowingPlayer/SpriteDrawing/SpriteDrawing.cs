using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

class SpriteDrawing : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D balloon;

    [STAThread]
    static void Main()
    {
        SpriteDrawing game = new SpriteDrawing();
        game.Run();
    }

    public SpriteDrawing()
    {
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        balloon = Content.Load<Texture2D>("spr_lives");

        MediaPlayer.Play(Content.Load<Song>("snd_music"));
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin();
        spriteBatch.Draw(balloon, Vector2.Zero, Color.White);
        spriteBatch.End();
    }
}