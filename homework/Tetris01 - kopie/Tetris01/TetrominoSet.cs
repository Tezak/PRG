using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris01
{
    internal class TetrominoSet
    {
        public static List<Tetromino> tetrominos = createTetraminos();
        private static List<Tetromino> createTetraminos()
        {
            List<Tetromino> tetraminos = new List<Tetromino>();
            Tetromino I = new Tetromino();
            tetraminos.Add(I);
            I.shapeRotation.Add(new bool[,]{
                { false,false,false,false },
                { true,true,true,true },
                { false,false,false,false },
                { false,false,false,false }
            });
            I.shapeRotation.Add(new bool[,]{
                { false,true,false,false },
                { false,true,false,false },
                { false,true,false,false },
                { false,true,false,false }
            });
            Tetromino T = new Tetromino();
            tetraminos.Add(T);
            T.shapeRotation.Add(new bool[,]{
                { false,true,false,false },
                { true,true,true,false },
                { false,false,false,false },
                { false,false,false,false }
            });
            T.shapeRotation.Add(new bool[,]{
                { false,true,false,false },
                { false,true,true,false },
                { false,true,false,false },
                { false,false,false,false }
            });
            T.shapeRotation.Add(new bool[,]{
                { false,false,false,false },
                { true,true,true,false },
                { false,true,false,false },
                { false,false,false,false }
            });
            T.shapeRotation.Add(new bool[,]{
                { false,true,false,false },
                { true,true,false,false },
                { false,true,false,false },
                { false,false,false,false }
            });
            Tetromino Z = new Tetromino();
            tetraminos.Add(Z);
            Z.shapeRotation.Add(new bool[,]{
                { true,true,false,false },
                { false,true,true,false },
                { false,false,false,false },
                { false,false,false,false }
            });
            Z.shapeRotation.Add(new bool[,]{
                { true,false,false,false },
                { true,true,false,false },
                { false,true,false,false },
                { false,false,false,false }
            });
            Tetromino ZR = new Tetromino();
            tetraminos.Add(ZR);
            ZR.shapeRotation.Add(new bool[,]{
                { false,true,true,false },
                { true,true,false,false },
                { false,false,false,false },
                { false,false,false,false }
            });
            ZR.shapeRotation.Add(new bool[,]{
                { false,false,true,false },
                { false,true,true,false },
                { false,true,false,false },
                { false,false,false,false }
            });
            Tetromino L = new Tetromino();
            tetraminos.Add(L);
            L.shapeRotation.Add(new bool[,]{
                { false,false,true,false },
                { true,true,true,false },
                { false,false,false,false },
                { false,false,false,false }
            });
            L.shapeRotation.Add(new bool[,]{
                { false,true,false,false },
                { false,true,false,false },
                { false,true,true,false },
                { false,false,false,false }
            });
            L.shapeRotation.Add(new bool[,]{
                { true,true,true,false },
                { true,false,false,false },
                { false,false,false,false },
                { false,false,false,false }
            });
            L.shapeRotation.Add(new bool[,]{
                { false,true,true,false },
                { false,false,true,false },
                { false,false,true,false },
                { false,false,false,false }
            });
            Tetromino LR = new Tetromino();
            tetraminos.Add(LR);
            LR.shapeRotation.Add(new bool[,]{
                { true,false,false,false },
                { true,true,true,false },
                { false,false,false,false },
                { false,false,false,false }
            });
            LR.shapeRotation.Add(new bool[,]{
                { false,true,false,false },
                { false,true,false,false },
                { true,true,false,false },
                { false,false,false,false }
            });
            LR.shapeRotation.Add(new bool[,]{
                { true,true,true,false },
                { false,false,true,false },
                { false,false,false,false },
                { false,false,false,false }
            });
            LR.shapeRotation.Add(new bool[,]{
                { true,true,false,false },
                { true,false,false,false },
                { true,false,false,false },
                { false,false,false,false }
            });
            Tetromino O = new Tetromino();
            tetraminos.Add(O);
            O.shapeRotation.Add(new bool[,]{
                { false,true,true,false },
                { false,true,true,false },
                { false,false,false,false },
                { false,false,false,false }
            });
            return tetraminos;
        }
    }
}
