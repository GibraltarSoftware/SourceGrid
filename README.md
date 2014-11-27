SourceGrid
==========

This is a modified version of SourceGrid 4.11 used by Loupe.  
SourceGrid is an open source .NET Windows Forms grid control.

Main Project Site
-----------------

The official information site for SourceGrid is [On CodePlex](http://sourcegrid.codeplex.com/).  
If you want to contribute to SourceGrid you should go there and follow the official branch unless you
specifically want our customized version.

What's Different About This Version
-----------------------------------

Several years ago we customized the source code of SouceGrid to address issues with multiple UI threads. 
This version supports multiple UI threads in the same application domain using grids at the same time.
Loupe uses this to have multiple truly independent log viewers running concurrently on different threads
to maximize interactive performance and to isolate our viewer from the applications that integrate it.

Unless you're planning on creating different main UI threads within a single application domain (and 
if you are, you should seriously consider why you are) you're better off with the official distribution.

License
-------

SourceGrid, and therefore this customized version of SourceGrid, is covered by the MIT License

Copyright(c) 2009 Davide Icardi

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
documentation files (the "Software"), to deal in the Software without restriction, including without limitation the 
rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to 
permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the 
Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
