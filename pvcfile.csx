pvc.Task("nuget-push", () => {
    pvc.Source("src/Pvc.CommonMark.csproj")
       .Pipe(new PvcNuGetPack(
            createSymbolsPackage: true
       ))
       .Pipe(new PvcNuGetPush());
});
