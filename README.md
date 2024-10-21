# RTSP Frame Capture and SignalR Communication

## Overview

This repository contains two .NET applications:

- **RTSPListener**: Captures frames from an RTSP stream and sends them via SignalR.
- **ImageReceiver**: Receives images via SignalR and saves them to the local file system.

> **Note**: The **ImageReceiver** application depends on the **RTSPListener**. When you run the project, both applications start automatically.

---

## Prerequisites

- **.NET 8.0 SDK**: Ensure that the .NET 8 SDK is installed on your machine.
- **VLC Media Player**: Used to stream your webcam over RTSP.
- **Visual Studio 2022** or **Visual Studio Code**: For editing and running the code.

---

## Setting Up the RTSP Stream with VLC

### 1. Download and Install VLC

- Download VLC Media Player from the official website.
- Follow the standard installation instructions for your operating system.

### 2. Configure VLC to Stream Your Webcam

1. **Open VLC Media Player**.
2. Go to **Media** > **Stream...** 
3. In the **Open Media** window, select the **Capture Device** tab.
    - **Capture mode**: Select **DirectShow** (on Windows) or the appropriate option for your OS.
    - **Video device name**: Select your webcam.
    - **Audio device name**: Select your microphone (optional).
4. Click the **Stream** button.
5. In the **Stream Output** window, confirm your settings and click **Next**.
6. Under **Destinations**, do the following:
    - **New destination**: Select **RTSP**.
    - Click **Add**.
    - **Address**: Leave it blank or enter `localhost`.
    - **Port**: Use `8554` 
    - **Path**: Enter `/webcam`.
7. Click **Next**.
8. In **Transcoding Options**, you can choose a profile or leave it as default.
9. Click **Next**.
10. In the final window, click **Stream**.

**Your webcam is now streaming over RTSP at `rtsp://localhost:8554/webcam`.**

---

## Running the Project

### 1. Clone the Repository

Open a terminal and clone the repository:

bash

`git clone https://github.com/your-username/your-repository.git cd your-repository`

### 2. Restore Dependencies

For both projects, run:

bash

`dotnet restore`

### 3. Run the Project

Since **ImageReceiver** depends on **RTSPListener**, we'll run the main project that starts both applications.

#### Using Visual Studio:

1. Open the solution in Visual Studio.
2. Set the startup project to the main project that initiates both applications.
3. Press `F5` or click **Start Debugging**.

#### Using the Terminal:

If the project is configured to start both applications when running, simply execute:

bash

`dotnet run`

> **Note**: Ensure you are in the root directory of the solution when running the above command.

### 4. Ensure the RTSP Stream is Active

Before starting the applications, make sure the RTSP stream via VLC is active as described in the previous section.

---

## Project Explanation

- **RTSPListener**:
    - Uses OpenCvSharp to connect to the RTSP stream.
    - Captures frames from the stream.
    - Sends the frames as byte arrays via SignalR to **ImageReceiver**.
    
- **ImageReceiver**:
    - Acts as a SignalR server.
    - Receives the frames sent by **RTSPListener**.
    - Saves the frames as images in the `ReceivedImages` directory.

---

## Important Directories and Files

- **RTSPListener/Program.cs**: Main code that captures and sends frames.
- **ImageReceiver/Program.cs**: SignalR server setup and logic to save images.
- **ReceivedImages/**: Directory where the received images are saved.

---

## Additional Tips

- **Adjust the RTSP URL**: If your RTSP stream URL is different, update the `rtspUrl` variable in `RTSPListener/Program.cs`.
    
- **Check Ports and Firewall**:
    
    - **Port 5000**: Used by SignalR. Ensure it is open.
    - **Port 8554**: Used by VLC for the RTSP stream.
---

## Troubleshooting

- **Cannot Access the RTSP Stream**:
    - Verify that VLC is streaming correctly.
    - Ensure the RTSP URL is correct and accessible
	
- **SignalR Connection Errors**:
    - Make sure **ImageReceiver** is running before **RTSPListener**.
    - Check that the SignalR Hub URL is correct in `RTSPListener/Program.cs`.
    
- **Images Are Not Being Saved**:
    - Confirm that the `ReceivedImages` directory is being created.
    - Check write permissions for the directory.
    
- **Issues with OpenCvSharp**:
    - Ensure that the native OpenCV dependencies are installed.
    - For Windows systems, the `OpenCvSharp4.runtime.win` package should resolve dependencies.

---
## Contact

If you have any questions or need further assistance, feel free to reach out:

- **Email**: lucasp4iva@gmail.com
- **GitHub**: [bylucaspaiva](https://github.com/bylucaspaiva)