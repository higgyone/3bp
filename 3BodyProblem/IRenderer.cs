namespace _3BodyProblem
{
    /// <summary>
    /// renderer to display sprites
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// clear everything in the canvas
        /// </summary>
        void ClearCanvas();

        /// <summary>
        /// draw the sprites on the canvas
        /// </summary>
        /// <param name="sprite">array of sprites to draw</param>
        void DrawSprite(Sprite[] sprite);

        /// <summary>
        /// get the actual height of the renderer display canvas
        /// Note to be called AFTER the canvas has been setup otherwise
        /// returns 0 before the canvas has been initiated
        /// </summary>
        /// <returns>doubel actual height</returns>
        double GetRenderHeight();

        /// <summary>
        /// get the actual width of the renderer display canvas
        /// Note to be called AFTER the canvas has been setup otherwise
        /// returns 0 before the canvas has been initiated
        /// </summary>
        /// <returns>dispaly canvas actual width</returns>
        double GetRenderWidth();

        /// <summary>
        /// remove the sprrite from the canvas
        /// </summary>
        /// <param name="sprite">sprite to remove</param>
        void RemoveSprite(Sprite sprite);

        /// <summary>
        /// get the display canvas
        /// </summary>
        /// <returns>display canvas</returns>
        IDisplayCanvas GetDisplayCanvas();
    }
}