@echo off
rem #----------------------------------------------------
rem # ■WPポイントカードシステムのバックアップ処理 Ver.1
rem #   2016/05/26 OA-Center Create
rem #   2016/06/01 変更: 1週間分の履歴を残す
rem #   2016/06/06 変更: ログ出力を追加
rem #----------------------------------------------------

echo WPポイントカードシステムのバックアップ処理を実行します。

sqlcmd -S .\SQLEXPRESS -U sa -P wp0012001 -Q "BACKUP DATABASE POINTCARD TO DISK='C:\WPPointCard\WP_Point_Data.bak' WITH INIT" > "C:\WPPointCard\WP_Point_Backup.log"

set logfile=d:\WPPointCard_BackUp\BackUp.log
set prefix=Backup_
set datetime=%DATE:~-10,4%%DATE:~-5,2%%DATE:~-2%%TIME:~0,2%%TIME:~3,2%%TIME:~6,2%
echo C:\WPPointCard を d:\WPPointCard_BackUp\%prefix%%datetime%にコピーします。 >> %logfile%
xcopy C:\WPPointCard d:\WPPointCard_BackUp\%prefix%%datetime% /D /S /E /H /C /Y /R /I >> %logfile%

rem 1週間以上前のバックアップフォルダを削除
forfiles /P d:\WPPointCard_BackUp /m %prefix%* /D -7 /c "cmd /c echo @path を削除します。 >> %logfile% & rmdir /S /Q @path 2>> %logfile%"

echo.
echo.
echo WPポイントカードシステムのバックアップ処理が完了しました。

:END
