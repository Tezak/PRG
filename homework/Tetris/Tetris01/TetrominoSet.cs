using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris01
{
    internal class TetrominoSet
    {
        const bool o = true;
        const bool x = false;
        public static List<Tetromino> tetrominos = createTetraminos();
        private static List<Tetromino> createTetraminos()
        {
            List<Tetromino> tetraminos = new List<Tetromino>();
            Tetromino I = new Tetromino();
            tetraminos.Add(I);
            I.shapeRotation.Add(new bool[,]{
                { x,x,x,x },
                { o,o,o,o },
                { x,x,x,x },
                { x,x,x,x }
            });
            I.shapeRotation.Add(new bool[,]{
                { x,o,x,x },
                { x,o,x,x },
                { x,o,x,x },
                { x,o,x,x }
            });
            Tetromino T = new Tetromino();
            tetraminos.Add(T);
            T.shapeRotation.Add(new bool[,]{
                { x,o,x,x },
                { o,o,o,x },
                { x,x,x,x },
                { x,x,x,x }
            });
            T.shapeRotation.Add(new bool[,]{
                { x,o,x,x },
                { x,o,o,x },
                { x,o,x,x },
                { x,x,x,x }
            });
            T.shapeRotation.Add(new bool[,]{
                { x,x,x,x },
                { o,o,o,x },
                { x,o,x,x },
                { x,x,x,x }
            });
            T.shapeRotation.Add(new bool[,]{
                { x,o,x,x },
                { o,o,x,x },
                { x,o,x,x },
                { x,x,x,x }
            });
            Tetromino Z = new Tetromino();
            tetraminos.Add(Z);
            Z.shapeRotation.Add(new bool[,]{
                { o,o,x,x },
                { x,o,o,x },
                { x,x,x,x },
                { x,x,x,x }
            });
            Z.shapeRotation.Add(new bool[,]{
                { x,o,x,x },
                { o,o,x,x },
                { o,x,x,x },
                { x,x,x,x }
            });
            Tetromino ZR = new Tetromino();
            tetraminos.Add(ZR);
            ZR.shapeRotation.Add(new bool[,]{
                { x,o,o,x },
                { o,o,x,x },
                { x,x,x,x },
                { x,x,x,x }
            });
            ZR.shapeRotation.Add(new bool[,]{
                { x,o,x,x },
                { x,o,o,x },
                { x,x,o,x },
                { x,x,x,x }
            });
            Tetromino L = new Tetromino();
            tetraminos.Add(L);
            L.shapeRotation.Add(new bool[,]{
                { x,x,o,x },
                { o,o,o,x },
                { x,x,x,x },
                { x,x,x,x }
            });
            L.shapeRotation.Add(new bool[,]{
                { x,o,x,x },
                { x,o,x,x },
                { x,o,o,x },
                { x,x,x,x }
            });
            L.shapeRotation.Add(new bool[,]{
                { o,o,o,x },
                { o,x,x,x },
                { x,x,x,x },
                { x,x,x,x }
            });
            L.shapeRotation.Add(new bool[,]{
                { x,o,o,x },
                { x,x,o,x },
                { x,x,o,x },
                { x,x,x,x }
            });
            Tetromino LR = new Tetromino();
            tetraminos.Add(LR);
            LR.shapeRotation.Add(new bool[,]{
                { o,x,x,x },
                { o,o,o,x },
                { x,x,x,x },
                { x,x,x,x }
            });
            LR.shapeRotation.Add(new bool[,]{
                { x,o,x,x },
                { x,o,x,x },
                { o,o,x,x },
                { x,x,x,x }
            });
            LR.shapeRotation.Add(new bool[,]{
                { o,o,o,x },
                { x,x,o,x },
                { x,x,x,x },
                { x,x,x,x }
            });
            LR.shapeRotation.Add(new bool[,]{
                { o,o,x,x },
                { o,x,x,x },
                { o,x,x,x },
                { x,x,x,x }
            });
            Tetromino O = new Tetromino();
            tetraminos.Add(O);
            O.shapeRotation.Add(new bool[,]{
                { x,o,o,x },
                { x,o,o,x },
                { x,x,x,x },
                { x,x,x,x }
            });
            return tetraminos;
        }
    }
}
