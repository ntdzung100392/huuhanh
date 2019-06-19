@echo off
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe MSBuildMigrationRunner.csproj ^
/t:migrate-rollback-all ^
/p:DatabaseProvider=sqlserver2008 ^
/p:ConnectionStringConfigPath=ConnectionStrings.config ^
/p:ConnectionStringName=SymphonyMigrations ^
/p:MigrationsProjectName=Migrations ^
/p:MigrationsProjectRootPath=. ^
/p:MigratorReferencesDirectory=..\..\..\..\NuGetPackages\FluentMigrator.Tools.1.6.2\tools\AnyCPU\40\
/verbosity:d

pause
