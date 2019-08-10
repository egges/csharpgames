using Engine;
using Microsoft.Xna.Framework;
using System;

class BombTimer : GameObjectList
{
    double timeLeft;

    public bool Running { get; set; }
    public float Multiplier { get; set; }

    TextGameObject label;

    public bool HasPassed { get { return timeLeft <= 0; } }

    public BombTimer()
    {
        localPosition = new Vector2(20, 20);
        
        // add a background image
        SpriteGameObject background = new SpriteGameObject("Sprites/UI/spr_timer", TickTick.Depth_UIBackground);
        AddChild(background);

        // add a text
        label = new TextGameObject("Fonts/MainFont", TickTick.Depth_UIForeground, Color.Yellow, TextGameObject.Alignment.Center);
        label.LocalPosition = new Vector2(50,25);
        AddChild(label);

        Reset();
    }

    public override void Update(GameTime gameTime)
    {
        if (!Running)
            return;

        // decrease the timer
        double oldTimeLeft = timeLeft;
        if (!HasPassed)
            timeLeft -= gameTime.ElapsedGameTime.TotalSeconds * Multiplier;

        // display the remaining time
        int secondsLeft = (int)Math.Ceiling(timeLeft);
        label.Text = CreateTimeString(secondsLeft);
        
        // in the last 10 seconds, let the color blink between yellow and red
        if (secondsLeft <= 10 && secondsLeft % 2 == 0)
            label.Color = Color.Red;
        else
            label.Color = Color.Yellow;

        // in the last 10 seconds, play a beep every second
        if ((int)Math.Ceiling(oldTimeLeft) != secondsLeft)
        {
            if (secondsLeft <= 3) // high beep
                ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_beep_high");
            else if (secondsLeft <= 10) // low beep
                ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_beep");
        }
    }

    public override void Reset()
    {
        base.Reset();
        timeLeft = 30;
        Running = true;
        Multiplier = 1;
    }

    string CreateTimeString(int secondsLeft)
    {
        return (secondsLeft / 60).ToString().PadLeft(2, '0') 
            + ":" 
            + (secondsLeft % 60).ToString().PadLeft(2, '0');
    }
}
