# Worker Service 添加 Serilog 日志

基于 WorkerServiceGracefullyShutdown 项目修改。

```bash
dotnet new worker -n "MyService"
```

## 添加必要的依赖库

Serilog 文档：<https://serilog.net/>

```bash
dotnet add package Serilog
dotnet add package Serilog.Settings.Configuration
dotnet add package Serilog.Extensions.Hosting
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.RollingFile
dotnet add package Serilog.Enrichers.Thread
dotnet add package Serilog.Enrichers.Environment
dotnet add package Serilog.Enrichers.Process
```

## 修改文件

修改的文件包含： *appsettings.json*, *Program.cs*

## 运行

```bash
dotnet build
dotnet run
```
