using System;
using System.Collections.Generic;

namespace TicTacToo {
    public class Game : IObservable<Board> {
        private Board _board;
        private Match _match;

        // Game開始
        public Stone Start(Match match, Board board) {
            _match = match;
            _board = board;
            IPlayer player = _match.First;
            Publish(_board);
            while (!_board.IsFin()) {
                player.MakeMove(_board);
                Publish(_board);
                player = Turn(player);
            }
            Complete();
            return _board.Judge();

        }

        // 次のプレイヤー
        private IPlayer Turn(IPlayer player) {
            return player == _match.First ? _match.Second : _match.First;
        }

        private List<IObserver<Board>> _observers = new List<IObserver<Board>>();

        // 終了を通知する
        private void Complete() {
            foreach (var observer in _observers) {
                observer.OnCompleted();
            }
        }

        // 状況変化を知らせるために購読者に通知する
        private void Publish(Board board) {
            foreach (var observer in _observers) {
                observer.OnNext(board);
            }
        }

        // 購読を申し込む -  observerが通知を受け取れるようになる
        public IDisposable Subscribe(IObserver<Board> observer) {
            _observers.Add(observer);
            return observer as IDisposable;
        }
    }
}
