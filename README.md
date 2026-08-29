# SkyWatcher Motor Controller Monitor

SynScanPro（天体望遠鏡制御ソフトウェア）と SkyWatcher モーターコントローラー間の通信をリアルタイムで監視・解析する Windows デスクトップアプリケーションです。

## 概要

このアプリケーションは、SynScanPro が送信するコマンドとモーターコントローラーが返すレスポンスをシリアル通信経由で傍受・表示します。機器の動作確認やデバッグ、通信プロトコルの学習に活用できます。

## 機能

- **シリアル通信の監視**: 2つの独立したシリアルポートで入出力を同時監視
- **コマンド解析**: SynScanProから送信されるコマンドを認識し、内容を人間が読める形式で表示
  - Timer Interrupt Freq 問い合わせ
  - Motor Board Version 問い合わせ
  - ステータス問い合わせ
  - Extended 問い合わせ
  - PEC period 問い合わせ
  - AutoGuide Speed 設定
  - Extended コマンド (X_ prefix)
  - Polar Scope LED 明るさ調整
- **レスポンス表示**: モーターコントローラーからのレスポンスをデコード
  - ヘキサダンプ表示（8文字ごとにスペース区切り）
  - エラー応答の検出
- **リアルタイムログ**: すべての通信内容と解析結果をログボックスに表示
- **ポート管理**: 接続可能なシリアルポートを自動検出、ドロップダウンで選択

## 必要な環境

- Windows 7 以上
- .NET Framework 4.7.2

## インストール

### ビルド方法

Visual Studio 2019 以上で開く場合：

```
1. SkyWatcherMotorControllerMonitor.sln をダブルクリック
2. ビルド > ソリューションのビルド
3. プロジェクト > プロパティから出力パスを確認
```

### リリースビルド生成

```
1. ビルド > 構成マネージャー
2. アクティブソリューション構成を "Release" に変更
3. ビルド > ソリューションのビルド
4. bin\Release\ に実行ファイルが生成される
```

## 使い方

### 起動

`SkyWatcherMotorControllerMonitor.exe` を実行します。

### ポート設定

1. 上部の "In Port" ドロップダウンで、SynScanPro側のシリアルポートを選択
2. "Out Port" ドロップダウンで、モーターコントローラー側のシリアルポートを選択

### 通信開始

- **Connect ボタン**をクリックしてシリアルポートをオープン
  - ボタンが赤く表示され、ポートが開いたことを示します
- SynScanProを起動し、望遠鏡を操作

### ログの確認

- **左側ログボックス**: すべての生データ（16進表示）
- **右側ログボックス**: 解析済みデータ（意味のある表現）

### ログクリア

**Clear** ボタンでログボックスをクリアできます。

## 通信フロー

```
SynScanPro → シリアルポート(In) → アプリケーション → シリアルポート(Out) → モーターコントローラー
                                           ↓
                                    ログ表示・解析
モーターコントローラー → シリアルポート(Out) → アプリケーション → シリアルポート(In) → SynScanPro
                                                     ↓
                                              ログ表示・解析
```

## 主要コマンド形式

### SynScanPro → コントローラー

コマンドは `:` で始まり、次のような形式です：

```
:<コマンド><チャンネル><パラメータ...>
```

**コマンド例:**
- `:b0` - タイマー割り込み周波数問い合わせ
- `:e0` - モーターボード版問い合わせ
- `:f0` - ステータス問い合わせ
- `:P101` - AutoGuide Speed 設定（パラメータあり）
- `:Xga<params>` - Extended コマンド

### コントローラー → SynScanPro

レスポンスは `=` で始まり、16進データが続きます：

```
=<ヘキサデータ>
```

エラーレスポンスは `!` で始まります：

```
!
```

## データフォーマット

- 16進表示は 8 文字ごとにスペースで区切られます
- 例：`12345678 9ABCDEF0`

### Sky-Watcher HEX デコード

モーターコントローラーからのデータは Sky-Watcher 独自の形式でエンコードされており、以下のルールに従います：
- 複数バイトの数値はリトルエンディアン形式（`DecodeSkyWatcherHexToLong` メソッド）

## ファイル構成

```
SkyWatcherMotorControllerMonitor/
├── Program.cs                      # アプリケーション エントリポイント
├── MainForm.cs                     # メインウィンドウロジック
├── MainForm.Designer.cs            # UI デザイナー生成コード
├── MainForm.resx                   # フォームリソース
├── SkyWatcherMotorControllerMonitor.csproj
├── SkyWatcherMotorControllerMonitor.sln
├── Properties/
│   ├── AssemblyInfo.cs
│   ├── Resources.Designer.cs
│   └── Settings.Designer.cs
└── README.md                       # このファイル
```

## トラブルシューティング

### ポートが認識されない

- Windows デバイスマネージャーで COM ポートが認識されているか確認
- USB シリアルアダプタの場合、ドライバが正しくインストールされているか確認
- アプリを再起動し、ポートのリフレッシュを試みる

### データが表示されない

- ポートが正しく選択されているか確認
- SynScanPro が正しいポート（ボーレート：9600 等）を使用しているか確認
- 結線を確認（TXD ↔ RXD）

### 文字化けが表示される

- ボーレートやパリティが一致しているか確認
- シリアルケーブルが正常か確認

## ライセンス

MIT License

Copyright (c) 2026 takesa03

このプロジェクトは MIT ライセンスの下で公開されています。
詳細は [LICENSE](LICENSE) ファイルを参照してください。

## 作成者

Sky-Watcher 天体望遠鏡用ユーティリティ

## 参考資料

- SkyWatcher コントローラープロトコル仕様書
- SynScanPro ドキュメント
