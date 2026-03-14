Project Context

Project Name: ToolCoreSuite

Language: C#

Framework: .NET 8

UI Framework: WPF

IDE: Visual Studio 2022

Deployment Goal:
Single-file portable .exe with all runtime data stored in a folder beside the executable.

Solution Structure
ToolCoreSuite

ToolShell.Wpf
  WPF UI application
  Application startup
  MainWindow

ToolCore
  Shared models
  Interfaces
  AppServices container
  Path utilities

ToolCore.Logging
  Serilog logging implementation

ToolCore.Configuration
  JSON configuration system
Application Folder Structure

When the application runs:

AppName.exe

AppName_Data/
    Logs/
    Config/
        appsettings.json
    Licenses/
    Temp/
Implemented Systems
Logging

Implemented using Serilog

Daily rolling logs

Log retention limit

File size limit

Logs stored in:

AppName_Data/Logs

Logging is accessed through:

AppServices.Logger
Configuration

Configuration stored as JSON:

AppName_Data/Config/appsettings.json

Loaded at startup using:

ConfigurationService

Settings model:

AppSettings
{
    int LogRetentionDays
    int MaxLogFileSizeMB
    bool CheckForUpdatesOnStartup
}

Accessed via:

AppServices.Configuration.Settings
Shared Services

Global services stored in:

AppServices

Example:

AppServices.Logger
AppServices.Configuration
Application Startup Flow
Application Start
     ↓
Create App Data folders
     ↓
Load configuration
     ↓
Initialize logging
     ↓
Launch MainWindow
Current Development Stage

Framework skeleton for reusable engineering tools.

Core components implemented:

portable application folder structure

logging system

configuration system

service container

Future tools will reuse this framework.

Planned Future Features

Settings window

Licensing system

Automatic update checker

Bug report uploader

Shared base window layout

Tool plugin architecture

Development Goals

Create a reusable framework for building small engineering utilities such as:

PLC signal logger

Alarm log analyzer

PLC tag documentation generator

PLC → SQL data bridge

Industrial network scanner

Tools should be:

lightweight

portable

easy to distribute

low maintenance

When asking for help

Typical requests might include:

improving architecture

adding new services

creating reusable UI components

improving logging/config systems

adding licensing or update systems