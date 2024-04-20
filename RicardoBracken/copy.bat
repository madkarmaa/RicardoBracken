@echo off

FOR %%I IN (.) DO SET CurrentD=%%~nI%%~xI

md "%APPDATA%\Thunderstore Mod Manager\DataFolder\LethalCompany\profiles\Default\BepInEx\plugins\%CurrentD%\" 2> nul

xcopy /Y /S ".\bin\Debug\netstandard2.1\*.dll" "..\thunderstore\"
xcopy /Y /S ".\AssetBundles" "..\thunderstore\assets\"

xcopy /Y /S "..\thunderstore" "%APPDATA%\Thunderstore Mod Manager\DataFolder\LethalCompany\profiles\Default\BepInEx\plugins\%CurrentD%\"