param(
    # Parameter help description
    [Parameter(Position=0)]
    [ValidateSet("install","uninstall","start","stop","status")]
    [System.String]
    $action
)

Write-Host "Executing action $action"

$exeName = "LaunchProcessInCurrentUserService.exe"
$exePath = [System.IO.Path]::Combine($PSScriptRoot, $exeName)
$serviceName = "A NotepadLauncher +"
$displayName = $serviceName
$serviceDescription = "A NotepadLauncher +"

<# docs
    https://stackoverflow.com/questions/15085856/using-sc-to-install-a-windows-service-and-then-set-recovery-properties
    https://docs.microsoft.com/en-us/previous-versions/windows/it-pro/windows-server-2012-r2-and-2012/cc990289(v=ws.11)
#>

<# 
sc [<ServerName>] create [<ServiceName>] 
# [type= {own | share | kernel | filesys | rec | interact 
#   type= {own | share}}] 
# [start= {boot | system | auto | demand | disabled}] # 
# [error= {normal | severe | critical | ignore}] 
# [binpath= <BinaryPathName>] 
# [group= <LoadOrderGroup>] 
# [tag= {yes | no}] 
# [depend= <dependencies>] 
# [obj= {<AccountName> | <ObjectName>}] 
# [displayname= <DisplayName>] [password= <Password>]
#>

$fullFailureReset = [System.TimeSpan]::FromHours(24).TotalSeconds
$autoRestartDelay = [System.TimeSpan]::FromMinutes(1).TotalSeconds

if($action -eq "install"){
    Write-Host "Installing service $serviceName at path $exePath"
    Invoke-Expression "sc.exe create ""$serviceName"" binPath=$exePath DisplayName= ""$displayName"" start= auto"
    Invoke-Expression "sc.exe description ""$serviceName"" ""$serviceDescription"""
    Invoke-Expression "sc.exe failure ""$serviceName"" reset= $fullFailureReset actions= restart/$autoRestartDelay/restart/$autoRestartDelay/restart/$autoRestartDelay"
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
