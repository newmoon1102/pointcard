@echo off
rem #----------------------------------------------------
rem # ��WP�|�C���g�J�[�h�V�X�e���̃o�b�N�A�b�v���� Ver.1
rem #   2016/05/26 OA-Center Create
rem #   2016/06/01 �ύX: 1�T�ԕ��̗������c��
rem #   2016/06/06 �ύX: ���O�o�͂�ǉ�
rem #----------------------------------------------------

echo WP�|�C���g�J�[�h�V�X�e���̃o�b�N�A�b�v���������s���܂��B

sqlcmd -S .\SQLEXPRESS -U sa -P wp0012001 -Q "BACKUP DATABASE POINTCARD TO DISK='C:\WPPointCard\WP_Point_Data.bak' WITH INIT" > "C:\WPPointCard\WP_Point_Backup.log"

set logfile=d:\WPPointCard_BackUp\BackUp.log
set prefix=Backup_
set datetime=%DATE:~-10,4%%DATE:~-5,2%%DATE:~-2%%TIME:~0,2%%TIME:~3,2%%TIME:~6,2%
echo C:\WPPointCard �� d:\WPPointCard_BackUp\%prefix%%datetime%�ɃR�s�[���܂��B >> %logfile%
xcopy C:\WPPointCard d:\WPPointCard_BackUp\%prefix%%datetime% /D /S /E /H /C /Y /R /I >> %logfile%

rem 1�T�Ԉȏ�O�̃o�b�N�A�b�v�t�H���_���폜
forfiles /P d:\WPPointCard_BackUp /m %prefix%* /D -7 /c "cmd /c echo @path ���폜���܂��B >> %logfile% & rmdir /S /Q @path 2>> %logfile%"

echo.
echo.
echo WP�|�C���g�J�[�h�V�X�e���̃o�b�N�A�b�v�������������܂����B

:END
