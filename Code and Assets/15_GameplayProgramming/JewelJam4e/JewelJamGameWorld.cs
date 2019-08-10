using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class JewelJamGameWorld : GameObjectList
{
    /// <summary>
    /// A reference to the game, so that this class can use the ScreenToWorld method.
    /// </summary>
    JewelJam game;

    /// <summary>
    /// The width of the grid: the number of cells in the horizontal direction.
    /// </summary>
    const int GridWidth = 5;

    /// <summary>
    /// The height of the grid: the number of cells in the vertical direction.
    /// </summary>
    const int GridHeight = 10;

    /// <summary>
    /// The horizontal and vertical distance between two adjacent grid cells.
    /// </summary>
    const int CellSize = 85;

    /// <summary>
    /// The size of the game world, in game units.
    /// </summary>
    public Point Size { get; private set; }

    /// <summary>
    /// The player's current score.
    /// </summary>
    public int Score { get; private set; }

    /// <summary>
    /// A reference to the moving jewel cart.
    /// </summary>
    JewelCart jewelCart;

    // References to the different overlays and buttons.
    SpriteGameObject titleScreen, gameOverScreen, helpScreen, helpButton;

    /// <summary>
    /// An enum describing the possible game states that the game can be in.
    /// </summary>
    enum GameState
    {
        TitleScreen,
        Playing,
        HelpScreen,
        GameOver
    }

    // The game state that the game is currently in.
    GameState currentState;

    public JewelJamGameWorld(JewelJam game)
    {
        // store a reference to the game
        this.game = game;

        // add the background
        SpriteGameObject background = new SpriteGameObject("spr_background");
        Size = new Point(background.Width, background.Height);
        AddChild(background);

        // add a "playing field" parent object for the grid and all related objects
        GameObjectList playingField = new GameObjectList();
        playingField.LocalPosition = new Vector2(85, 150);
        AddChild(playingField);

        // add the grid to the playing field
        JewelGrid grid = new JewelGrid(GridWidth, GridHeight, CellSize);
        playingField.AddChild(grid);

        // add the row selector to the playing field
        playingField.AddChild(new RowSelector(grid));

        // add a background sprite for the score object
        SpriteGameObject scoreFrame = new SpriteGameObject("spr_scoreframe");
        scoreFrame.LocalPosition = new Vector2(20, 20);
        AddChild(scoreFrame);

        // add the object that displays the score
        ScoreGameObject scoreObject = new ScoreGameObject();
        scoreObject.LocalPosition = new Vector2(270, 30);
        AddChild(scoreObject);

        // add the moving jewel cart
        jewelCart = new JewelCart(new Vector2(410, 230));
        AddChild(jewelCart);

        // add the help button
        helpButton = new SpriteGameObject("spr_button_help");
        helpButton.LocalPosition = new Vector2(1270, 20);
        AddChild(helpButton);
            
        // add the various overlays
        titleScreen = AddOverlay("spr_title");
        gameOverScreen = AddOverlay("spr_gameover");
        helpScreen = AddOverlay("spr_frame_help");

        // start at the title screen
        GoToState(GameState.TitleScreen);
    }

    SpriteGameObject AddOverlay(string spriteName)
    {
        // create the object
        SpriteGameObject result = new SpriteGameObject(spriteName);
        // set the center as its origin
        result.SetOriginToCenter();

        // move the object to the center of the screen
        Vector2 worldCenter = new Vector2(Size.X / 2.0f, Size.Y / 2.0f);
        result.LocalPosition = worldCenter;

        // add the object to the list of children
        AddChild(result);

        return result;
    }

    public void AddScore(int points)
    {
        // increase the score
        Score += points;

        // push the jewel cart back a bit
        jewelCart.PushBack();
    }

    public override void Reset()
    {
        base.Reset();
        Score = 0;
    }

    /// <summary>
    /// Sets the game to a certain state. 
    /// This will determine the visibility of images and the behavior of the game in general.
    /// </summary>
    /// <param name="newState">The game state to move to</param>
    void GoToState(GameState newState)
    {
        // update the game state
        currentState = newState;

        // make overlays visible or invisible
        titleScreen.Visible = currentState == GameState.TitleScreen;
        helpScreen.Visible = currentState == GameState.HelpScreen;
        gameOverScreen.Visible = currentState == GameState.GameOver;
    }

    public override void Update(GameTime gameTime)
    {
        // only run the game if we're in the "playing" state
        if (currentState == GameState.Playing)
        {
            base.Update(gameTime);

            // if the jewel cart has moved out of the screen, the game is over
            if (jewelCart.GlobalPosition.X > Size.X - 230)
                GoToState(GameState.GameOver);
        } 
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        // in the playing state, update all game objects and check if the player presses the Help button
        if (currentState == GameState.Playing)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.MouseLeftButtonPressed() && helpButton.BoundingBox.Contains(game.ScreenToWorld(inputHelper.MousePosition)))
                GoToState(GameState.HelpScreen);
        }

        // on the title screen and game-over screen, a spacebar key press should reset the game and go to the playing state
        else if (currentState == GameState.TitleScreen || currentState == GameState.GameOver)
        {
            if (inputHelper.KeyPressed(Keys.Space))
            {
                Reset();
                GoToState(GameState.Playing);
            }
        }

        // in the help screen, a spacebar key press should resume the game (without resetting it)
        else if (currentState == GameState.HelpScreen)
        {
            if (inputHelper.KeyPressed(Keys.Space))
                GoToState(GameState.Playing);
        }
    }
}