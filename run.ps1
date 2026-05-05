# 编译并运行 War3Trainer
$ErrorActionPreference = "Continue"

$msbuild = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
$exePath = ".\War3Trainer\bin\Release\War3Trainer.exe"

Write-Host "正在尝试结束已运行的进程..." -ForegroundColor Cyan
try {
    # 尝试关闭已有的进程，避免文件被占用导致编译失败
    Stop-Process -Name "War3Trainer" -Force -ErrorAction SilentlyContinue
} catch {}

Write-Host "正在编译项目..." -ForegroundColor Cyan
& $msbuild War3Trainer.sln /p:Configuration=Release

if ($LASTEXITCODE -ne 0) {
    Write-Host "编译失败，请检查上方错误信息！如果提示文件被占用，请手动关闭修改器窗口。" -ForegroundColor Red
    Pause
    exit
}

Write-Host "编译成功，正在启动程序..." -ForegroundColor Green
if (Test-Path $exePath) {
    # 使用 Start-Process 启动新进程，不会阻塞当前控制台
    Start-Process $exePath
} else {
    Write-Host "找不到可执行文件: $exePath" -ForegroundColor Red
    Pause
}
