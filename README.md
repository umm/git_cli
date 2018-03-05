# git

* git コマンドの Unity 実装

## Requirement

* Unity 2017.1
* git
* hub (Optional)

## Install

```shell
$ npm install github:umm-projects/git
```

## Usage

### 実装済のクラス

* [`Git`](Assets/Scripts/UnityModule/Command/VCS/Git.cs)
    * `git` コマンドを同期的に実行
* [`GitAsync`](Assets/Scripts/UnityModule/Command/VCS/Git.cs)
    * `git` コマンドを非同期的に実行
* [`Hub`](Assets/Scripts/UnityModule/Command/VCS/Hub.cs)
    * `hub` コマンドを同期的に実行
* [`HubAsync`](Assets/Scripts/UnityModule/Command/VCS/Hub.cs)
    * `hub` コマンドを非同期的に実行

### 実装済のサブコマンド

* `add`
* `branch`
* `checkout`
* `commit`
* `push`
* `rev-parse`
* `rm`
* `pull-request`
    * `hub` コマンドを利用

### 同期的にコマンドを実行する

```csharp
Git.Add(new [] { "hoge.txt", "fuga.cs", });
Git.Commit("message for commit");
Git.Push("any_branch_name");
```

### 非同期的にコマンドを実行する

```csharp
// 書き方を統一するために、空の Unit を吐き出すところから開始している
Observable.Return(Unit.Default)
    .SelectMany(_ => GitAsync.Add(new [] { "hoge.txt", "fuga.cs", }))
    .SelectMany(_ => GitAsync.Commit("message for commit"))
    .SelectMany(_ => GitAsync.Push("any_branch_name"))
    .Subscribe();
```

## License

Copyright (c) 2017-2018 Tetsuya Mori

Released under the MIT license, see [LICENSE.txt](LICENSE.txt)

