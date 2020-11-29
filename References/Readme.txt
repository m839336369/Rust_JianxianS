Hey there!

I wanted to let you know that i deobfuscated the original ulink.dll
That means that the ulink communication should be like 3x faster.
The server runs stable and I also patched the method that was flooding.
I don't exactly know how to fix that error yet, so for now I bypass the flood
This means that when the flood occurs there will be no RED messages at all
on console, neither on the logs. The server should stay online for a longer time
too.

The only thing I know at the moment is that when this error happens
there are points when you can't join the server, stops at waiting for character
When that happens the server is still stable you are able to save everything in 
the console by typing fougerite.save

If you don't want to use this ulink then you should return to the original file.
(Didn't include that)

Also note that I included a prepatched ulink dll and an empty/fresh dll file too.

The patcher now requires the fresh ulink dll, so if you don't want to use the
new dll you can still patch the new one with an assemblycsharp.dll and only
copy the assembly dll on the server.

I would still recommend switching to the deobfuscated ulink.dll

Also, there is an original version of the assembly file, but the usage is not recommended, since I modified two methods with dnspy inorder to make the threaded saving work.
(StructureMaster.FindComponentID and StructureComponent.WriteObjectSave)


HOW TO USE PATCHER:
	1. Have the already provided Assembly-CSharp.dll in the package, or use the deobfuscated version.
	2. Make sure that you are also using the clean uLink.dll and Facepunch.Meshbatch dll.
	3. Run patcher and enter 0.

Have fun