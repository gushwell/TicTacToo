using System;
namespace TicTacToo {
    // 対戦を表すクラス
    public class Match {

        // 先攻のプレイヤー
        public IPlayer First { get; set; }

        // 後攻のプレイヤー
        public IPlayer Second { get; set; }

        // 人間のプレイヤーを取得する。 どちらかが人間と仮定
        public IPlayer Human => First is HumanPlayer ? First : Second;

    }
}
