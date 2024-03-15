using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame_ATB
{
    internal class Player
    {

        private int _PosX, _PosY;

        public int PosX { get; private set; }
        public int PosY { get; private set; }
        

        public List<Pos> Positions { get { return _positions; } }
        private List<Pos> _positions = new List<Pos>();

        public enum Move
        {
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3
        }

        public void Initialize(int posY, int posX)
        {
            PosY = posY;
            PosX = posX;
            _positions.Add(new Pos(PosY, PosX));
        }

        public void MovePlayer(Move move)
        {
            _PosY = _positions[0].Y;
            _PosX = _positions[0].X;

            switch (move)
            {
                case Move.Up:
                    _positions[0].Y--;
                    break;
                case Move.Down:
                    _positions[0].Y++;
                    break;
                case Move.Left:
                    _positions[0].X--;
                    break;
                case Move.Right:
                    _positions[0].X++;
                    break;
            }
        }
    }       
}
