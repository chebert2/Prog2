@rem Windows compilation batch script for compiling the Scheme interpreter
@rem Update the path in the call command for your Visual Studio installation

call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\VsDevCmd.bat"

csc /target:library /out:SPP.dll Parse\*.cs Print\*.cs Tokens\*.cs
csc /target:module /out:SPP.netmodule Parse\*.cs Print\*.cs Tokens\*.cs
csc /out:Scheme4101.exe Scheme4101.cs Tree\*.cs Special\*.cs /addmodule:SPP.netmodule
