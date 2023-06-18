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

![new1](https://github.com/varun7244/Defanger-1/assets/72227999/a3f4aa9f-8910-40d3-a78a-8699538628a0)

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
- 0x4427 - [Twitter](https://twitter.com/0x4427/) | [LinkedIn](https://www.linkedin.com/in/varun-singh-5944b9222/) | [Github](https://github.com/0x4427/)
- knight0x07 - [Twitter](https://twitter.com/knight0x07/) | [LinkedIn](https://www.linkedin.com/in/niraj-s/) | [Github](https://github.com/knight0x07/)

# Misc
This plugin has been created using the Notepad++ pluginpack for .Net https://github.com/kbilsted/NotepadPlusPlusPluginPack.Net/releases
