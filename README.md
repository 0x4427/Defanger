# Defanger
A Notepad++ plugin to neutralize and restore the functionality of Indicators of Compromise (IOCs) by defanging and refanging them.

- Selective or whole-text defanging: You can choose between defanging/refanging selected portions of the text or applying the process to the entire content of the Notepad++ document. This makes it convenient for handling IOCs of varying lengths and complexities.
- Supports full URLs, valid domains, IPv4 and IPv6 addresses. 

# Installation

1. Create `Defanger` folder in Notepad++'s plugins installation folder.
2. Copy `Defanger.dll` from the Defanger [release](https://github.com/0x4427/Defanger/releases) zip file into the newly created folder. Please use the correct archive version based on your Notepad++ architecture - x86, x64.
3. Restart Notepad++ and you are all set.
4. Alternatively, download the source and build it - the build will copy the dll to the Notepad++'s plugins folder.

# Usage

- When applied to the selected text `Defang` defangs any valid URLs, domains and IP addresses whichever included in the selected portion.

![sample3 (2)](https://github.com/0x4427/Defanger/assets/91937971/412809e7-16a7-4944-b1a6-ac39e5e70890)

- Selected Defang

![sample4](https://github.com/0x4427/Defanger/assets/91937971/0058ede8-fe50-422b-9461-3f334352ab02)

- `Auto Defang All` defangs all the valid URLs, domains and IP addresses found in the document while excluding the invalid ones.

![new1](https://github.com/0x4427/Defanger/assets/91937971/1ae6a97a-0b5f-439c-9023-276a1fa4aafb)

- Defanged all text in the document excluding one invalid IP address.

![new2](https://github.com/0x4427/Defanger/assets/91937971/c02b4ba4-25bb-40c8-9f44-6e025812b7d8)

- `Refang` and `Auto Refang All` supported styles

![fubal1](https://github.com/0x4427/Defanger/assets/91937971/7b467a6a-1371-460e-9bdc-da910590ca63)

- Refanged output
  
![final ](https://github.com/0x4427/Defanger/assets/91937971/061893b7-bce4-4e63-b612-a07e28398abe)

| Styles                |
|---------------------------|                                    
|   `hxxp(s)` -> `http(s)`   |
|   `hXXp(s)` -> `http(s)`   |
|       `[://]` -> `://`    |                            
|       `[.]` -> `.`        |                    
|       `{.}` -> `.`        |
|       `(.)` -> `.`        |
|       `[dot]` -> `.`      |
|       `{dot}` -> `.`      |
|       `(dot)` -> `.`      |
|       `[:]` -> `:`        |
|       `\.`  -> `.`        |
|       `[/]` -> `/`        |

# Authors

#### 0x4427
  
[<img align="left" width="50px" src="https://github-production-user-asset-6210df.s3.amazonaws.com/72227999/246673026-44eca0ae-3020-4041-86f4-778f7236e3dc.svg">](https://twitter.com/0x4427/) 
[<img align="left" width="50px" src="https://github-production-user-asset-6210df.s3.amazonaws.com/72227999/246673032-6ab6cd40-918e-429f-9905-c93750dfa35c.svg">](https://linkedin.com/0x4427/) 
[<img align="left" width="55px" src="https://github-production-user-asset-6210df.s3.amazonaws.com/72227999/246673029-1380eb15-d262-40e6-928f-1321a9338e41.svg">](https://github.com/0x4427/)
<br/> &nbsp;

#### knight0x07

[<img align="left" width="50px" src="https://github-production-user-asset-6210df.s3.amazonaws.com/72227999/246673026-44eca0ae-3020-4041-86f4-778f7236e3dc.svg">](https://twitter.com/knight0x07/) 
[<img align="left" width="50px" src="https://github-production-user-asset-6210df.s3.amazonaws.com/72227999/246673032-6ab6cd40-918e-429f-9905-c93750dfa35c.svg">](https://linkedin.com/niraj-s/) 
[<img align="left" width="55px" src="https://github-production-user-asset-6210df.s3.amazonaws.com/72227999/246673029-1380eb15-d262-40e6-928f-1321a9338e41.svg">](https://github.com/knight0x07/)
<br/> &nbsp;

# Misc
This plugin has been created using the Notepad++ pluginpack for .Net https://github.com/kbilsted/NotepadPlusPlusPluginPack.Net/releases
