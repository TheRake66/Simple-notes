#AutoIt3Wrapper_Icon=icon.ico
#AutoIt3Wrapper_Res_Description=Post-it-3
#AutoIt3Wrapper_Res_Fileversion=1.1.0.0
#AutoIt3Wrapper_Res_ProductName=Post-it-3
#AutoIt3Wrapper_Res_ProductVersion=1.1.0.0
#AutoIt3Wrapper_Res_Legalcopyright=TheRake66

#NoTrayIcon

$PROCESSNUMBER = ProcessList(@ScriptName)
If $PROCESSNUMBER[0][0] > 1 Then
	Exit
EndIf

FileCreateShortcut(@ScriptFullPath, @StartupDir & "\" & @ScriptName & ".lnk")

$XSIZE = IniRead(@AppDataDir & "\Post-it-3.ini", "Options", "SizeX", 350)
$YSIZE = IniRead(@AppDataDir & "\Post-it-3.ini", "Options", "SizeY", 300)
$XPOS = IniRead(@AppDataDir & "\Post-it-3.ini", "Options", "PosX", @DesktopWidth-358)
$YPOS = IniRead(@AppDataDir & "\Post-it-3.ini", "Options", "PosY", 0)
$COLOR = IniRead(@AppDataDir & "\Post-it-3.ini", "Options", "Color", 0xF7FF3C)
$NUMBER = IniRead(@AppDataDir & "\Post-it-3.ini", "Options", "Number", 1)

; 0xF7FF3C = Jaune citron
; 0x87CEEB = Bleu ciel
; 0xFF69B4 = Magenta hot pink
; 0xADFF2F = Vert jaune

$NOBAR = GUICreate("Cette gui supprime l'icon de la bar des taches")
$MAIN = GUICreate("Post-it-3", $XSIZE, $YSIZE, $XPOS, $YPOS, 0x00040000, 0x00000080, $NOBAR)

$OPENSAVE = FileOpen(@AppDataDir & "\Post-it-3.save")
$READSAVE = FileRead($OPENSAVE)
FileClose($OPENSAVE)
If $READSAVE = "" Then $READSAVE = "Bienvenue dans Post-it-3 !"
$EDIT = GUICtrlCreateEdit($READSAVE, 0, 0, $XSIZE+50, $YSIZE)
	GUICtrlSetBkColor(-1, $COLOR)
	GUICtrlSetFont(-1, 26, 0, 0, "Chiller")
	GUICtrlSetResizing(-1, 2+4+32+64)

$MENUOPTIONS = GUICtrlCreateMenu("Options")
$MENUOPTIONSSAVE = GUICtrlCreateMenuItem("Sauvegarder", $MENUOPTIONS)
GUICtrlCreateMenuItem("", $MENUOPTIONS)
$MENUOPTIONSRESET = GUICtrlCreateMenuItem("Réinitialiser", $MENUOPTIONS)
GUICtrlCreateMenuItem("", $MENUOPTIONS)
$MENUOPTIONSQUIT = GUICtrlCreateMenuItem("Quitter	ESC", $MENUOPTIONS)

$MENUCOLOR = GUICtrlCreateMenu("Couleur")
$MENUCOLOR1 = GUICtrlCreateMenuItem("Jaune citron", $MENUCOLOR, -1, 1)
If $COLOR = 0xF7FF3C Then GUICtrlSetState(-1, 1)
$MENUCOLOR2 = GUICtrlCreateMenuItem("Bleu ciel", $MENUCOLOR, -1, 1)
If $COLOR = 0x87CEEB Then GUICtrlSetState(-1, 1)
$MENUCOLOR3 = GUICtrlCreateMenuItem("Magenta hot pink", $MENUCOLOR, -1, 1)
If $COLOR = 0xFF69B4 Then GUICtrlSetState(-1, 1)
$MENUCOLOR4 = GUICtrlCreateMenuItem("Vert jaune", $MENUCOLOR, -1, 1)
If $COLOR = 0xADFF2F Then GUICtrlSetState(-1, 1)



GUISetState(@SW_SHOW, $MAIN)

$TIMERINIT = TimerInit()

Do
	$GUIMSG = GUIGetMsg()
	
	If TimerDiff($TIMERINIT) >= 5000 Or $GUIMSG = $MENUOPTIONSSAVE Then
		WinSetTitle($MAIN, "", "Post-it-3 (sauvegarde..)")
		$ARRAYPOS = WinGetPos($MAIN)
		IniWrite(@AppDataDir & "\Post-it-3.ini", "Options", "SizeX", $ARRAYPOS[2]-14)
		IniWrite(@AppDataDir & "\Post-it-3.ini", "Options", "SizeY", $ARRAYPOS[3]-14)
		IniWrite(@AppDataDir & "\Post-it-3.ini", "Options", "PosX", $ARRAYPOS[0])
		IniWrite(@AppDataDir & "\Post-it-3.ini", "Options", "PosY", $ARRAYPOS[1])
		IniWrite(@AppDataDir & "\Post-it-3.ini", "Options", "Color", $COLOR)
		$OPENSAVE2 = FileOpen(@AppDataDir & "\Post-it-3.save", 2)
		FileWrite($OPENSAVE2 , GUICtrlRead($EDIT))
		FileClose($OPENSAVE2)
		$TIMERINIT = TimerInit()
		WinSetTitle($MAIN, "", "Post-it-3")
	EndIf
	
	If $GUIMSG = $MENUOPTIONSRESET Then
		FileDelete(@AppDataDir & "\Post-it-3.ini")
		FileDelete(@AppDataDir & "\Post-it-3.save")
		GUICtrlSetData($EDIT, "")
		WinMove($MAIN, "", @DesktopWidth-358, 0, 364, 314)
	EndIf
	
	If $GUIMSG = $MENUCOLOR1 Then
		$COLOR = 0xF7FF3C
		GUICtrlSetBkColor($EDIT, $COLOR)
	EndIf
	
	If $GUIMSG = $MENUCOLOR2 Then
		$COLOR = 0x87CEEB
		GUICtrlSetBkColor($EDIT, $COLOR)
	EndIf
	
	If $GUIMSG = $MENUCOLOR3 Then
		$COLOR = 0xFF69B4
		GUICtrlSetBkColor($EDIT, $COLOR)
	EndIf
	
	If $GUIMSG = $MENUCOLOR4 Then
		$COLOR = 0xADFF2F
		GUICtrlSetBkColor($EDIT, $COLOR)
	EndIf

Until $GUIMSG = -3 Or $GUIMSG = $MENUOPTIONSQUIT

FileClose($OPENSAVE2)
Exit








