```mermaid
flowchart TB

anim[アニメーションを実行]
startcl[コルーチンを開始]
endcl[コルーチンを開始]
getanimator[hitobjからアニメーターの取得]


startcl --> getanimator
getanimator --> anim
anim --> endcl


```