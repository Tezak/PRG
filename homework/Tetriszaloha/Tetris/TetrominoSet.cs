using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class TetrominoSet
    {
        public static List<Tetromino> tetrominos = createTetraminos();
        private static List<Tetromino> createTetraminos()
        {
            List<Tetromino> tetraminos = new List<Tetromino>();
            Tetromino I = new Tetromino();
            tetraminos.Add(I);
            I.shapeRotation.Add(new int[,]{
                { 0,0,0,0 },
                { 1,1,1,1 },
                { 0,0,0,0 },
                { 0,0,0,0 }
            });
            I.shapeRotation.Add(new int[,]{
                { 0,1,0,0 },
                { 0,1,0,0 },
                { 0,1,0,0 },
                { 0,1,0,0 }
            });
            Tetromino T = new Tetromino();
            tetraminos.Add(T);
            T.shapeRotation.Add(new int[,]{
                { 0,1,0,0 },
                { 1,1,1,0 },
                { 0,0,0,0 },
                { 0,0,0,0 }
            });
            T.shapeRotation.Add(new int[,]{
                { 0,1,0,0 },
                { 0,1,1,0 },
                { 0,1,0,0 },
                { 0,0,0,0 }
            });
            T.shapeRotation.Add(new int[,]{
                { 0,0,0,0 },
                { 1,1,1,0 },
                { 0,1,0,0 },
                { 0,0,0,0 }
            });
            T.shapeRotation.Add(new int[,]{
                { 0,1,0,0 },
                { 1,1,0,0 },
                { 0,1,0,0 },
                { 0,0,0,0 }
            });
            Tetromino Z = new Tetromino();
            tetraminos.Add(Z);
            Z.shapeRotation.Add(new int[,]{
                { 1,1,0,0 },
                { 0,1,1,0 },
                { 0,0,0,0 },
                { 0,0,0,0 }
            });
            Z.shapeRotation.Add(new int[,]{
                { 1,0,0,0 },
                { 1,1,0,0 },
                { 0,1,0,0 },
                { 0,0,0,0 }
            });
            Tetromino ZR = new Tetromino();
            tetraminos.Add(ZR);
            ZR.shapeRotation.Add(new int[,]{
                { 0,1,1,0 },
                { 1,1,0,0 },
                { 0,0,0,0 },
                { 0,0,0,0 }
            });
            ZR.shapeRotation.Add(new int[,]{
                { 0,0,1,0 },
                { 0,1,1,0 },
                { 0,1,0,0 },
                { 0,0,0,0 }
            });
            Tetromino L = new Tetromino();
            tetraminos.Add(L);
            L.shapeRotation.Add(new int[,]{
                { 0,0,1,0 },
                { 1,1,1,0 },
                { 0,0,0,0 },
                { 0,0,0,0 }
            });
            L.shapeRotation.Add(new int[,]{
                { 0,1,0,0 },
                { 0,1,0,0 },
                { 0,1,1,0 },
                { 0,0,0,0 }
            });
            L.shapeRotation.Add(new int[,]{
                { 1,1,1,0 },
                { 1,0,0,0 },
                { 0,0,0,0 },
                { 0,0,0,0 }
            });
            L.shapeRotation.Add(new int[,]{
                { 0,1,1,0 },
                { 0,0,1,0 },
                { 0,0,1,0 },
                { 0,0,0,0 }
            });
            Tetromino LR = new Tetromino();
            tetraminos.Add(LR);
            LR.shapeRotation.Add(new int[,]{
                { 1,0,0,0 },
                { 1,1,1,0 },
                { 0,0,0,0 },
                { 0,0,0,0 }
            });
            LR.shapeRotation.Add(new int[,]{
                { 0,1,0,0 },
                { 0,1,0,0 },
                { 1,1,0,0 },
                { 0,0,0,0 }
            });
            LR.shapeRotation.Add(new int[,]{
                { 1,1,1,0 },
                { 0,0,1,0 },
                { 0,0,0,0 },
                { 0,0,0,0 }
            });
            LR.shapeRotation.Add(new int[,]{
                { 1,1,0,0 },
                { 1,0,0,0 },
                { 1,0,0,0 },
                { 0,0,0,0 }
            });
            Tetromino O = new Tetromino();
            tetraminos.Add(O);
            O.shapeRotation.Add(new int[,]{
                { 0,1,1,0 },
                { 0,1,1,0 },
                { 0,0,0,0 },
                { 0,0,0,0 }
            });
        return tetraminos;
        }
    }
}
