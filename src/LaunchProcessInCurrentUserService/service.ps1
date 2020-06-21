param(
    # Parameter help description
    [Parameter(Position=0)]
    [ValidateSet("install","uninstall","start","stop","status")]
    [System.String]
    $action
)

Write-Host "Executing action $action"

$exePath = "LaunchProcessInCurrentUserService.exe"
$serviceName = "A NotepadLauncher +"

if($action -eq "install"){
    Invoke-Expression "sc.exe create ""$serviceName"" binPath=$exePath"
}

if($action -eq "uninstall"){
    Invoke-Expression "sc.exe delete ""$serviceName"""
}

if($action -eq "start"){
    Invoke-Expression "sc.exe start ""$serviceName"""
}

if($action -eq "stop"){
    Invoke-Expression "sc.exe stop ""$serviceName"""
}

if($action -eq "status"){
    Invoke-Expression "sc.exe query ""$serviceName"""
}
