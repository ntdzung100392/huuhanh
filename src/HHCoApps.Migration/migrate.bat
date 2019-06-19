@echo off
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe MSBuildMigrationRunner.csproj ^
/t:migrate ^
/p:DatabaseProvider=sqlserver2017 ^
/p:ConnectionStringConfigPath=ConnectionStrings.config ^
/p:ConnectionStringName=HHCoAppsMigrations ^
/p:MigrationsProjectName=HHCoApps.Migration ^
/p:MigrationsProjectRootPath=. ^
/p:MigratorReferencesDirectory=..\packages\FluentMigrator.Tools.3.1.3\net461\any\
/verbosity:d

pause
