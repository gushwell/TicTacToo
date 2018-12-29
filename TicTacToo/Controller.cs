using System;
namespace TicTacToo {
    class Controller : IObserver<Board> {

        private Board _board;
        private Match _match;

        // 試合の開始
        public void Run() {
            _match = DecideFirstPlayer();
            _board = new Board();
            var game = new Game();
            // 購読者は自分自身
            game.Subscribe(this);
            var win = game.Start(_match, _board);
        }

        // 先手を決める
        private Match DecideFirstPlayer() {
            var match = new Match();
            var b = Confirm("Are you first?");
            if (b) {
                match.First = new HumanPlayer(Stone.Black);
                match.Second = new PerfectPlayer(Stone.White);
            } else {
                match.First = new PerfectPlayer(Stone.Black);
                match.Second = new HumanPlayer(Stone.White);
            }
            return match;
        }

        // 盤面を表示
        private void Print(Board board) {
            Console.Clear();
            Console.WriteLine("You: {0}\n", _match.Human.Stone == Stone.White ? "O" : "X");
            for (int y = 1; y <= 3; y++) {
                for (int x = 1; x <= 3; x++) {
                    Console.Write(board[x, y].Value + " ");
                }
                Console.WriteLine();
            }
        }

        // 終了した
        public void OnCompleted() {
            var win = _board.Judge();
            if (win == Stone.Empty)
                Console.WriteLine("Draw");
            else if (win == _match.Human.Stone)
                Console.WriteLine("You Win");
            else
                Console.WriteLine("You Lose");
        }

        // エラー発生
        public void OnError(Exception error) {
            Console.WriteLine(error.Message);
        }

        // 状況変化
        public void OnNext(Board value) {
            Print(value);
        }

        // (y/n)の確認
        public static bool Confirm(string message) {
            Console.Write(message);
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            try {
                while (true) {
                    var key = Console.ReadKey();
                    if (key.KeyChar == 'y')
                        return true;
                    if (key.KeyChar == 'n')
                        return false;
                    Console.CursorLeft = left;
                    Console.CursorTop = top;
                }
            } finally {
                Console.WriteLine();
            }
        }
    }
}
