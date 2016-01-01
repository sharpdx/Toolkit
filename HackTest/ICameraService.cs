using SharpDX;

namespace SharpDXTutorial2
{
    /// <summary>
    /// Provides camera information, like view and projection matrices
    /// </summary>
    internal interface ICameraService
    {
        /// <summary>
        /// The camera's view matrix
        /// </summary>
        Matrix View { get; }

        /// <summary>
        /// The camera's projection matrix
        /// </summary>
        Matrix Projection { get; }
    }
}