# Publish Instruction
Make sure to set `/yoursubdir/` or change it manually in `index.html` if this doesn't work.

You can create a published version with this command, assuming your current directory is the WebSite folder.
```dotnet publish -c Release -o ./bin/publish```

Make sure to update `./bin/publish/wwwroot/index.html` to set the `base href` correctly.