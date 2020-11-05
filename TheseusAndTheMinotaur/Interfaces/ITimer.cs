namespace TheseusAndTheMinotaur
{
    interface ITimer
    {
        /// <summary>
        /// Start the timer
        /// </summary>
        void Start();

        /// <summary>
        /// Stop the timer
        /// </summary>
        void Stop();

        /// <summary>
        /// Reset the timer
        /// </summary>
        /// <param name="startTime">Optionally set a non-zero start time. Measured in milliseconds</param>
        /// <param name="startImmediately">Set true to start the timer with this method call</param>
        void Reset(int startTime = 0, bool startImmediately = false);

        /// <summary>
        /// Reset the timer
        /// </summary>
        /// <param name="startImmediately">Set true to start the timer with this method call</param>
        void Reset(bool startImmediately);
    }
}
