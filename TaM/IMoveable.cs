namespace TaM
{
    interface IMoveable
    {
        void GoUp();

        void GoDown();

        void Goleft();

        void GoRight();

        int Row { get; }

        int Column { get; }
    }
}
