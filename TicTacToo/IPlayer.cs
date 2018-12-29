using System;
namespace TicTacToo {
    public interface IPlayer {
        Stone Stone { get; }
        int MakeMove(Board board);
    }
}
