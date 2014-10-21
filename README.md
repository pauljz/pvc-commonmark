pvc-commonmark
========

Converts CommonMark files into HTML using CommonMark.NET

Usage:

```
pvc.Task("build"), () => {
    pvc.Source("*.md")
        .Pipe(new PvcCommonMark())
        .Save("deploy");
});
```
