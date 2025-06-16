
# RCC – Remote Connection Center

[![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey)](#)
[![Built with](https://img.shields.io/badge/Built_with-C%23-239120?logo=c-sharp)](#)
[![Issues](https://img.shields.io/github/issues/Minimal-Project/Remote-Connection-Center)](https://github.com/Minimal-Project/Remote-Connection-Center/issues)
[![Last Commit](https://img.shields.io/github/last-commit/Minimal-Project/Remote-Connection-Center)](https://github.com/Minimal-Project/Remote-Connection-Center/commits)

**RCC** (Remote Connection Center) is a lightweight Windows tool that simplifies connecting to multiple servers through various protocols, including **SSH**, **RDP**, and **WEB**.

It provides a user-friendly interface for managing server connections and offers quick access to different server environments, all from a single application.

This project is built using C#, designed to make it easy for system administrators, developers, and technicians to manage server connections efficiently.

## Features

- **Multi-Protocol Support**  
  Supports connections via **SSH**, **RDP** (Windows Remote Desktop), and **WEB** (for accessing web interfaces such as Proxmox, routers, admin panels etc.).

- **Server Configuration via JSON**
  The servers are loaded from the `servers.json` file, which makes it easier to change, save and restore server configurations.

- **Visual Server Cards**  
  Servers are displayed as interactive cards in a scrollable panel.  
  Each card shows the server’s name, connection details, and a Connect button to initiate the connection.

- **Backup & Restore Configurations**  
  Easily back up the current configuration and restore it from a previous backup with just a few clicks.

- **One-Click Connection**  
  Simply click a server card to automatically open the appropriate connection tool (e.g., `ssh`, `mstsc.exe`, or a web browser).

## Installation

You can download the latest release of RCC from the [Releases page](https://github.com/Minimal-Project/Server-Manager/releases).

1. Go to the **[Releases](https://github.com/Minimal-Project/Server-Manager/releases)** section.
2. Download the latest `.zip` or `.exe` file.
3. Extract the files (if you downloaded a `.zip`) and run the executable to launch RCC.

Alternatively, you can clone the repository and build the project yourself:

```bash
git clone https://github.com/Minimal-Project/Remote-Connection-Center.git
```

Open it in Visual Studio (or another C# editor), build and run the project.

## Configuration File (`servers.json`)

The application loads server configurations from a `servers.json` file. Here is an example of how the file is structured:

```json
[
    {
        "Name": "Server 1",
        "Host": "192.168.1.1",
        "Port": 22,
        "User": "user1",
        "Mode": "SSH"
    },
    {
        "Name": "Server 2",
        "Host": "192.168.1.2",
        "Port": 22,
        "User": "user2",
        "Mode": "SSH"
    },
    {
        "Name": "Server 3",
        "Host": "192.168.1.3",
        "Port": 3389,
        "Mode": "RDP"
    },
    {
        "Name": "Server 4",
        "Url": "example.com",
        "Port": 443,
        "Mode": "WEB"
    }
]
```

Make sure to adjust the `Host`, `Port`, `User`, and `Mode` fields to match your server configurations.

## Usage

1. **Load Configurations:**  
   Upon startup, the application will load the server configurations from `servers.json`. You can edit this file to add or modify server entries.

2. **Connect to a Server:**  
   To connect to a server, simply click the **Connect** button on the respective server card. The application will open the appropriate tool (e.g., SSH terminal, RDP client, or web browser).

3. **Backup and Restore:**  
   - To back up your current configuration, click the **Backup Config** button.
   - To restore a previous backup, click the **Restore Backup** button and select your `.bak` file.

## Contributing

Contributions to RCC are welcome!  
If you have ideas for new features or improvements, feel free to open an issue via the [issue tracker](https://github.com/Minimal-Project/Remote-Connection-Center/issues).

## Acknowledgments

- Special thanks to [SichelBichel](https://github.com/SichelBichel) for his support on this project.
