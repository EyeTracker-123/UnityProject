```mermaid
flowchart TB
start[停電状態の部屋]
second[停電の復旧]
map[マップの入手]
key[脱出のカギを知る]

goal[脱出]


start -->|チュートリアル終了| second
second --> map
map --> key
key --> goal


```
