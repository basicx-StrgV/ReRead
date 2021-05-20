# ReRead
 
## What is ReRead
ReRead is a small console program that can make code files, that are hard to read (e.g. one line .js files), more readable.

![ReRead](https://i.imgur.com/McLc8jk.png)

![ReRead](https://i.imgur.com/wWgSGIW.png)

## How to use
When the program is first started, it will create a **ReRead** folder, that contains an **Input**, **Output** and **Log** folder.

Put the files that you want to use in the **Input** directory.

ReRead will show you a list of every file in the **Input** directory.
Then you just need to select the file that you want.

When the program shows **DONE** your new file can be found in the **Output** directory.

### Options

You can also use arguments.

Use "-file" or "-f" to only use a specified file from any directory you want:
```
ReRead.exe -f C:\myDirectory\file.js"
```

Use "-directory" or "-d" to use every file from any directory you want:
```
"ReRead.exe -d C:\myDirectory" 
```

## Project Infos
### Dependencies
BasicxLogger: 
- Git: https://github.com/basicx-StrgV/BasicxLogger
- nuget: https://www.nuget.org/packages/BasicxLogger/
