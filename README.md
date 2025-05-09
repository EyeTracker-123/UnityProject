# Oz-branches の説明書

## プレイヤー操作一覧
- ### 移動
  - キーボード：WASD
  - ゲームパッド：Lスティック
- ### ダッシュ
  - キーボード：Shiftキー
  - ゲームパッド：Rボタン
- ### 視点操作
  - キーボード：矢印キー
  - ゲームパッド：Rスティック
- ### アイテムを取る/ドアを開ける
  - キーボード：Zキー
  - ゲームパッド：Aボタン（または〇ボタン）
*** 
## オブジェクトを置換する際に必要なスクリプト達
### ドア
* door Name（変数は空欄でおｋ）  
\* ドアのオブジェクトの名前を「door」+「A~Gまでの大文字アルファベット」にする必要がある  
 例）doorA

### 鍵
* key Flag（変数は空欄でおｋ）  
\*ドアに同じくオブジェクトの名前を「key」+「A~Gまでの大文字アルファベット」にする必要がある  
 例）keyA
*** 



```mermaid
flowchart LR


b[脱出ゲームの流れ]

b0(プレイヤーは人間)
b1(屋内の迷路)
b2(目標クリアタイム:30分程度)

b --> b0
b --> b1
b --> b2

x[仕掛け]

x0(風切り音)
x1(ダイアル（あってたら「カチッ」って音がする）)
x2(音によって箱などの中身がわかる)
x3(敵の背中に仕掛けを解くヒントがあるやつ)


x --> x0
x --> x1
x --> x2
x --> x3 



s[システム]

s0(敵の足音が聞こえる)

s --> s0

```
# ユースケース図

```mermaid
flowchart LR

p{{プレイヤー}}

p0(座標移動)
p1(視点移動)
p2(目の開閉)
p3(アクション)

pp0(音が聞こえる)
pp1(エネミーに捕まる)

pp800(ゲームオーバー)

subgraph コントローラー
p0
p1
p3
end

subgraph アイトラッカー
p2
end

p---p0
p---p1
p---p3
p---pp1

p---p2
p2---pp0
pp1---pp800


e{{エネミー}}

e0(歩く)
e1(プレイヤーを捕まえる)
e2(視点移動)
e3(走る)

e---e0
e---e1
e---e2
e---e3



```
