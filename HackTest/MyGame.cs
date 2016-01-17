namespace SharpDXTutorial2
{
    using SharpDX;
    using SharpDX.Toolkit;

    /// <summary>
    /// Main game class.
    /// </summary>
    internal sealed class MyGame : Game
    {
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly SceneRenderer _sceneRenderer;
        private readonly CameraProvider _cameraProvider;
        private readonly FPSCounter _fpsCounter;

        /// <summary>
        /// Construct any component instances here.
        /// </summary>
        public MyGame()
        {
            // GraphicsDeviceManager is mandatory for a game to run
            _graphicsDeviceManager = new GraphicsDeviceManager(this);

            _fpsCounter = new FPSCounter(this);

            // initialize the scene renderer system
            _sceneRenderer = new SceneRenderer(this);

            // initialize the camera provider, it will register the camera service also
            _cameraProvider = new CameraProvider(this);

            // Set the content root directory relative to current executable's folder
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Draws game content.
        /// </summary>
        /// <param name="gameTime">Structure containing information about elapsed game time.</param>
        protected override void Draw(GameTime gameTime)
        {
            // clear the scene to a CornflowerBlue color
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
