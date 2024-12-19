## Introduction 📢

<div>

  <div align="left">
    <img align="left" src="https://github.com/user-attachments/assets/0936a5b6-b2b9-428b-af07-c309d2c41f0f" alt="Pomodoro app screen" title="Pomodoro app screen" width="400">
    <br>
  </div>
<br>

<h3>Idea 💡</h3>
<p><b>The Pomodoro Time Manager is a lightweight application designed to help individuals stay focused and manage their time effectively.</b></p>
<p>Originally, I built this app for my personal use to help me stay productive during my studies and projects. Creating tools for my own needs is one of the best things in programming and
  I'm sharing this app hoping others can also make use of it.</p>
<br>

</div>

## Tech Stack 🛠️
<ul>
    <p><strong>Language:</strong> C# (.NET 8+)</p>
    <p><strong>UI Framework:</strong> Avalonia (Cross-Platform UI Toolkit)</p>
    <p><strong>Platform:</strong> .NET 8+</p>
    <p><strong>Asynchronous Programming:</strong> async/await</p>
</ul> 

## Features ✨
- ✅ **Pomodoro Timer**: A built-in timer that automatically alternates between work and rest sessions.
- ✅ **Customizable Work/Break Duration**: Easily adjust the time intervals to suit your workflow.
- ✅ **Manual switching**: Seamlessly navigate to previous or next session whenever needed.

## Roadmap 🗺️
- [ ] 🎯 Include audio notification for session completion
- [ ] 🎯 Enable theme customization
- [ ] 🎯 Create session history view

## Installation
#### 1. Clone the repository:
```bash
git clone https://github.com/panevka/PomodoroProductivityApp.git
```
#### 2. Navigate to project directory:
```bash
cd PomodoroProductivityApp
```
#### 3. Clean build environment & restore dependencies
```bash
dotnet clean
dotnet restore
```
#### 4. Build the Project [^1]
[^1]: If you are a linux user, replace `win-x64` with `linux-x64` during build

- **For Self-Contained Single-File Deployment:**  
  Use the following command to create a self-contained executable packaged as a single file:

  ```bash
  dotnet publish -c Release -r win-x64 /p:PublishSingleFile=true --self-contained true -o ./publish
  ```
- **For Non-Self-Contained Deployment (separate binaries):**  
  Use the following command to create a self-contained deployment that includes all dependencies but places them in separate files rather than bundling everything into a single executable:
  ```bash
  dotnet publish -c Release -r win-x64 --self-contained true -o ./publish
  ```

- **Deployment for a Target Runtime**
  Use the following command to publish create platform-specific executable without bundling the .NET runtime. It assumes the target system already has the .NET runtime installed:
  
  ```bash
  dotnet publish -c Release -r win-x64 -o ./publish
  ```
  Alternatively you can also download self-contained, single-file executable from [releases](https://github.com/panevka/PomodoroProductivityApp/releases/tag/v0.1.0).
#### 5. Open the app[^2]
[^2]: On linux you might have to execute `chmod +x ./PomodoroApp` first, before running `./PomodoroApp`
```bash
cd publish
./PomodoroApp.exe
```
