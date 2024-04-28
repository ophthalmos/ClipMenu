#define MyAppName "ClipMenu"
#define MyAppVersion "1.0.0.1"

[Setup]
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
VersionInfoVersion={#MyAppVersion}
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=admin
AppPublisher=Wilhelm Happe
VersionInfoCopyright=(C) 2024, W. Happe
AppPublisherURL=https://www.ophthalmostar.de/
AppSupportURL=https://www.ophthalmostar.de/
AppUpdatesURL=https://www.ophthalmostar.de/
;DefaultDirName={reg:HKCU\Software\ClipMenu,InstallPath|{autopf}\{#MyAppName}}
DefaultDirName={autopf}\{#MyAppName}
DisableWelcomePage=yes
DisableDirPage=no
DisableReadyPage=yes
CloseApplications=yes
WizardStyle=modern
WizardSizePercent=100
SetupIconFile=ClipMenu.ico
UninstallDisplayIcon={app}\ClipMenu.exe
DefaultGroupName=ClipMenu
AppId=ClipMenu
TimeStampsInUTC=yes
OutputDir=.
OutputBaseFilename={#MyAppName}Setup
Compression=lzma2/max
SolidCompression=yes
DirExistsWarning=no
MinVersion=0,10.0
;AppMutex={#MyAppName}_MultiStartPrevent => s. [Code]
;SignTool=sha256
;Uninstallable=not IsTaskSelected('portablemode')

;[Tasks]
;Name: portablemode; Description: "Portable Mode"
[Languages]
Name: "German"; MessagesFile: "compiler:Languages\German.isl"

[Files]
Source: "bin\x64\Release\net8.0-windows\ClipMenu.exe"; DestDir: "{app}"; Permissions: users-modify; Flags: ignoreversion
Source: "bin\x64\Release\net8.0-windows\{#MyAppName}.dll"; DestDir: "{app}"; Permissions: users-modify; Flags: ignoreversion
Source: "bin\x64\Release\net8.0-windows\{#MyAppName}.runtimeconfig.json"; DestDir: "{app}"; Permissions: users-modify; Flags: ignoreversion
Source: "Lizenzvereinbarung.txt"; DestDir: "{app}"; Permissions: users-modify;

[Icons]
Name: "{userdesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppName}.exe"
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppName}.exe"


[UnInstallDelete]
Type: filesandordirs; Name: {userappdata}\{#MyAppName}\{#MyAppName}*
Type: dirifempty; Name: {userappdata}\{#MyAppName}

[Registry]
Root: HKCU; Subkey: "Software\{#MyAppName}"; Flags: uninsdeletekey
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "ClipMenu"; Flags: dontcreatekey uninsdeletevalue

[Run]
Filename: "{app}\{#MyAppName}.exe"; Verb: runas; Description: "Launch {#MyAppName}"; Flags: postinstall nowait skipifsilent shellexec

[Messages]
BeveledLabel=
WinVersionTooLowError=Das Programm erfordert eine höhere Windowsversion.
ConfirmUninstall=Möchten Sie %1 und alle Komponenten entfernen? Eine Deinstallation ist vor einem Update nicht erforderlich.

[CustomMessages]
RemoveSettings=Möchten Sie die Einstellungsdatei ebenfalls entfernen?

[Code]
const
  SetupMutexName = 'ClipMenuSetupMutex';
  
function InitializeSetup(): Boolean; // only one instance of Inno Setup without prompting
begin
  Result := True;
  if CheckForMutexes(SetupMutexName) then
  begin
    Result := False; // Mutex exists, setup is running already, silently aborting
  end
    else
  begin
    CreateMutex(SetupMutexName); 
  end;
end;

procedure CurUninstallStepChanged (CurUninstallStep: TUninstallStep);
var
  mres : integer;
begin
  case CurUninstallStep of                   
    usPostUninstall:
      begin
        mres := MsgBox(CustomMessage('RemoveSettings'), mbConfirmation, MB_YESNO or MB_DEFBUTTON2)
        if mres = IDYES then
          begin
          DelTree(ExpandConstant('{userappdata}\{#MyAppName}'), True, True, True);
          RegDeleteKeyIncludingSubkeys(HKEY_CURRENT_USER, 'Software\ClipMenu');
          end;
      end;
  end;
end; 

procedure DeinitializeSetup();
var
  FilePath: string;
  BatchPath: string;
  S: TArrayOfString;
  ResultCode: Integer;
begin
  if ExpandConstant('{param:deleteSetup|false}') = 'true' then
  begin
    FilePath := ExpandConstant('{srcexe}');
    begin
      BatchPath := ExpandConstant('{%TEMP}\') + 'delete_' + ExtractFileName(ExpandConstant('{tmp}')) + '.bat';
      SetArrayLength(S, 7);
      S[0] := ':loop';
      S[1] := 'del "' + FilePath + '"';
      S[2] := 'if not exist "' + FilePath + '" goto end';
      S[3] := 'goto loop';
      S[4] := ':end';
      S[5] := 'rd "' + ExpandConstant('{tmp}') + '"';
      S[6] := 'del "' + BatchPath + '"';
      if SaveStringsToFile(BatchPath, S, True) then
      begin
        Exec(BatchPath, '', '', SW_HIDE, ewNoWait, ResultCode)
      end;
    end;
  end;
end;

procedure InitializeWizard;
var
  StaticText: TNewStaticText;
begin
  StaticText := TNewStaticText.Create(WizardForm);
  StaticText.Parent := WizardForm.FinishedPage;
  StaticText.Left := WizardForm.FinishedLabel.Left;
  StaticText.Top := WizardForm.FinishedLabel.Top + 120;
  StaticText.Font.Style := [fsBold];
  StaticText.Caption := 'Das Programm benötigt Administratorrechte,'#13'um Eingaben in andere Anwendungen einzufügen.'#13#13 + 
'Die UIPI (User Interface Privilege Isolation) ermög-'#13'licht Eingaben nur in Anwendungen mit gleicher'#13'oder geringerer Integritätsebene.';
end;
