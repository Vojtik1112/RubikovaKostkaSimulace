using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikovaKostkaSimulace.Models
{

    public class MoveHistory
    {
        public List<Move> UndoList { get; private set; }
        public List<Move> RedoList { get; private set; }

        public bool CanUndo
        {
            get { return UndoList.Count > 0; }
        }

        public bool CanRedo
        {
            get { return RedoList.Count > 0; }
        }

        public MoveHistory()
        {
            UndoList = new List<Move>();
            RedoList = new List<Move>();
        }

        public void RecordMove(Move move)
        {
            UndoList.Add(move);
            RedoList.Clear();
        }

        public Move PopUndo()
        {
            if (!CanUndo)
                return null;

            int lastIndex = UndoList.Count - 1;
            Move lastMove = UndoList[lastIndex];
            UndoList.RemoveAt(lastIndex);
            RedoList.Add(lastMove);

            return lastMove;
        }

        public Move PopRedo()
        {
            if (!CanRedo)
                return null;

            int lastIndex = RedoList.Count - 1;
            Move redoMove = RedoList[lastIndex];
            RedoList.RemoveAt(lastIndex);
            UndoList.Add(redoMove);

            return redoMove;
        }

        public void Clear()
        {
            UndoList.Clear();
            RedoList.Clear();
        }
    }
}